using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace ImageProcessing.Model
{
    public class ImageFilterModel
    {
        private string _filterId;
        private Image _image;
        private bool _isBusy;
        private bool _isNeedToProcessImage;
        private bool _isNeedToApplyFilterToCurrentImage;
        private readonly Dictionary<string, IImageFilter> _filters = new Dictionary<string, IImageFilter>();

        // first param is filter id, second param is filter description
        public event Action<string, string> FilterIsAdded;
        public event Action<Image> CurrentImageChanged;
        public event Action<Image> PreviewImageChanged;
        // filter id
        public event Action<string> CurrentFilterChanged;

        public string FilterId
        {
            get { return _filterId; }
            set
            {
                _filterId = _filters.ContainsKey(value) ? value : "";
                CurrentFilterChanged?.Invoke(_filterId);
            }
        }

        public IImageFilter Filter()
        {
            return _filters.ContainsKey(FilterId) ? _filters[FilterId] : null;
        }

        public string FilterDescription(string filterId)
        {
            try
            {
                return _filters[filterId].FilterDescription();
            }
            catch (Exception)
            {
                return "";
            }
        }
        
        public ICollection<string> RegiseteredFilters()
        {
            return _filters.Keys;
        }

        public bool ApplyFilterSetting(IImageFilterSetting setting)
        {
            var result = false;
            var filter = Filter();
            if (filter != null)
            {
                result = filter.ApplySettings(setting);
            }
            return result;
        }

        private static Image ConverImageToArgb32(Image sourceImage)
        {   
            var result = new Bitmap(sourceImage.Width, sourceImage.Height, PixelFormat.Format32bppArgb);
            using (var gr = Graphics.FromImage(result))
            {
                gr.DrawImage(sourceImage, new Rectangle(0, 0, result.Width, result.Height));
            }
            return result;
        }

        public Image Image
        {
            get { return _image; }
            set {
                // force argb32 image format
                _image = value.PixelFormat != PixelFormat.Format32bppArgb ? ConverImageToArgb32(value) : value;
                PreviewImage?.Dispose();
                PreviewImage = (Image) _image.Clone();
                CurrentImageChanged?.Invoke(_image);
            }
        }

        private async void ProcessImageHelper(Image image, IImageFilter filter, bool applyToCurrentImage)
        {   
            var clonedImg = (Image)image.Clone();
            _isBusy = true;
            // ReSharper disable once AccessToDisposedClosure
            var tmpImage = await Task.Run(() => filter.ProcessImage(clonedImg));
            clonedImg.Dispose();
            if (applyToCurrentImage)
            {
                Image = tmpImage;
            }
            else
            {
                PreviewImage?.Dispose();
                PreviewImage = tmpImage;
                PreviewImageChanged?.Invoke(tmpImage);
            }
            
            _isBusy = false;
            
            if (!_isNeedToProcessImage) return;
            _isNeedToProcessImage = false;
            _isNeedToApplyFilterToCurrentImage = false;
            Debug.WriteLine("auto update filter");
            ProcessImage(_isNeedToApplyFilterToCurrentImage);
        }

        public bool ProcessImage(bool applyToCurrentImage=false)
        {
            var filter = Filter();
            if (filter == null || Image == null) return false;
            if (_isBusy)
            {
                // ставим флаг, указывающий что после окончания обработки текущего значения фильтра
                // надо снова обработать изображение, так как изменились настройки фильтра.

                if (!_isNeedToApplyFilterToCurrentImage)
                {
                    _isNeedToApplyFilterToCurrentImage = applyToCurrentImage;
                }
                _isNeedToProcessImage = true;
                return false;
            }
            ProcessImageHelper(Image, filter, applyToCurrentImage);
            return true;
        }

        public Image PreviewImage { get; private set; }

        public void RegisterFilter(IImageFilter filter)
        {
            _filters.Add(filter.FilterId(), filter);
            FilterIsAdded?.Invoke(filter.FilterId(), filter.FilterDescription());
        }
    }
}

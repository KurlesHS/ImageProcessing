using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ImageProcessing.Model;
using ImageProcessing.View;

namespace ImageProcessing.Presenter
{
    public class ImageFilterPresenter
    {
        private readonly IMainView _view;
        private readonly ImageFilterModel _model;
        private readonly Dictionary<string, IFilterSettingChanger> _filterSettingChangers = new Dictionary<string, IFilterSettingChanger>();
        
        public ImageFilterPresenter(IMainView view, ImageFilterModel model)
        {
            _view = view;
            _view.NewImageLoaded += _view_NewImageLoaded;
            _view.RequestApplyImageChanges += _view_RequestApplyImageChanges;
            _view.RequestChangeFilter += ViewRequestChangeFilter;

            _model = model;
            _model.CurrentFilterChanged += _model_CurrentFilterChanged;
            _model.CurrentImageChanged += _model_CurrentImageChanged;
            _model.FilterIsAdded += _model_FilterIsAdded;
            _model.PreviewImageChanged += ModelOnPreviewImageChanged;

            foreach (var filterId in _model.RegiseteredFilters())
            {
                var description = _model.FilterDescription(filterId);
                if (description.Length > 0)
                {
                    view.RegisterImageFilter(filterId, description);
                }
            }
        }

        public void RegisterFilterSettingChanger(IFilterSettingChanger settingChanger)
        {
            _filterSettingChangers[settingChanger.FilterId()] = settingChanger;
            settingChanger.FilterSettingChanged += SettingChanger_FilterSettingChanged;
        }

        private void SettingChanger_FilterSettingChanged(IImageFilterSetting setting)
        {
            _model.ApplyFilterSetting(setting);
            _model.ProcessImage();
        }

        private void ModelOnPreviewImageChanged(Image image)
        {
            _view.SetImage(image);
        }

        private void _model_FilterIsAdded(string filterId, string filterDescription)
        {
            _view.RegisterImageFilter(filterId, filterDescription);
        }

        private void _model_CurrentImageChanged(Image image)
        {
            _view.SetImage(image);
        }

        private void _model_CurrentFilterChanged(string filterId)
        {
            _view.SetCurrentFilter(filterId);
            _view.SetFilterSettingControl(null);
            if (_model.Image != null)
            {
                _view.SetImage(_model.Image);
            }
            Control settingControl = null;
            try
            {
                var changer = _filterSettingChangers[filterId];
                settingControl = (Control) changer;
            }
            catch (Exception)
            {
                // nothing to do here
            }
            finally
            {
                _view.SetFilterSettingControl(settingControl);
            }
        }

        private void ViewRequestChangeFilter(string filterId)
        {
            if (_model.FilterId == filterId) return;
            _model.FilterId = filterId;
        }

        private void _view_RequestApplyImageChanges()
        {
            _model.ProcessImage(true);
        }

        private void _view_NewImageLoaded(Image image)
        {
            _model.Image = image;
        }
    }
}

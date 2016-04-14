using ImageProcessing.Model;

namespace ImageProcessing.ContrastFilter
{
    internal class ContrastImageFilterSetting : IImageFilterSetting
    {
        private int _threshold = -100;

        public bool Grayscale { get; set; } = false;

        public int Threshold
        {
            get { return _threshold; }
            set {
                if (value > 100)
                {
                    _threshold = 100;
                }
                else if (value < -100)
                {
                    _threshold = -100;
                }
                else
                {
                    _threshold = value;
                }
            }
            
        }
    }
}

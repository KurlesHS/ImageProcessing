using ImageProcessing.Model;

namespace ImageProcessing.GradientFilter
{
    internal class GradientImageFilterSetting : IImageFilterSetting
    {

        public GradientImageFilterSetting()
        {
            Operator = GradientOperator.Sobel;
        }

        public enum GradientOperator
        {
            Sobel,
            Prewitt,
            SobelFeldman
        }

        public GradientOperator Operator { get; set; }
    }
}

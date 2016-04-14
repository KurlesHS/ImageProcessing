using System.Drawing;

namespace ImageProcessing.Model
{
    public interface IImageFilter
    {
        string FilterDescription();
        string FilterId();
        bool ApplySettings(IImageFilterSetting setting);
        Image ProcessImage(Image sourceImage);
    }
}

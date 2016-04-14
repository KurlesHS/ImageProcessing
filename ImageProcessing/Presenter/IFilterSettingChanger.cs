using System;
using ImageProcessing.Model;

namespace ImageProcessing.Presenter
{
    public interface IFilterSettingChanger
    {
        event Action<IImageFilterSetting> FilterSettingChanged;
        string FilterId();
    }
}

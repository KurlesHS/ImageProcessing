using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageProcessing.View
{
    public interface IMainView
    {
        event Action RequestApplyImageChanges;
        event Action<Image> NewImageLoaded;
        event Action<string> RequestChangeFilter;

        void SetImage(Image image);
        void RegisterImageFilter(string filterId, string filterDescription);
        bool SetCurrentFilter(string filterId);
        void SetFilterSettingControl(Control control);
    }
}

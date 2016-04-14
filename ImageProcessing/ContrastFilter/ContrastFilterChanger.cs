using System;
using System.Windows.Forms;
using ImageProcessing.Model;
using ImageProcessing.Presenter;

namespace ImageProcessing.ContrastFilter
{
    public partial class ContrastFilterChanger : UserControl, IFilterSettingChanger
    {
        public ContrastFilterChanger()
        {
            InitializeComponent();
        }

        public event Action<IImageFilterSetting> FilterSettingChanged;

        public string FilterId()
        {
            return "contrast";
        }

        private void ApplySettings()
        {
            var setting = new ContrastImageFilterSetting {Threshold = vScrollBar.Value, Grayscale = radioButtonGrayscale.Checked};
            FilterSettingChanged?.Invoke(setting);
        }

        private void radioButtonGrayscale_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButtonGrayscale.Checked) return;
            ApplySettings();
        }

        private void radioButtonColor_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButtonColor.Checked) return;
            ApplySettings();
        }

        private void vScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            ApplySettings();
        }
    }
}

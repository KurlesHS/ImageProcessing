using System;
using System.Windows.Forms;
using ImageProcessing.Model;
using ImageProcessing.Presenter;

namespace ImageProcessing.GradientFilter
{
    public partial class GradientSettingChanger : UserControl, IFilterSettingChanger
    {
        public GradientSettingChanger()
        {
            InitializeComponent();
        }

        public event Action<IImageFilterSetting> FilterSettingChanged;
        public string FilterId()
        {
            return "gradient";
        }

        private void HandleNewSetting(object sender, GradientImageFilterSetting.GradientOperator oper)
        {
            try
            {
                var radioButton = (RadioButton)sender;
                if (!radioButton.Checked) return;
                var setting = new GradientImageFilterSetting()
                {
                    Operator = oper
                };
                FilterSettingChanged?.Invoke(setting);
            }
            catch (InvalidCastException)
            {
            }
        }

        private void radioButtonSobel_CheckedChanged(object sender, EventArgs e)
        {
            HandleNewSetting(sender, GradientImageFilterSetting.GradientOperator.Sobel);
        }

        private void radioButtonPrewitt_CheckedChanged(object sender, EventArgs e)
        {
            HandleNewSetting(sender, GradientImageFilterSetting.GradientOperator.Prewitt);
        }

        private void radioButtonSobelFeldman_CheckedChanged(object sender, EventArgs e)
        {
            HandleNewSetting(sender, GradientImageFilterSetting.GradientOperator.SobelFeldman);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ImageProcessing.View
{
    public partial class MainView : Form, IMainView
    {
        public class ImageFilterData
        {
            public string Id;
            public string Description;
        }

        public event Action RequestApplyImageChanges;
        public event Action<Image> NewImageLoaded;
        public event Action<string> RequestChangeFilter;

        private Control _currentSettingControl;

        public MainView()
        {
            InitializeComponent();
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";
        }

        private void buttonApplyChanges_Click(object sender, EventArgs e)
        {
            RequestApplyImageChanges?.Invoke();
        }

        private void buttonLoadImage_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = @"Image Files|(*.jpg; *.png; *.bmp)",
                Title = @"Select a Image File"
            };

            // Show the Dialog.
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            try
            {
                var image = Image.FromFile(openFileDialog.FileName);
                NewImageLoaded?.Invoke(image);
            }
            catch (Exception)
            {
                // Ignore all errors / do nothing / suppress resharper warning =)
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var filterId = ((KeyValuePair<string, string>) (comboBox1.Items[comboBox1.SelectedIndex])).Key;
                if (filterId.Length > 0)
                {
                    RequestChangeFilter?.Invoke(filterId);
                }
            }
            catch (InvalidCastException)
            {
                // do nothing here
            }
        }
        
        public void SetImage(Image image)
        {   
            var prevImg = pictureBoxResultImage.Image;
            prevImg?.Dispose();
            pictureBoxResultImage.Image = (Image)image.Clone();
        }

        public void RegisterImageFilter(string filterId, string filterDescription)
        {
            comboBox1.Items.Add(new KeyValuePair<string, string>(filterId, filterDescription));
        }

        public bool SetCurrentFilter(string filterId)
        {
            // int index = comboBox1.Items.IndexOf(filterId); // ?почему то не работает?
            var index = -1;
            for (var i = 0; i < comboBox1.Items.Count; ++i)
            {
                if (((KeyValuePair<string, string>) (comboBox1.Items[i])).Key != filterId) continue;
                index = i;
                break;
            }
            

            if (index < 0) return false;
            comboBox1.SelectedIndex = index;
            return true;
        }

        public void SetFilterSettingControl(Control control)
        {
            if (_currentSettingControl != null)
            {
                panelForSettingsControls.Controls.Remove(_currentSettingControl);
            }
            
            _currentSettingControl = control;
            if (control == null) return;
            control.Dock = DockStyle.Fill;
            panelForSettingsControls.Controls.Add(control);
        }
    }
}

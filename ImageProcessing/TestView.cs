using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using ImageProcessing.ContrastFilter;
using ImageProcessing.GradientFilter;
using ImageProcessing.Model;

namespace ImageProcessing
{
    public partial class TestView : Form
    {
        private readonly ImageFilterModel _model = new ImageFilterModel();
        public TestView()
        {
            InitializeComponent();
            _model.CurrentFilterChanged += _model_CurrentFilterChanged;
            _model.CurrentImageChanged += ModelCurrentImageChanged;
            _model.FilterIsAdded += ModelFilterIsAdded;
            _model.PreviewImageChanged += ModelPreviewImageChanged;
            
            _model.RegisterFilter(new GradientImageFilter());
            _model.RegisterFilter(new ContrastImageFilter());

        }

        private void ModelPreviewImageChanged(Image obj)
        {
            Debug.WriteLine("preview image changed: w: " + obj.Width + " h:" + obj.Height);
            var prevImg = pictureBox1.Image;
            prevImg?.Dispose();
            pictureBox1.Image = (Image) obj.Clone();
        }

        private void ModelFilterIsAdded(string arg1, string arg2)
        {
            Debug.WriteLine("filter added, name: " + arg1 + ": " + arg2);
            _model.FilterId = arg1;
        }

        private void ModelCurrentImageChanged(Image obj)
        {
            Debug.WriteLine("current image changed: w: " + obj.Width + " h:" + obj.Height);
        }

        private void _model_CurrentFilterChanged(string obj)
        {
            Debug.WriteLine("current filter changed: " + obj);
        }

        private void button1_Click(object sender, EventArgs e)
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
                _model.Image = image;
                _model.ProcessImage();
            }
            catch (Exception)
            {
                // Ignore all errors / do nothing / suppress resharper warning =)
            }
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            var setting = new ContrastImageFilterSetting {Threshold = hScrollBar1.Value};
            _model.ApplyFilterSetting(setting);
            _model.ProcessImage();
        }
    }
}

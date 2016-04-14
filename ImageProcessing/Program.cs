using System;
using System.Windows.Forms;
using ImageProcessing.ContrastFilter;
using ImageProcessing.GradientFilter;
using ImageProcessing.Model;
using ImageProcessing.Presenter;
using ImageProcessing.View;

namespace ImageProcessing
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var view = new MainView();
            var model = new ImageFilterModel();
            var presenter = new ImageFilterPresenter(view, model);
            model.RegisterFilter(new ContrastImageFilter());
            model.RegisterFilter(new GradientImageFilter());
            var fc = new GradientSettingChanger();
            presenter.RegisterFilterSettingChanger(fc);
            presenter.RegisterFilterSettingChanger(new ContrastFilterChanger());
            model.FilterId = fc.FilterId();
            Application.Run(view);
        }
    }
}

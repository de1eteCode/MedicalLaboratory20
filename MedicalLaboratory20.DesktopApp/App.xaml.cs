using MedicalLaboratory20.DesktopApp.Core;
using MedicalLaboratory20.DesktopApp.View.Window;
using MedicalLaboratory20.DesktopApp.ViewModel.Window;
using System.Windows;

namespace MedicalLaboratory20.DesktopApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var inst = WindowManager.GetInstance();
            inst.RegisterWindow<LoginVM, LoginWindow>();
            inst.RegisterWindow<WindowPresentorVM, PresentorWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var inst = WindowManager.GetInstance();
            inst.ShowWindow(new LoginVM());
        }
    }
}

using MedicalLaboratory20.DesktopApp.Core;
using MedicalLaboratory20.DesktopApp.WindowArea.ViewModels;
using MedicalLaboratory20.DesktopApp.WindowArea.Views;
using System.Windows;

namespace MedicalLaboratory20.DesktopApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public WindowController WinController = new WindowController();

        public App()
        {
            WinController.RegisterVMAndWindow<LoginVM, Login>();
            WinController.RegisterVMAndWindow<LaborantVM, Workflow>();
            WinController.RegisterVMAndWindow<LaborantResearcherVM, Workflow>();
            WinController.RegisterVMAndWindow<AccountantVM, Workflow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            WinController.ShowWindow(new LoginVM());
        }
    }
}

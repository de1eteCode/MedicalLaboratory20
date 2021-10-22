using MedicalLaboratory20.DesktopApp.Core;
using MedicalLaboratory20.DesktopApp.WindowArea.ViewModels;
using MedicalLaboratory20.DesktopApp.WindowArea.ViewModels.Modals;
using MedicalLaboratory20.DesktopApp.WindowArea.Views;
using MedicalLaboratory20.DesktopApp.WindowArea.Views.Modals;
using System.Windows;

namespace MedicalLaboratory20.DesktopApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public WindowController WinController = new();

        public App()
        {
            WinController.RegisterVMAndWindow<LoginVM, Login>();
            WinController.RegisterVMAndWindow<LaborantVM, Workflow>();
            WinController.RegisterVMAndWindow<LaborantResearcherVM, Workflow>();
            WinController.RegisterVMAndWindow<AccountantVM, Workflow>();
            WinController.RegisterVMAndWindow<AdminVM, Workflow>();

            //modals
            WinController.RegisterVMAndWindow<AddPatientVM, AddPatient>();
            WinController.RegisterVMAndWindow<AddServiceVM, AddService>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            WinController.ShowWindow(new LoginVM());
        }
    }
}

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
            WinController.RegisterVmAndWindow<LoginVm, Login>();
            WinController.RegisterVmAndWindow<LaborantVm, Workflow>();
            WinController.RegisterVmAndWindow<LaborantResearcherVm, Workflow>();
            WinController.RegisterVmAndWindow<AccountantVm, Workflow>();
            WinController.RegisterVmAndWindow<AdminVm, Workflow>();

            //modals
            WinController.RegisterVmAndWindow<AddPatientVm, AddPatient>();
            WinController.RegisterVmAndWindow<AddServiceVm, AddService>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            WinController.ShowWindow(new LoginVm());
        }
    }
}

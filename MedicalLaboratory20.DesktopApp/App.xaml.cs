using MedicalLaboratory20.DesktopApp.Services;
using MedicalLaboratory20.DesktopApp.View.Window;
using MedicalLaboratory20.DesktopApp.ViewModel;
using MedicalLaboratory20.DesktopApp.ViewModel.Window;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;

namespace MedicalLaboratory20.DesktopApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private bool _closing = false;

        static App()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            test();
            base.OnStartup(e);

            var authForm = new LoginWindow();
            authForm.Closed += ClosedWindow;
            authForm.ShowDialog();

            var result = authForm.DataContext as LoginVM;
            if (result.IsLogIn)
            {
                var window = new PresentorWindow();
                window.Closed += ClosedWindow;
                window.Show();
            }
        }

        private void test()
        {
            var client = ClientService.GetInstance();

            client.Auth("chacking0", "4tzqHdkqzo4");

            var user = client.User;
        }

        private void ClosedWindow(object? sender, EventArgs e)
        {
            if (sender is LoginWindow lWind)
            {
                _closing = ((LoginVM)lWind.DataContext).IsLogIn is false;
            }
            else
            {
                _closing = true;
            }

            if (_closing is true)
            {
                Shutdown();
            }            
        }
    }
}

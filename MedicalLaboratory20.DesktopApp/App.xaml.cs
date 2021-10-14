using MedicalLaboratory20.DesktopApp.View.Window;
using MedicalLaboratory20.DesktopApp.ViewModel;
using MedicalLaboratory20.DesktopApp.ViewModel.Window;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
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

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var authForm = new LoginWindow();
            authForm.Closed += ClosedWindow;
            authForm.ShowDialog();

            var result = authForm.DataContext as LoginVM;
            if (result.IsLoggedIn)
            {
                var window = new PresentorWindow();
                window.Closed += ClosedWindow;
                window.Show();
            }
        }

        private void ClosedWindow(object? sender, EventArgs e)
        {
            if (sender is LoginWindow lWind)
            {
                _closing = ((LoginVM)lWind.DataContext).IsLoggedIn is false;
            }
            else if (sender is PresentorWindow)
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

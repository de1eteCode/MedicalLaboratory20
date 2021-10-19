using MedicalLaboratory20.DesktopApp.WindowArea.ViewModels;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace MedicalLaboratory20.DesktopApp.WindowArea.Abstract
{
    abstract class BaseWindowVM : ObservableObject
    {
        protected Dispatcher _dispatcher;

        public BaseWindowVM()
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
        }

        private ICommand LogOutCommand
        {
            get
            {
                return new RelayCommand(LogOut);
            }
        }

        private void LogOut()
        {
            OpenNewWindowAndCloseOld(new LoginVM());
        }

        protected void OpenNewWindowAndCloseOld(BaseWindowVM vm)
        {
            var app = (App)Application.Current;
            app.WinController.ShowWindow(vm);
            app.WinController.HideWindow(this);
        }
    }
}

using MedicalLaboratory20.DesktopApp.WindowArea.ViewModels;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace MedicalLaboratory20.DesktopApp.WindowArea.Abstract
{
    abstract class BaseWindowVm : ObservableObject
    {
        protected Dispatcher Dispatcher;

        protected BaseWindowVm()
        {
            Dispatcher = Dispatcher.CurrentDispatcher;
        }

        public ICommand LogOutCommand => new RelayCommand(LogOut);

        private void LogOut()
        {
            OpenNewWindowAndCloseOld(new LoginVm());
        }

        protected void OpenNewWindowAndCloseOld(BaseWindowVm vm)
        {
            var app = (App)Application.Current;
            app.WinController.ShowWindow(vm);
            app.WinController.HideWindow(this);
        }
    }
}

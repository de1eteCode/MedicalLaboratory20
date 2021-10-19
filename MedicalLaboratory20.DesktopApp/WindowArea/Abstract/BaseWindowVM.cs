using MedicalLaboratory20.DesktopApp.WindowArea.ViewModels;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Windows;

namespace MedicalLaboratory20.DesktopApp.WindowArea.Abstract
{
    abstract class BaseWindowVM : ObservableObject
    {
        protected void OpenNewWindowAndCloseOld()
        {
            var app = (App)Application.Current;
            app.WinController.ShowWindow(new WorkflowVM());
            app.WinController.HideWindow(this);
        }
    }
}

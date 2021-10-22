using MedicalLaboratory20.DesktopApp.WindowArea.Abstract;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Windows;

namespace MedicalLaboratory20.DesktopApp.PageArea.Abstract
{
    abstract class PageVmBase : ObservableObject
    {
        public bool IsLoaded { get; set; }

        public abstract string Title { get; }

        /// <summary>
        /// Возникает при открытии страницы, необходимо вызывать базовую реализацию, или изменять значение IsLoaded на true
        /// </summary>
        public virtual void OnLoad()
        {
            IsLoaded = true;
        }

        /// <summary>
        /// Возникает при каждом открытии страницы
        /// </summary>
        public virtual void OnOpen() { }

        /// <summary>
        /// Открытие модальных окон
        /// </summary>
        /// <param name="vm">Объект ViewModal для окна</param>
        protected void OpenDialog(BaseModalWindowVm vm)
        {
            var app = (App)Application.Current;
            app.WinController.ShowModal(vm);
        }
    }
}

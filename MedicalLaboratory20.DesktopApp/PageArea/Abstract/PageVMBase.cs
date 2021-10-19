using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace MedicalLaboratory20.DesktopApp.PageArea.Abstract
{
    abstract class PageVMBase : ObservableObject
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
    }
}

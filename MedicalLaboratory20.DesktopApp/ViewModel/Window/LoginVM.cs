using MedicalLaboratory20.DesktopApp.ViewModel.Abstract;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MedicalLaboratory20.DesktopApp.ViewModel.Window
{
    class LoginVM : ObservableObject, IWinVM
    {
        private ICommand _loginCommand;
        public bool IsLogIn { get; set; }


        public ICommand LoginCommand
        {
            get
            {
                return _loginCommand ??= new RelayCommand(LogInSystem);
            }
        }

        private void LogInSystem()
        {
            IsLogIn = true;
        }
    }
}

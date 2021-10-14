using MedicalLaboratory20.DesktopApp.ViewModel.Abstract;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MedicalLaboratory20.DesktopApp.ViewModel.Window
{
    public class WindowPresentorVM : ObservableObject, IWinVM
    {
        private ICommand _changePageVM;
        private IPageVM _currentPageVM;
        private ObservableCollection<IPageVM> _pageVMs;


        public WindowPresentorVM()
        {

        }

        public ObservableCollection<IPageVM> PageVMs
        {
            get
            {
                if (_pageVMs is null)
                {
                    _pageVMs = new();
                }
                return _pageVMs;
            }
        }
        public IPageVM CurrentPageVM
        {
            get => _currentPageVM;
            set
            {
                _currentPageVM = value;
                OnPropertyChanged(nameof(CurrentPageVM));
            }
        }

        private void RegisterPageVM<T>(T vm)
            where T : IPageVM
        {
            PageVMs.Add(vm);

            if (CurrentPageVM is null)
            {
                CurrentPageVM = PageVMs.First();
            }
        }

        public ICommand ChangePageVM
        {
            get
            {
                return _changePageVM ??= new RelayCommand<IPageVM>(ChangeVM, obj => obj is IPageVM);
            }
        }

        private void ChangeVM(IPageVM vm)
        {
            if (!PageVMs.Contains(vm))
                RegisterPageVM(vm);

            CurrentPageVM = PageVMs.First(v => v == vm);
        }
    }
}

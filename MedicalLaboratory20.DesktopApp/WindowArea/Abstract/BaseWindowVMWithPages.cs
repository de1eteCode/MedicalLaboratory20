using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel;
using MedicalLaboratory20.DesktopApp.Core;
using MedicalLaboratory20.DesktopApp.PageArea.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalLaboratory20.DesktopApp.WindowArea.Abstract
{
    abstract class BaseWindowVMWithPages : BaseWindowVM
    {
        private readonly PageController _pageController;
        private Page? _currentPage;

        public BaseWindowVMWithPages()
        {
            _pageController = new PageController();

            ChangePageCommand = new RelayCommand<PageVMBase>(SwitchPage);
            ClosePageCommand = new RelayCommand(ClosePage);
        }

        public IEnumerable<PageVMBase> Pages => _pageController.Pages;

        public Page CurrentPage
        {
            get => _currentPage;
            set
            {
                if (_currentPage != value)
                {
                    _currentPage = value;
                    OnPropertyChanged();
                }
            }
        }

        #region Commands

        public ICommand ChangePageCommand { get; }
        public ICommand ClosePageCommand { get; }

        #endregion

        protected void RegisterPageWithVm<VM, Pag>()
            where VM : PageVMBase
            where Pag : Page
        {
            _pageController.RegisterVMAndPage<VM, Pag>();

            if (CurrentPage is null)
            {
                SwitchPage((PageVMBase)_pageController.GetFirstPage().DataContext);
            }
        }

        private void SwitchPage(PageVMBase vm)
        {
            if (vm != CurrentPage?.DataContext)
            {
                if (vm.IsLoaded is false)
                {
                    Task.Run(() => vm.OnLoad());
                }
                CurrentPage = _pageController.GetPage(vm);
            }
        }

        private void ClosePage()
        {
            PageVMBase vmForClose = (PageVMBase)_currentPage.DataContext;
            CurrentPage = _pageController.GetLastPage();
            _pageController.HidePage(vmForClose);
        }
    }
}

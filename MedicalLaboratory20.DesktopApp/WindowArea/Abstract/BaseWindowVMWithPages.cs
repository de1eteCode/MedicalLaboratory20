using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel;
using MedicalLaboratory20.DesktopApp.Core;
using System.Collections.Generic;
using MedicalLaboratory20.DesktopApp.PageArea.Abstract;
using System.Threading.Tasks;

namespace MedicalLaboratory20.DesktopApp.WindowArea.Abstract
{
    abstract class BaseWindowVmWithPages : BaseWindowVm
    {
        private readonly PageController _pageController;
        private Page? _currentPage;

        protected BaseWindowVmWithPages()
        {
            _pageController = new PageController();

            ChangePageCommand = new RelayCommand<PageVmBase>(SwitchPage);
            ClosePageCommand = new RelayCommand(ClosePage);
        }

        public IEnumerable<PageVmBase> Pages => _pageController.Pages;

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

        protected void RegisterPageWithVm<TVm, TPag>()
            where TVm : PageVmBase
            where TPag : Page
        {
            _pageController.RegisterVmAndPage<TVm, TPag>();

            if (CurrentPage is null)
            {
                SwitchPage((PageVmBase)_pageController.GetFirstPage().DataContext);
            }
        }

        private void SwitchPage(PageVmBase vm)
        {
            if (vm != CurrentPage?.DataContext)
            {
                if (vm.IsLoaded is false)
                {
                    Task.Run(vm.OnLoad);
                }
                Task.Run(vm.OnOpen);
                CurrentPage = _pageController.GetPage(vm);
            }
        }

        private void ClosePage()
        {
            PageVmBase vmForClose = (PageVmBase)_currentPage.DataContext;
            CurrentPage = _pageController.GetLastPage();
            _pageController.HidePage(vmForClose);
        }
    }
}

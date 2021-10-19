using MedicalLaboratory20.DesktopApp.PageArea.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace MedicalLaboratory20.DesktopApp.Core
{
    class PageController
    {
        private readonly Dictionary<Type, Type> _vmToPageMap = new();
        private readonly Dictionary<PageVMBase, Page> _createdPages = new();

        public IEnumerable<PageVMBase> Pages => _createdPages.Keys;

        public void RegisterVMAndPage<VM, Pag>()
            where VM : PageVMBase
            where Pag : Page
        {
            var typeVm = typeof(VM);
            if (typeVm.IsInterface || typeVm.IsAbstract)
                throw new ArgumentException(typeVm.Name + " can't to register");

            if (_vmToPageMap.ContainsKey(typeVm))
                throw new InvalidOperationException(typeVm.Name + " is registered");

            _vmToPageMap[typeVm] = typeof(Pag);

            // Костыль
            var vm = (PageVMBase)Activator.CreateInstance(typeVm);
            var page = CreatePageWithVM(vm);
            _createdPages[vm] = page;
        }

        private Page CreatePageWithVM(PageVMBase vm)
        {
            if (!_vmToPageMap.TryGetValue(vm.GetType(), out Type? pag))
                throw new InvalidOperationException(nameof(vm));

            if (pag is null)
                throw new ArgumentNullException(nameof(pag));

            var page = (Page)Activator.CreateInstance(pag);
            page.DataContext = vm;
            return page;
        }

        public Page GetPage(PageVMBase vm)
        {
            if (vm is null)
                throw new ArgumentNullException(nameof(vm));

            if (_createdPages.TryGetValue(vm, out var page))
                return page;

            page = CreatePageWithVM(vm);
            _createdPages[vm] = page;
            return page;
        }

        public void HidePage(PageVMBase vm)
        {
            if (!_createdPages.TryGetValue(vm, out var page))
                throw new InvalidOperationException(nameof(vm));

            _createdPages.Remove(vm);
        }

        public Page GetFirstPage()
        {
            if (_createdPages.Count == 0)
                GetPage((PageVMBase)Activator.CreateInstance(_vmToPageMap.FirstOrDefault().Key));

            return _createdPages.FirstOrDefault().Value;
        }

        public Page GetLastPage()
        {
            if (_createdPages.Count == 0)
                GetPage((PageVMBase)Activator.CreateInstance(_vmToPageMap.LastOrDefault().Key));

            return _createdPages.LastOrDefault().Value;
        }
    }
}

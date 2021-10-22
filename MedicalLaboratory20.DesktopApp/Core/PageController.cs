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
        private readonly Dictionary<PageVmBase, Page> _createdPages = new();

        public IEnumerable<PageVmBase> Pages => _createdPages.Keys;

        public void RegisterVmAndPage<TVm, TPag>()
            where TVm : PageVmBase
            where TPag : Page
        {
            var typeVm = typeof(TVm);
            if (typeVm.IsInterface || typeVm.IsAbstract)
                throw new ArgumentException(typeVm.Name + " can't to register");

            if (_vmToPageMap.ContainsKey(typeVm))
                throw new InvalidOperationException(typeVm.Name + " is registered");

            _vmToPageMap[typeVm] = typeof(TPag);

            // Костыль
            if (Activator.CreateInstance(typeVm) is not PageVmBase vm) 
                return;
            var page = CreatePageWithVm(vm);
            _createdPages[vm] = page;
        }

        private Page CreatePageWithVm(PageVmBase vm)
        {
            if (!_vmToPageMap.TryGetValue(vm.GetType(), out Type? pag))
                throw new InvalidOperationException(nameof(vm));

            if (pag is null)
                throw new ArgumentNullException(nameof(pag));

            var page = Activator.CreateInstance(pag) as Page;
            page.DataContext = vm;
            return page;
        }

        public Page GetPage(PageVmBase vm)
        {
            if (vm is null)
                throw new ArgumentNullException(nameof(vm));

            if (_createdPages.TryGetValue(vm, out var page))
                return page;

            page = CreatePageWithVm(vm);
            _createdPages[vm] = page;
            return page;
        }

        public void HidePage(PageVmBase vm)
        {
            if (!_createdPages.TryGetValue(vm, out var page))
                throw new InvalidOperationException(nameof(vm));

            _createdPages.Remove(vm);
        }

        public Page GetFirstPage()
        {
            if (_createdPages.Count == 0)
                GetPage((PageVmBase)Activator.CreateInstance(_vmToPageMap.FirstOrDefault().Key)!);

            return _createdPages.FirstOrDefault().Value;
        }

        public Page GetLastPage()
        {
            if (_createdPages.Count == 0)
                GetPage((PageVmBase)Activator.CreateInstance(_vmToPageMap.LastOrDefault().Key)!);

            return _createdPages.LastOrDefault().Value;
        }
    }
}

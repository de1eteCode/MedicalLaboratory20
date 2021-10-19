using MedicalLaboratory20.DesktopApp.PageArea.ViewModels;
using MedicalLaboratory20.DesktopApp.PageArea.Views;
using MedicalLaboratory20.DesktopApp.WindowArea.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLaboratory20.DesktopApp.WindowArea.ViewModels
{
    internal class AdminVM : WorkflowVM
    {
        public AdminVM()
        {
            RegisterPageWithVm<HistoryLoginVM, HistoryLogin>();
            RegisterPageWithVm<MaterialsVM, Materials>();
        }
    }
}

using MedicalLaboratory20.DesktopApp.PageArea.ViewModels;
using MedicalLaboratory20.DesktopApp.PageArea.Views;
using MedicalLaboratory20.DesktopApp.WindowArea.Abstract;

namespace MedicalLaboratory20.DesktopApp.WindowArea.ViewModels
{
    internal abstract class WorkflowVm : BaseWindowVmWithPages
    {
        protected WorkflowVm()
        {
            RegisterPageWithVm<InfoVm, Info>();
        }
    }
}

using MedicalLaboratory20.DesktopApp.PageArea.ViewModels;
using MedicalLaboratory20.DesktopApp.PageArea.Views;
using MedicalLaboratory20.DesktopApp.WindowArea.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalLaboratory20.DesktopApp.WindowArea.ViewModels
{
    class LaborantVM : WorkflowVM
    {
        private TimeSpan _session = new TimeSpan(2, 30, 0);
        private readonly TimeSpan _removeTime = new TimeSpan(0, 0, -1);

        private Timer _timer;

        public LaborantVM()
        {
            RegisterPageWithVm<BiomaterialVM, Biomaterial>();
            RegisterPageWithVm<ReportVM, Report>();

            _timer = new Timer(TimeUpdate, new AutoResetEvent(false), 0, 1000);
        }

        public string TimeToEndSession => _session.ToString("T");

        private void TimeUpdate(object? state)
        {
            _session = _session.Add(_removeTime);
            OnPropertyChanged(nameof(TimeToEndSession));
            if (_session.TotalSeconds < 1)
            {
                _dispatcher.Invoke(() =>
                {
                    _timer.Dispose();
                    OpenNewWindowAndCloseOld(new LoginVM());
                });
            }
        }
    }
}

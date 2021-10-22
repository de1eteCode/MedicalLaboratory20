using MedicalLaboratory20.DesktopApp.PageArea.ViewModels;
using MedicalLaboratory20.DesktopApp.PageArea.Views;
using System;
using System.Threading;

namespace MedicalLaboratory20.DesktopApp.WindowArea.ViewModels
{
    internal class LaborantVm : WorkflowVm
    {
        private TimeSpan _session = new TimeSpan(2, 30, 0);
        private readonly TimeSpan _removeTime = new TimeSpan(0, 0, -1);

        private readonly Timer _timer;

        public LaborantVm()
        {
            RegisterPageWithVm<BiomaterialVm, Biomaterial>();
            RegisterPageWithVm<ReportVm, Report>();

            _timer = new Timer(TimeUpdate, new AutoResetEvent(false), 0, 1000);
        }

        public string TimeToEndSession => _session.ToString("T");

        private void TimeUpdate(object? state)
        {
            _session = _session.Add(_removeTime);
            OnPropertyChanged(nameof(TimeToEndSession));
            if (_session.TotalSeconds < 1)
            {
                Dispatcher.Invoke(() =>
                {
                    _timer.Dispose();
                    OpenNewWindowAndCloseOld(new LoginVm());
                });
            }
        }
    }
}

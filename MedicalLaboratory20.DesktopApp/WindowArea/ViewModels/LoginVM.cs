using MedicalLaboratory20.DesktopApp.Model;
using MedicalLaboratory20.DesktopApp.Models.POCO;
using MedicalLaboratory20.DesktopApp.WindowArea.Abstract;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MedicalLaboratory20.DesktopApp.WindowArea.ViewModels
{
    class LoginVM : BaseWindowVM
    {
        private Authorization _authorization;
        private int _tryLogIn = 0;
        private Captcha _captcha;
        private string _currentCaptcha;

        public LoginVM()
        {
            _authorization = Authorization.GetInstance();
            _captcha = new Captcha();
            _authorization.Error += ShowMsgForUser;

            #region cmd

            LoginCommand = new AsyncRelayCommand<LoginModel>(ExecuteLoginCommand);

            #endregion
        }

        #region Properties
        private int TryLogIn
        {
            get => _tryLogIn;
            set
            {
                _tryLogIn = value;
                if (value > 1)
                {
                    Blocker();
                }
                OnPropertyChanged();
                OnPropertyChanged(nameof(NeedCaptcha));
                OnPropertyChanged(nameof(CaptchaText));
            }
        }

        public bool IsLogIn => _authorization.IsLogin;

        public bool NeedCaptcha
        {
            get
            {
                return TryLogIn > 0;
            }
        }

        public string CaptchaText
        {
            get
            {
                if (NeedCaptcha)
                {
                    _currentCaptcha = _captcha.Captha;
                    return _currentCaptcha;
                }
                return string.Empty;
            }
        }

        public string CaptchaInput { get; set; } = String.Empty;

        public LoginModel LoginModel { get; set; } = new();

        public bool IsUnblocked { get; set; } = true;

        #endregion
        #region Commands
        public ICommand LoginCommand { get; }
        #endregion

        private async Task ExecuteLoginCommand(LoginModel lModel)
        {
            if (NeedCaptcha)
            {
                if (CaptchaInput.Equals(_currentCaptcha) is false)
                {
                    ShowMsgForUser("Неверная капча");
                    OnPropertyChanged(nameof(CaptchaText));
                    CaptchaInput = String.Empty;
                    TryLogIn++;
                    return;
                }
            }

            bool result = await _authorization.Auth(lModel.Login, lModel.Password);
            if (result)
            {
                OpenNewWindowAndCloseOld();
            }
            else
            {
                TryLogIn++;
            }
        }

        private async Task Blocker()
        {
            IsUnblocked = false;
            OnPropertyChanged(nameof(IsUnblocked));
            await Task.Delay(10000);
            IsUnblocked = true;
            OnPropertyChanged(nameof(IsUnblocked));
        }

        private void ShowMsgForUser(string text)
        {
            Task.Run(() =>
            {
                System.Windows.MessageBox.Show(text, "Ошибка", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            });
        }
    }
}

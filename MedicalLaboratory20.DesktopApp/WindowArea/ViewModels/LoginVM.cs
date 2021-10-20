using MedicalLaboratory20.DesktopApp.Models;
using MedicalLaboratory20.DesktopApp.WindowArea.Abstract;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using SharedModels;

namespace MedicalLaboratory20.DesktopApp.WindowArea.ViewModels
{
    class LoginVM : BaseWindowVM
    {
        private Client _client;
        private int _tryLogIn = 0;
        private Captcha _captcha;
        private string _currentCaptcha;

        public LoginVM()
        {
            _client = Client.GetInstance();
            _captcha = new Captcha();
            _client.Error += ShowMsgForUser;

            #region cmd

            LoginCommand = new AsyncRelayCommand<LoginRequest>(ExecuteLoginCommand);

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

        public LoginRequest LoginRequest { get; set; } = new();

        public bool IsUnblocked { get; set; } = true;

        #endregion
        #region Commands
        public ICommand LoginCommand { get; }
        #endregion

        private async Task ExecuteLoginCommand(LoginRequest lModel)
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

            if (string.IsNullOrEmpty(lModel.Login) || string.IsNullOrEmpty(lModel.Password))
            {
                ShowMsgForUser("Не введен логин или пароль");
                return;
            }

            bool result = await _client.Auth(lModel.Login, lModel.Password);
            if (result)
            {
                switch (_client.User.RoleId)
                {
                    case "1":
                        OpenNewWindowAndCloseOld(new LaborantVM());
                        break;
                    case "2":
                        OpenNewWindowAndCloseOld(new LaborantResearcherVM());
                        break;
                    case "3":
                        OpenNewWindowAndCloseOld(new AccountantVM());
                        break;
                    case "4":
                        OpenNewWindowAndCloseOld(new AdminVM());
                        break;

                    default:
                        throw new ArgumentOutOfRangeException("Не найдено значение для роли");
                }
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

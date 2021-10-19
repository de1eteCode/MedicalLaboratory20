﻿using MedicalLaboratory20.DesktopApp.Model;
using MedicalLaboratory20.DesktopApp.Models.POCO;
using MedicalLaboratory20.DesktopApp.WindowArea.Abstract;
using Microsoft.Toolkit.Mvvm.Input;
using System;
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

        private int TryLogIn
        {
            get => _tryLogIn;
            set
            {
                _tryLogIn = value;
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
                return TryLogIn > 1;
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

        public ICommand LoginCommand { get; }


        private async Task ExecuteLoginCommand(LoginModel lModel)
        {
            if (NeedCaptcha)
            {
                if (CaptchaInput.Equals(_currentCaptcha) is false)
                {
                    ShowMsgForUser("Неверная капча");
                    OnPropertyChanged(nameof(CaptchaText));
                    CaptchaInput = String.Empty;
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

        private void ShowMsgForUser(string text)
        {
            System.Windows.MessageBox.Show(text, "Ошибка", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
        }
    }
}
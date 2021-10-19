﻿using MedicalLaboratory20.DesktopApp.Core.Abstract;
using MedicalLaboratory20.DesktopApp.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MedicalLaboratory20.DesktopApp.PageArea.ViewModels
{
    class InfoVM : ObservableObject, IPageVM
    {
        private readonly LoginResult _user;

        public InfoVM()
        {
            _user = Client.GetInstance().User;
            SplitNameFromUserData();
        }

        public string Title => "Информация";
        public string FirstName { get; private set; }
        public string SecondName { get; private set; }
        public string Role => _user.Role;
        public string AvatarUri => @"~\..\..\..\Resource\" + GetUri();

        private string GetUri() =>
            _user.RoleId switch
            {
                "1" => "laborant_1.png",
                "2" => "laborant_2.png",
                "3" => "acountat.png",
                "4" => "admin.png",
                _   => string.Empty
            };

        private void SplitNameFromUserData()
        {
            var fullname = _user.Name;
            if (!string.IsNullOrEmpty(fullname))
            {
                var splited = fullname.Split(' ');
                if (splited.Length > 1)
                {
                    FirstName = splited[0];
                    SecondName = splited[1];
                }
                else if (splited.Length > 0)
                {
                    FirstName = splited[0];
                    SecondName= String.Empty;
                }
                OnPropertyChanged(nameof(FirstName));
                OnPropertyChanged(nameof(SecondName));
            }
            else
            {
                throw new ArgumentException("Not found full name for user");
            }
        }
    }
}

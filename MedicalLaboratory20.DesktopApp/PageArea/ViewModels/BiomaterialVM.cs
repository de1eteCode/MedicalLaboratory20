using MedicalLaboratory20.DesktopApp.PageArea.Abstract;
using Microsoft.Toolkit.Mvvm.Input;
using NetBarcode;
using SharedModels;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Input;

namespace MedicalLaboratory20.DesktopApp.PageArea.ViewModels
{
    class BiomaterialVm : PageVmBase
    {
        private string _customBarcode = string.Empty;
        private BarcodeRecord _barcode = new BarcodeRecord("0", "000000");

        public BiomaterialVm()
        {
            CreateNewCustomBarcodeCommand = new RelayCommand(CreateNewCustomBarcode);
            AddNewPatientCommand = new RelayCommand(AddNewPatient);
            AddNewServiceCommand = new RelayCommand(AddNewService);
            AddServiceCommand = new RelayCommand(AddService);
            RemoveServiceCommand = new RelayCommand(RemoveService);
        }

        #region Properties

        public override string Title => "Биоматериал";
        public string BarcodeDisplay => _barcode.ToString();
        public byte[] BarcodeImage => GenerateBarcode()?.GetByteArray();
        public string CustomBarcode
        {
            get => _customBarcode;
            set
            {
                if (value.Length > 13)
                    return;
                _customBarcode = value;
                OnPropertyChanged();
            }
        }

        #endregion
        #region Commands

        public ICommand CreateNewCustomBarcodeCommand { get; }
        public ICommand AddNewPatientCommand { get; }
        public ICommand AddNewServiceCommand { get; }
        public ICommand AddServiceCommand { get; }
        public ICommand RemoveServiceCommand { get; }

        #endregion
        #region Events

        public override void OnOpen()
        {
            _barcode = new BarcodeRecord();
            OnPropertyChanged(nameof(BarcodeImage));
            OnPropertyChanged(nameof(BarcodeDisplay));
        }

        #endregion

        private Barcode GenerateBarcode()
        {
            return new Barcode(_barcode.ToString(),
                                NetBarcode.Type.EAN13,
                                true,
                                230,
                                100,
                                LabelPosition.BottomCenter,
                                AlignmentPosition.Center,
                                Color.White,
                                Color.Black,
                                new Font("Courier New", 12));
        }

        #region For commands

        private void CreateNewCustomBarcode()
        {
            if (string.IsNullOrEmpty(CustomBarcode) || CustomBarcode.Length != 13)
            {
                MessageBox.Show("Введенный баркод не является действительным",
                                "Ошибка",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return;
            }
            foreach (var symbol in CustomBarcode)
            {
                if (Char.IsDigit(symbol) is false)
                {
                    MessageBox.Show("Введенный баркод не является действительным",
                               "Ошибка",
                               MessageBoxButton.OK,
                               MessageBoxImage.Error);
                    return;
                }
            }

            var unique = CustomBarcode.Substring(0, 1);
            var day = Convert.ToInt32(CustomBarcode.Substring(1, 2));
            var month = Convert.ToInt32(CustomBarcode.Substring(3, 2));
            var year = Convert.ToInt32(CustomBarcode.Substring(4, 2));
            var code = CustomBarcode.Substring(6, 6);

            if (year < 0 || day < 0 || day > 31 || month < 0 || month > 12)
            {
                MessageBox.Show($"Дата {day}.{month}.{year+2000} не существует",
                               "Ошибка",
                               MessageBoxButton.OK,
                               MessageBoxImage.Error);
                return;
            }

            _barcode = new BarcodeRecord(unique, day, month, year, code);
            OnPropertyChanged(nameof(BarcodeDisplay));
            OnPropertyChanged(nameof(BarcodeImage));
        }

        private void AddNewPatient()
        {
            OpenDialog(new WindowArea.ViewModels.Modals.AddPatientVm());
        }

        private void AddNewService()
        {
            OpenDialog(new WindowArea.ViewModels.Modals.AddServiceVm());
        }

        private void AddService()
        {

        }

        private void RemoveService()
        {

        }

        #endregion
    }
}

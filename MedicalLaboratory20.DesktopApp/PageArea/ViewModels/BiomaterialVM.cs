using MedicalLaboratory20.DesktopApp.PageArea.Abstract;
using NetBarcode;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedModels;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;

namespace MedicalLaboratory20.DesktopApp.PageArea.ViewModels
{
    class BiomaterialVM : PageVMBase
    {
        private BarcodeRecord _barcode = new BarcodeRecord("0", "000000");

        public BiomaterialVM()
        {
            CreateNewCustomBarcodeCommand = new RelayCommand(CreateNewCustomBarcode);
            AddNewPatientCommand = new RelayCommand(AddNewPatient);
            AddNewServiceCommand = new RelayCommand(AddNewService);
            AddServiceCommand = new RelayCommand(AddService);
            RemoveServiceCommand = new RelayCommand(RemoveService);
            UpdateListPatientsCommand = new RelayCommand(UpdateListPatients);
        }

        #region Properties

        public override string Title => "Биоматериал";
        public string BarcodeDisplay => _barcode.ToString();
        public byte[] BarcodeImage => GenerateBarcode()?.GetByteArray();

        #endregion
        #region Commands

        public ICommand CreateNewCustomBarcodeCommand { get; }
        public ICommand AddNewPatientCommand { get; }
        public ICommand AddNewServiceCommand { get; }
        public ICommand AddServiceCommand { get; }
        public ICommand RemoveServiceCommand { get; }
        public ICommand UpdateListPatientsCommand { get; }

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

        }

        private void AddNewPatient()
        {

        }

        private void AddNewService()
        {

        }

        private void AddService()
        {

        }

        private void RemoveService()
        {

        }

        private void UpdateListPatients()
        {

        }

        #endregion
    }
}

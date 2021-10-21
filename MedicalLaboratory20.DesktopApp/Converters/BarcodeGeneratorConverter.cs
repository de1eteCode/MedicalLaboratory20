using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Drawing;

namespace MedicalLaboratory20.DesktopApp.Converters
{
    class BarcodeGeneratorConverter : IValueConverter, IDisposable
    {
        private Graphics _graphics;
        private Image _barcode;
        private Brush _brush;

        public BarcodeGeneratorConverter()
        {
            _barcode = new Bitmap(120, 40);
            _brush = new SolidBrush(Color.Black);
            CreateGraphics();
        }

        private void CreateGraphics()
        {
            _graphics = Graphics.FromImage(_barcode);
            _graphics.PageUnit = GraphicsUnit.Millimeter;
        }

        private void PaintLine(int code, float stX, float stY, float endX, float endY)
        {
            _graphics.FillRectangle(_brush, stX, stY, endX - stX, endY - stY);
        }

        private void CreateBarcode(char[] codes)
        {

            foreach (var code in codes)
            {

            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str && string.IsNullOrEmpty(str) is false && str.Length == 13)
            {
                CreateBarcode(str.ToCharArray());
                return _barcode;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _graphics.Dispose();
            _barcode.Dispose();
        }
    }
}

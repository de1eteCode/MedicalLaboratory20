using System;

namespace SharedModels
{
    public class BarcodeRecord
    {
        public string UniqueCode { get; set; }
        public DateTime Time { get; set; }
        public string Code { get; set; }

        public override string ToString()
        {
            return $"{UniqueCode}{Time:ddMMyy}{Code}";
        }

        public BarcodeRecord()
        {
            Random r = new Random();
            Time = DateTime.Now;
            UniqueCode = r.Next(0,10).ToString();
            Code = String.Empty;
            for (int i = 0; i < 6; i++)
            {
                Code += r.Next(0,10).ToString();
            }
        }

        public BarcodeRecord(string uniqueCode, int day, int month, int year, string code)
        {
            UniqueCode = uniqueCode;
            Code = code;
            Time = new DateTime(year, month, day);
        }

        public BarcodeRecord(string uniqueCode, DateTime time, string code)
        {
            UniqueCode = uniqueCode;
            Time = time;
            Code = code;
        }

        public BarcodeRecord(string uniqueCode, string code) : this(uniqueCode, DateTime.Now, code) { }
        
    }
}

using System;
using System.Linq;

namespace MedicalLaboratory20.DesktopApp.Model
{
    class Captcha
    {
        private const string CaptchaSymbols = "1234567890QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm";
        private Random _random;

        public Captcha()
        {
            _random = new Random();
        }

        public string Captha => GenerateCaptha();

        public string GenerateCaptha()
        {
            return new string(Enumerable.Repeat(CaptchaSymbols, 6).Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}

using Microsoft.Extensions.Configuration;

namespace MedicalLaboratory20.DesktopApp.Services
{
    internal class ConfigurationService
    {
        #region Singleton
        private static readonly object _lock = new();
        private static ConfigurationService? _configurationService;
        public static ConfigurationService GetInstance()
        {
            if (_configurationService is null)
            {
                lock (_lock)
                {
                    if (_configurationService is null)
                    {
                        _configurationService = new ConfigurationService();
                    }
                }
            }
            return _configurationService;
        }
        #endregion

        private readonly IConfigurationRoot _confRoot;

        private ConfigurationService()
        {
            _confRoot = new ConfigurationBuilder().AddJsonFile(path: "config.json", optional: false, reloadOnChange: true).Build();
        }

        /// <summary>
        /// Возвращает ip сервера
        /// </summary>
        public string ServerIp => _confRoot["ServerIp"];

        /// <summary>
        /// Возвращает порт сервера
        /// </summary>
        public string ServerPort => _confRoot["ServerPort"];

        /// <summary>
        /// Возвращает полный адрес сервера
        /// </summary>
        public string HttpServerAddress => $"http://{ServerIp}:{ServerPort}";
    }
}

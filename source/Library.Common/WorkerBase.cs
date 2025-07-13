using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Library.Domain.Services;
using System;
using System.IO;
using TDCore.Core.Logging;

namespace Library.Common
{
    public abstract class WorkerBase<T> : BackgroundService
    {
        private readonly ILogger<T> _logger;

        private const string token = "8324590A&*B&*(3@C78((JFUHKJHfsgdiewok|:~][ioKfçskjf#@kkkjftijH&¨";

        public IConfiguration Configuration
        {
            get;
            set;
        }

        public TDCore.Core.Logging.ILog Logger
        {
            get
            {
                return TDCore.DependencyInjection.ObjectFactory.GetSingleton<ILog>();
            }
        }

        /// <summary>
        /// Name: WorkerBase
        /// Description: Starts services.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public WorkerBase(ILogger<T> logger)
        {
            _logger = logger;

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();

            Configuration = configuration;

            var origins = Configuration.GetSection("CORS_Origins").Value;

            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs")))
            {
                // Se não existir, cria o diretório
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "parameters")))
            {
                // Se não existir, cria o diretório
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "parameters"));
            }
            string logFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", $"{AppDomain.CurrentDomain.FriendlyName}.txt");
            string pathResources = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resources");

            InitializationService.InitializeApplication(false, logFile, pathResources, origins, token, false).Wait();

            if (Logger != null)
            {
                Logger.Log(LogType.Information, "error", "Serviço iniciado!");
            }
        }
    }
}

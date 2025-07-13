using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace NN.Watchdog
{
    public class Program
    {
        /// <summary>
        /// Name: Main
        /// Description: program main call
        /// Created by: wazc Programa Novo 2022-09-08
        /// </summary>
        public static void Main(string[] args)
        {
            Console.WriteLine("projeto iniciado");
            CreateHostBuilder(args).Build().Run();
        }

#if DEBUG
        /// <summary>
        /// Name: "CreateHostBuilder" 
        /// Description: method adds a host service of the generic type of "Worker" to debug.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                });

#else
        /// <summary>
        /// Name: "CreateHostBuilder" 
        /// Description: method adds a host service of the generic type of "Worker" to release.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            if (args != null && args.Length == 1 && args[0] == "--windows-service")
            {
                return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                }).UseWindowsService();
            }
            else
            {
                return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                });
            }
        }
#endif
    }
}

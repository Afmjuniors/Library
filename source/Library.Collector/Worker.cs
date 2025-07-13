using Microsoft.Extensions.Logging;
using NN.Checklist.Common;
using NN.Checklist.Domain.Services.Specifications;
using System;
using System.Threading;
using System.Threading.Tasks;
using TDCore.Core.Logging;
using TDCore.DependencyInjection;

namespace NN.Checklist.Collector
{
    public class Worker : WorkerBase<Worker>
    {
        /// <summary>
        /// Name: Worker
        /// Description: class constructor.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public Worker(ILogger<Worker> logger) : base(logger)
        {

        }

        /// <summary>
        /// Name: ExecuteAsync
        /// Description: Starts collector.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            int frequency = int.Parse(Configuration.GetSection("CollectorFrequency").Value);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var service = ObjectFactory.GetSingleton<ICollectorService>();

                    service.CollectAlarms();
                    service.CollectEvents();
                    service.CollectExtremeValues();

                }
                catch (Exception ex)
                {
                    Logger.Log(LogType.Error, ex);
                }
                finally
                {
                    await Task.Delay(frequency * 1000, stoppingToken);
                }
            }
        }
    }
}

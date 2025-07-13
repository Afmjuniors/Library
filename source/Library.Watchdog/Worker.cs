using Microsoft.Extensions.Logging;
using NN.Checklist.Common;
using System;
using System.Threading;
using System.Threading.Tasks;
using TDCore.Core.Logging;


namespace NN.Watchdog
{
    public class Worker : WorkerBase<Worker>
    {
        public Worker(ILogger<Worker> logger) : base(logger)
        {

        }

        /// <summary>
        /// Name: ExecuteAsync
        /// Description: event callback method
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            int frequency = int.Parse(Configuration.GetSection("WatchdogFrequency").Value); // in seconds

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    //var service = ObjectFactory.GetSingleton<IWatchdogService>();

                    //service.LoadAdministrators();
                    //service.LoadWatchdogAlarms();

                    //try
                    //{
                    //    service.CheckDatabaseConnection();
                    //}
                    //catch (Exception ex)
                    //{
                    //    Logger.Log(LogType.Error, ex);
                    //}

                    //try
                    //{
                    //    service.CheckTwilioConnection();
                    //}
                    //catch (Exception ex)
                    //{
                    //    Logger.Log(LogType.Error, ex);
                    //}


                    //try
                    //{
                    //    service.CheckAlarmsConnection();
                    //}
                    //catch (Exception ex)
                    //{
                    //    Logger.Log(LogType.Error, ex);
                    //}

                    //try
                    //{
                    //    service.CheckEventsConnection();
                    //}
                    //catch (Exception ex)
                    //{
                    //    Logger.Log(LogType.Error, ex);
                    //}

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

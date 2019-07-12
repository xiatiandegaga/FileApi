using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using FileApi.Model;
using FileApi.Utility;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FileApi.HostedService
{
    internal class TimedHostedService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly HostedServiceOptions _options;
        public TimedHostedService(IOptions<HostedServiceOptions> options)
        {
            _options = options.Value;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            LogUtility.Info("TimedHostedService is starting.");
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromHours(_options.Period));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            try
            {
                var current = Directory.GetCurrentDirectory();
                var baseDirPhoto = "/Great/Photo/";
                var baseDirVideo = "/Great/Video/";
                var baseDirWord = "/Great/Word/";
                var baseDirOther = "/Great/Other/";
                var now = DateTime.Now;
                var year = now.ToString("yyyy");
                var month = now.ToString("MM");
                var day = now.ToString("dd");

                var tomorrow = now.AddDays(1);
                var tomorrowYear = tomorrow.ToString("yyyy");
                var tomorrowMonth = tomorrow.ToString("MM");
                var tomorrowDay = tomorrow.ToString("dd");

                //新建今明两天的photo文件夹
                if (!Directory.Exists($"{current}{baseDirPhoto}{year}/{month}/{day}"))
                {
                    Directory.CreateDirectory($"{current}{baseDirPhoto}{year}/{month}/{day}");
                }
                if (!Directory.Exists($"{current}{baseDirPhoto}{tomorrowYear}/{tomorrowMonth}/{tomorrowDay}"))
                {
                    Directory.CreateDirectory($"{current}{baseDirPhoto}{tomorrowYear}/{tomorrowMonth}/{tomorrowDay}");
                }

                //新建今明两天的video文件夹
                if (!Directory.Exists($"{current}{baseDirVideo}{year}/{month}/{day}"))
                {
                    Directory.CreateDirectory($"{current}{baseDirVideo}{year}/{month}/{day}");
                }
                if (!Directory.Exists($"{current}{baseDirVideo}{tomorrowYear}/{tomorrowMonth}/{tomorrowDay}"))
                {
                    Directory.CreateDirectory($"{current}{baseDirVideo}{tomorrowYear}/{tomorrowMonth}/{tomorrowDay}");
                }

                //新建今明两天的word文件夹
                if (!Directory.Exists($"{current}{baseDirWord}{year}/{month}/{day}"))
                {
                    Directory.CreateDirectory($"{current}{baseDirWord}{year}/{month}/{day}");
                }
                if (!Directory.Exists($"{current}{baseDirWord}{tomorrowYear}/{tomorrowMonth}/{tomorrowDay}"))
                {
                    Directory.CreateDirectory($"{current}{baseDirWord}{tomorrowYear}/{tomorrowMonth}/{tomorrowDay}");
                }

                //新建今明两天的other文件夹
                if (!Directory.Exists($"{current}{baseDirOther}{year}/{month}/{day}"))
                {
                    Directory.CreateDirectory($"{current}{baseDirOther}{year}/{month}/{day}");
                }
                if (!Directory.Exists($"{current}{baseDirOther}{tomorrowYear}/{tomorrowMonth}/{tomorrowDay}"))
                {
                    Directory.CreateDirectory($"{current}{baseDirOther}{tomorrowYear}/{tomorrowMonth}/{tomorrowDay}");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Exception(ex);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            LogUtility.Info("TimedHostedService is stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}

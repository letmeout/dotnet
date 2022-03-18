using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using SampleMVC.Service;
 
namespace SampleMVC.BackgroundTask
{
    public class DerivedBackgroundPrinter : BackgroundService
    {
        private readonly IServiceProvider services;
 
        public DerivedBackgroundPrinter(IServiceProvider services)
        {
            this.services = services;
        }
 
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = services.CreateScope())
            {
                var worker = 
                    scope.ServiceProvider
                        .GetRequiredService<IWorker>();

                await worker.DoWork(stoppingToken);
            }
        }
    }
}
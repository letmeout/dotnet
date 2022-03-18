using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using SampleMVC.Service;
 
namespace SampleMVC.BackgroundTask
{
    public class BackgroundPrinter : IHostedService
    {
        //private readonly ILogger<BackgroundPrinter> logger;
        private readonly IServiceProvider services;
 
        // public BackgroundPrinter(IServiceProvider services, ILogger<BackgroundPrinter> logger)
        // {
        //     this.logger = logger;
        //     this.services = services;
        // }

        public BackgroundPrinter(IServiceProvider services)
        {
            this.services = services;
        }
 
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = services.CreateScope())
            {
                var worker = 
                    scope.ServiceProvider
                        .GetRequiredService<IWorker>();

                await worker.DoWork(cancellationToken);
            }
        }
 
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            using (var scope = services.CreateScope())
            {
                var scopedProcessingService = 
                    scope.ServiceProvider
                        .GetRequiredService<IScopedProcessingService>();

                await scopedProcessingService.DoWork(cancellationToken);
            }
        }
    }
}
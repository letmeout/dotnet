using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using SampleMVC.Service;
 
namespace SampleMVC.BackgroundTask
{
    public class BackgroundWorker : IHostedService
    {
        private readonly ILogger<BackgroundWorker> logger;
        private readonly IServiceProvider services;
 
        public BackgroundWorker(IServiceProvider services, ILogger<BackgroundWorker> logger)
        {
            this.logger = logger;
            this.services = services;
        }
 
        public Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = services.CreateScope())
            {
                var worker = 
                    scope.ServiceProvider
                        .GetRequiredService<IWorker>();

                worker.DoWork(cancellationToken);
            }

            return Task.CompletedTask;
        }
 
        public Task StopAsync(CancellationToken cancellationToken)
        {
            using (var scope = services.CreateScope())
            {
                var scopedProcessingService = 
                    scope.ServiceProvider
                        .GetRequiredService<IScopedProcessingService>();

                scopedProcessingService.DoWork(cancellationToken);
            }

            return Task.CompletedTask;
        }
    }
}
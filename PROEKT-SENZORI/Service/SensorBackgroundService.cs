using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PROEKT_SENZORI.Controllers;
using System;
using System.Threading;
using System.Threading.Tasks;

public class SensorBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public SensorBackgroundService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var controller = scope.ServiceProvider.GetRequiredService<SensorsController>();
                controller.SaveSensorData();
            }

            await Task.Delay(60000); 
        }
    }
}

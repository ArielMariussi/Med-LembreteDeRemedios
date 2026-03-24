using Med.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Med.Api.Services
{
    public class ResetMedicinesService(IServiceScopeFactory scopeFactory)
    : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                
                var now = DateTime.Now;
                var midnight = now.Date.AddDays(1);
                var delay = midnight - now;

                await Task.Delay(delay, stoppingToken);

                using var scope = scopeFactory.CreateScope();
                var context = scope.ServiceProvider
                    .GetRequiredService<AppDbContext>();

                await context.Medicines
                    .Where(m => m.Taken)
                    .ExecuteUpdateAsync(m =>
                        m.SetProperty(x => x.Taken, false),
                        stoppingToken);
            }
        }
    }
}

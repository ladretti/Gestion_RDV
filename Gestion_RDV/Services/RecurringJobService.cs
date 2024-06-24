using Gestion_RDV.Models.Repository;
using Hangfire;
using Hangfire.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

public class RecurringJobService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public RecurringJobService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();

        // Configure le job récurrent pour appeler SendReminderEmails toutes les minutes
        recurringJobManager.AddOrUpdate(
            "SendReminderEmailsJob",
            Job.FromExpression<IAppointmentService>(service => service.SendReminderEmails()),
            "* * * * *"); // Cron expression pour chaque minute

        await Task.CompletedTask;
    }
}

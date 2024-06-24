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

        recurringJobManager.AddOrUpdate(
            "SendReminderEmailsJob",
            Job.FromExpression<IAppointmentService>(service => service.SendReminderEmails()),
            "00 6 * * *"); // Cron expression (-2h for utc)

        await Task.CompletedTask;
    }
}

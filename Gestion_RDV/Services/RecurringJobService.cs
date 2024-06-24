using Gestion_RDV.Models.Repository;
using Hangfire;
using Hangfire.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

public class RecurringJobService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<RecurringJobService> _logger;

    public RecurringJobService(IServiceProvider serviceProvider, ILogger<RecurringJobService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();

        _logger.LogInformation("Configuring recurring job to run every day.");

        try
        {
            recurringJobManager.AddOrUpdate(
                "SendReminderEmailsJob",
                Job.FromExpression<IAppointmentService>(service => service.SendReminderEmails()),
                "00 6 * * *"); // Cron every days at 8h (-2h for utc)

            _logger.LogInformation("Recurring job configured successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while configuring the recurring job.");
        }

        await Task.CompletedTask;
    }
}

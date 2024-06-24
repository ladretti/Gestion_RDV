using Gestion_RDV.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_RDV.Models.Repository
{
    public interface IAppointmentService
    {
        Task SendReminderEmails();
    }
}

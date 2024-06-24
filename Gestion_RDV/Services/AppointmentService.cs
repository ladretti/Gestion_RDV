using Gestion_RDV.Models.DTO;
using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Abstractions;

namespace Gestion_RDV.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IDataRepositoryRendezVous<RendezVous> _rendezVousRepository;
        private readonly IDataRepository<User> _dataRepositoryUser;
        private readonly IEmailService _emailService;
        private ILogger<AppointmentService> logger;

        public AppointmentService(IDataRepositoryRendezVous<RendezVous> rdvRepo, IEmailService emailService, IDataRepository<User> dataRepositoryUser, ILogger<AppointmentService> log)
        {
            _rendezVousRepository = rdvRepo;
            _emailService = emailService;
            _dataRepositoryUser = dataRepositoryUser;
            logger = log;
        }

        public async Task SendReminderEmails()
        {
            logger.LogInformation("SendReminderEmails started at :" + DateTime.Now);
            var rendezVousToRemind = await _rendezVousRepository.GetRendezvousForTomorrowAsync();
            await _dataRepositoryUser.GetAllAsync();

            if (rendezVousToRemind.Value == null)
            {
                return;
            }

            foreach (var rv in rendezVousToRemind.Value)
            {
                if (rv.User != null)
                {
                    var message = new EmailDTO
                    {
                        To = rv.User.Email,
                        Subject = "Rappel de Rendez-vous",
                        Body = $"Bonjour {rv.User.FirstName},<br/><br/> Ceci est un rappel pour votre rendez-vous le {rv.StartDate:dd/MM/yyyy} à {rv.StartDate:HH:mm}.<br/><br/> Description: {rv.Description}"
                    };

                    await _emailService.SendEmailAsync(message);
                }
            }
        }
    }
}

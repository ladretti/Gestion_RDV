using Gestion_RDV.Models.DTO;

namespace Gestion_RDV.Models.Repository
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailDTO email);
    }
}

using Gestion_RDV.Models.DTO;

namespace Gestion_RDV.Models.DTO
{
    public class NotificationDetailsDTO
    {
        public int NotificationId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public NotificationDetailsRendezVousDTO RendezVous { get; set; }
        public NotificationDetailsOfficeDTO Office { get; set; }
        
    }

    public class NotificationDetailsRendezVousDTO
    {
        public int RendezVousId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TypeRendezVous { get; set; }
        public string Description { get; set; }
    }

    public class NotificationDetailsOfficeDTO
    {
        public int OfficeId { get; set; }
        public NotificationDetailsUserDTO User { get; set; }

    }
    public class NotificationDetailsUserDTO
    {
        public string LastName { get; set; }
    }
}
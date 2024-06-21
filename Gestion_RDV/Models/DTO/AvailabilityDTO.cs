namespace Gestion_RDV.Models.DTO
{
    public class AvailabilityDTO
    {
        public int AvailabilityId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Reserve { get; set; }
    }
    public class AvailabilityPostDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Reserve { get; set; }
        public int OfficeId { get; set; }
    }
}

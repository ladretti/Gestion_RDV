namespace Gestion_RDV.Models.DTO
{
    public class OfficeDetailDTO
    {
        public int OfficeId { get; set; }
        public string Diplome { get; set; }
        public double Rating { get; set; }
        public string DomainePrincipal { get; set; }
        public string Metier { get; set; }
        public string Telephone { get; set; }
        public double PrixPCR { get; set; }
        public DateTime Date { get; set; }
        public string CV { get; set; }
        public string Description { get; set; }
        public string Video { get; set; }
        public OfficeUserDTO User { get; set; }
        public AddressDTO Adresse { get; set; }
        public OfficeRendezVousDTO RendezVous { get; set; }
    }
    public class OfficeRendezVousDTO
    {
        public OfficeReviewDTO Review { get; set; }
    }
    public class OfficeReviewDTO
    {
        public int Note { get; set; }
    }
}

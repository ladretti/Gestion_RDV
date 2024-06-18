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
        public string Video { get; set; }
        public DateTime Date { get; set; }
        public OfficeUserDTO User { get; set; }
    }
}

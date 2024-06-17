namespace Gestion_RDV.Models.DTO
{
    public class OfficeDTO
    {
        public int OfficeId { get; set; }
        public string Diplome { get; set; }
        public double Rating { get; set; }
        public string DomainePrincipal { get; set; }
        public string Metier { get; set; }
        public string Telephone { get; set; }
        public UserDto User { get; set; }
    }
}

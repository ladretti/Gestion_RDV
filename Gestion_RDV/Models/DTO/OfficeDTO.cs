namespace Gestion_RDV.Models.DTO
{
    public class OfficeDTO
    {
        public int OfficeId { get; set; }
        public string DomainePrincipal { get; set; }
        public string Metier { get; set; }
        public OfficeUserDTO User { get; set; }
        public AddressDTO Adresse { get; set; }
    }
    public class OfficeUserDTO
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
    }
    
}

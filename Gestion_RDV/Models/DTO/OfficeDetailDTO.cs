using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestion_RDV.Models.DTO
{
    public class OfficeDetailDTO
    {
        public int OfficeId { get; set; }
        public string Diplome { get; set; }
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
        public double Rating { get; set; }
        public int NbSub { get; set; }
        public IEnumerable<SocialDTO> Socials { get; set; }
    }
    public class SocialDTO
    {
        public int SocialMediaAccountId { get; set; }
        public string Platform { get; set; }
        public string Url { get; set; }
    }
    public class OfficePostDTO
    {
        public string Diplome { get; set; }
        public string DomainePrincipal { get; set; }
        public string Metier { get; set; }
        public string Telephone { get; set; }
        public double PrixPCR { get; set; }
        public DateTime Date { get; set; }
        public string CV { get; set; }
        public string Description { get; set; }
        public string Video { get; set; }
    }
    public class OfficePutDTO : OfficePostDTO
    {
        public int OfficeId { get; set; }
        public int UserId { get; set; }
        public int AdresseId { get; set; }
    }

}

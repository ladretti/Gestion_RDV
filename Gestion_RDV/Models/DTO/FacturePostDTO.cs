using Gestion_RDV.Models.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_RDV.Models.DTO
{
    public class FacturePostDTO
    {
        public decimal PrixAvantTva { get; set; }
        public decimal Tva { get; set; }
        public string Informations { get; set; }
        public int RendezVousId { get; set; }
    }
    public class FactureDTO
    {
        public int FactureId { get; set; }
        public decimal PrixAvantTva { get; set; }
        public decimal Tva { get; set; }
        public string Informations { get; set; }
        public RendezVousDTO? RendezVous { get; set; }
    }
}

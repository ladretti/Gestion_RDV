using Gestion_RDV.Models.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_RDV.Models.DTO
{
    public class OfficeEquipmentPostDTO
    {
        public int EquipmentId { get; set; }
        public int OfficeId { get; set; }
        public bool Etat { get; set; }
        public DateTime LastUpdate { get; set; } = DateTime.Now;
        public DateTime FutureUpdate { get; set; } = DateTime.Now;
    }
    public class OfficeEquipmentDTO : OfficeEquipmentPostDTO
    {
        public EquipmentDTO? Equipment { get; set; }
        public bool Etat { get; set; }
        public DateTime LastUpdate { get; set; } = DateTime.Now;
        public DateTime FutureUpdate { get; set; } = DateTime.Now;
    }
    public class EquipmentDTO
    {
        public int EquipmentId { get; set; }
        public string Name { get; set; }
    }
    public class EquipmentPostDTO
    {
        public string Name { get; set; }
    }

}

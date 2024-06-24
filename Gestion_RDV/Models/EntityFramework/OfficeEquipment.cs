using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestion_RDV.Models.EntityFramework
{
    [Table("t_j_office_equipment_ofe")]
    public class OfficeEquipment
    {
        [Column("ofe_state")]
        public bool Etat { get; set; }

        [Column("ofe_last_update")]
        public DateTime LastUpdate { get; set; } = DateTime.Now;
        [Column("ofe_future_update")]
        public DateTime FutureUpdate { get; set; } = DateTime.Now;

        // Foreign Key
        [Column("equ_id")]
        public int EquipmentId { get; set; }

        [Column("ofc_id")]
        public int OfficeId { get; set; }

        // Navigation property
        [ForeignKey("EquipmentId"), InverseProperty("OfficeEquipments")]
        public Equipment? Equipment { get; set; }

        [ForeignKey("OfficeId"), InverseProperty("OfficeEquipments")]
        public Office? Office { get; set; }


    }
}

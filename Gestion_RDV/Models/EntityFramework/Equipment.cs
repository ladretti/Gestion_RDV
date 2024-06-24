using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestion_RDV.Models.EntityFramework
{
    [Table("t_e_equipment_equ")]
    public class Equipment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("equ_id")]
        public int EquipmentId { get; set; }

        [Column("equ_name"), Required]
        public string Name { get; set; }


        // Foreign Key

        // Navigation property
        [InverseProperty("Equipment")]
        public ICollection<OfficeEquipment>? OfficeEquipments { get; set; }

    }
}

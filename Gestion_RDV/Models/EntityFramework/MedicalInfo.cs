using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestion_RDV.Models.EntityFramework
{
    [Table("t_e_medicalinfo_mif")]
    public class MedicalInfo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("mif_id")]
        public int MedicalInfoId { get; set; }

        [Column("mif_category")]
        public FileType Category { get; set; }

        [Column("mif_description")]
        public string Description { get; set; }

        //Foreign Key
        [Column("usr_id")]
        public int UserId { get; set; }

        // Navigation property
        [ForeignKey("UserId"), InverseProperty("MedicalInfos")]
        public User User { get; set; }
    }
    public enum FileType
    {
        [Display(Name = "Problèmes médicaux")]
        General,

        [Display(Name = "Chirurgie")]
        Chirurgical,

        [Display(Name = "Allergies")]
        Allergies,

        [Display(Name = "Autre")]
        Autre,
    }
}

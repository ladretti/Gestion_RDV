using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestion_RDV.Models.EntityFramework
{
    [Table("t_e_role")]
    public class Review
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("role_id")]
        public int RoleId { get; set; }

        [Required, Column("libelle"), StringLength(50)]
        public string Description { get; set; }
    }
}

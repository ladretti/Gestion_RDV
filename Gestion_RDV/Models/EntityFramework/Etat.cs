using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_RDV.Models.EntityFramework
{
    [Table("t_e_state_stt")]
    public class Etat
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("stt_id")]
        public int Id { get; set; }

        [Column("stt_name"), Required]
        public String Name { get; set; }

    }
}

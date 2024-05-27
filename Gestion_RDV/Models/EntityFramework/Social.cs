using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestion_RDV.Models.EntityFramework
{
    [Table("t_social")]
    public class Social
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public int Id { get; set; }

        [Column("youtube"), StringLength(255)]
        public string Youtube { get; set; }

        [Column("twitter"), StringLength(255)]
        public string Twitter { get; set; }

        [Column("facebook"), StringLength(255)]
        public string Facebook { get; set; }

        [Column("linkedin"), StringLength(255)]
        public string Linkedin { get; set; }

        [Column("instagram"), StringLength(255)]
        public string Instagram { get; set; }

        public virtual Profile Profile { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestion_RDV.Models.EntityFramework
{
    [Table("t_e_social_scl")]
    public class Social
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("scl_id")]
        public int SocialId { get; set; }

        [Column("scl_youtube"), StringLength(255)]
        public string Youtube { get; set; }

        [Column("scl_twitter"), StringLength(255)]
        public string Twitter { get; set; }

        [Column("scl_facebook"), StringLength(255)]
        public string Facebook { get; set; }

        [Column("scl_linkedin"), StringLength(255)]
        public string Linkedin { get; set; }

        [Column("scl_instagram"), StringLength(255)]
        public string Instagram { get; set; }

    }
}

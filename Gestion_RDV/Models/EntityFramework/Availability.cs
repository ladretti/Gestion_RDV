using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestion_RDV.Models.EntityFramework
{
    [Table("t_e_availability_avb")]
    public class Availability
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("avb_id")]
        public int AvailabilityId { get; set; }

        [Column("avb_start_date")]
        public DateTime StartDate { get; set; }

        [Column("avb_end_date")]
        public DateTime EndDate { get; set; }

    }
}

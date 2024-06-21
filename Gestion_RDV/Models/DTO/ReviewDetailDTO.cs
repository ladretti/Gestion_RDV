namespace Gestion_RDV.Models.DTO
{
    public class ReviewDetailDTO
    {
        public int ReviewId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public int Note { get; set; }
    }
}

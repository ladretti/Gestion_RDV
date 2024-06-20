namespace Gestion_RDV.Models.DTO
{
    public class ReviewDTO
    {
        public int ReviewId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Type { get; set; }
        public int Note { get; set; }
        public ReviewRendezVousDTO RendezVous { get; set; }
        public IEnumerable<CommentDTO> Comments { get; set; }
    }
    public class ReviewRendezVousDTO
    {
        public OfficeUserDTO User { get; set; }
    }
    public class CommentDTO
    {
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public OfficeUserDTO User { get; set; }
    }
}

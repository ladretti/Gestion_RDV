using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_RDV.Models.DTO
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public int ReviewId { get; set; }
    }
    public class CommentReviewDTO : CommentDTO
    {
        public OfficeUserDTO User { get; set; }
    }
    public class CommentPostDTO
    {
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public int ReviewId { get; set; }
    }
}

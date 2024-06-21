using Gestion_RDV.Models.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_RDV.Models.DTO
{
    public class PostDTO
    {
        public int PostId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; } 
        public string Type { get; set; }
        public OfficeUserDTO User { get; set; }
        public int TotalReplies { get; set; }
        public ICollection<PostDTO> ChildPosts { get; set; }
        public int NbLike { get; set; }
    }
    public class PostDetailDTO
    {
        public int PostId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; } 
        public string Type { get; set; }
        public OfficeUserDTO User { get; set; }
        public ICollection<PostDTO> ChildPosts { get; set; }
        public int NbLike { get; set; }
    }
    public class PostPostDTO
    {
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public int UserId { get; set; }
        public int? ParentPostId { get; set; }
    }

}

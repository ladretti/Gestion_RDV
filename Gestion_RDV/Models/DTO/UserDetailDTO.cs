using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Gestion_RDV.Models.EntityFramework;

namespace Gestion_RDV.Models.DTO
{
    public class UserDetailDTO
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateOnly BirthDate { get; set; }
        public bool Activated { get; set; }
        public string Avatar { get; set; }
        public UserRole Role { get; set; }
    }
}

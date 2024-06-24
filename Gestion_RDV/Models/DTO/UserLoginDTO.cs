using Gestion_RDV.Models.EntityFramework;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_RDV.Models.DTO
{
    public class UserLoginDTO
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        [Column("usr_activated")]
        public bool Activated { get; set; }
        public UserRole Role { get; set; }
    }
    public class PractitionerLoginDTO : UserLoginDTO
    {
        public OfficeSignInDTO Office { get; set; }
    }
    public class OfficeSignInDTO {
        public int OfficeId { get; set; }
    }

}

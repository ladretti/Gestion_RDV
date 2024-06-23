using Gestion_RDV.Models.EntityFramework;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

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

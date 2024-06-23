using Gestion_RDV.Models.EntityFramework;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;


namespace Gestion_RDV.Models.DTO
{
    public class UserSignInDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public DateOnly BirthDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sexe { get; set; }
        public UserRole Role { get; set; }
    }
}

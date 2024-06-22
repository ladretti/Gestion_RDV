namespace Gestion_RDV.Models.DTO
{
    public class SocialDTO
    {
        public int SocialMediaAccountId { get; set; }
        public string Platform { get; set; }
        public string Url { get; set; }
    }
    public class SocialPostDTO
    {
        public string Platform { get; set; }
        public string Url { get; set; }
        public int OfficeId { get; set; }
    }
}

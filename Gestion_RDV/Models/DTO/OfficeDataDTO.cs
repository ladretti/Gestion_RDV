namespace Gestion_RDV.Models.DTO
{
    public class OfficeStatsDTO
    {
        public int OfficeId { get; set; }
        public int NbSub { get; set; }
        public int NbRdvPasse { get; set; }
        public int NbRdvAVenir { get; set; }
        public int TotalReviews { get; set; }
        public double AverageReviewNote { get; set; }
       // public int TotalLikes { get; set; }
        //public int TotalDislikes { get; set; }
        public int NbFollowers { get; set; }
    }
}

namespace LocalizeR.Core.DTO
{
    public class RatingStatisticsDTO
    {
        public double AverageRating { get; set; }
        public int Count1 { get; set; }
        public int Count2 { get; set; }
        public int Count3 { get; set; }
        public int Count4 { get; set; }
        public int Count5 { get; set; }

        public Guid ServiceId { get; set; }
    }
}

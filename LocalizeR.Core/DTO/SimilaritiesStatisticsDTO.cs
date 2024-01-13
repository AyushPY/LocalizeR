namespace LocalizeR.Core.DTO
{
    public class SimilaritiesStatisticsDTO
    {
        public double Similarity { get; set; } = double.MinValue;
        public Guid ServiceId { get; set; }
    }
}

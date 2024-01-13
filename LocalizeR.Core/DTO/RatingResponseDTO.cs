using LocalizeR.Core.Identity;

namespace LocalizeR.Core.DTO
{
    public class RatingResponseDTO
    {
        public List<SimilaritiesStatisticsDTO> AllSimilarities { get; set; } = new List<SimilaritiesStatisticsDTO>();
        public List<RatingStatisticsDTO> RatingStats { get; set; } = new List<RatingStatisticsDTO>();

        public List<ApplicationUser> serviceProvider { get; set; }

    }
}
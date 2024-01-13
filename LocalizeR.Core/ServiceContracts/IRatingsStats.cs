using LocalizeR.Core.DTO;

namespace LocalizeR.Core.ServiceContracts
{
    public interface IRatingsStats
    {
        Task<List<RatingStatisticsDTO>> CalculateRatingStatisticsAsync(List<(List<double> Y, Guid ServiceId)> otherUsers);

    }
}

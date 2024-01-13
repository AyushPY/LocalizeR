using LocalizeR.Core.DTO;

namespace LocalizeR.Core.ServiceContracts
{
    public interface ISimilarityCalculator
    {
        Task<List<SimilaritiesStatisticsDTO>> CalculateSimilarity(List<double> ratedUser, List<(List<double> Y, Guid ServiceId)> otherUsers);
    }

}

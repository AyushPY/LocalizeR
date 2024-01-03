namespace LocalizeR.Core.ServiceContracts
{
    public interface ISimilarityCalculator
    {
        Task<List<(double Similarity, Guid ServiceId)>> CalculateSimilarity(List<double> ratedUser, List<(List<List<double>> Y, Guid ServiceId)> otherUsers);
    }

}

using LocalizeR.Core.ServiceContracts;

namespace LocalizeR.Core.Services
{
    public class SimilarityCalculator : ISimilarityCalculator
    {

        public async Task<List<(double Similarity, Guid ServiceId)>> CalculateSimilarity(List<double> ratedUser, List<(List<List<double>> Y, Guid ServiceId)> otherUsers)
        {
            List<(double Similarity, Guid ServiceId)> similarityResults = new List<(double Similarity, Guid ServiceId)>();
            HashSet<(List<List<double>> YValues, Guid ServiceId)> processedCombos = new HashSet<(List<List<double>> YValues, Guid ServiceId)>();

            foreach (var (YValues, serviceId) in otherUsers)
            {
                var combo = (YValues, serviceId);

                if (processedCombos.Contains(combo))
                {
                    continue; // Skip if similarity for this combo has already been calculated
                }

                double totalSimilarity = 0.0;

                foreach (var Y in YValues)
                {
                    totalSimilarity += CalculatePearsonCorrelation(ratedUser, Y);
                }

                similarityResults.Add((totalSimilarity, serviceId));
                processedCombos.Add(combo);
            }

            similarityResults = similarityResults.OrderBy(result => result.Similarity).ToList();

            return similarityResults;
        }


        private double CalculatePearsonCorrelation(List<double> x, List<double> y)
        {
            int maxLength = Math.Max(x.Count, y.Count);

            // Pad the shorter list with zeros
            while (x.Count < maxLength)
            {
                x.Add(0);
            }

            while (y.Count < maxLength)
            {
                y.Add(0);
            }
            int n = x.Count;

            // Calculate the mean of x and y
            double meanX = x.Average();
            double meanY = y.Average();

            // Calculate the covariance and variances
            double covariance = 0;
            double varianceX = 0;
            double varianceY = 0;

            for (int i = 0; i < n; i++)
            {
                double deviationX = x[i] - meanX;
                double deviationY = y[i] - meanY;

                covariance += deviationX * deviationY;
                varianceX += deviationX * deviationX;
                varianceY += deviationY * deviationY;
            }

            // Avoid division by zero
            if (varianceX == 0 || varianceY == 0)
            {
                return 0;
            }

            // Calculate Pearson Correlation Coefficient
            double correlation = covariance / Math.Sqrt(varianceX * varianceY);

            return correlation;
        }

    }
}

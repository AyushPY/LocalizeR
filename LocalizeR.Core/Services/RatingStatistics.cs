using LocalizeR.Core.DTO;
using LocalizeR.Core.ServiceContracts;

namespace LocalizeR.Core.Services
{
    public class RatingStatistics : IRatingsStats
    {
        public async Task<List<RatingStatisticsDTO>> CalculateRatingStatisticsAsync(List<(List<double> Y, Guid ServiceId)> otherUsers)
        {
            List<RatingStatisticsDTO> ratingStatisticsList = new List<RatingStatisticsDTO>();
            RatingStatisticsDTO ratingStatistics = null;

            foreach (var (Y, serviceId) in otherUsers)
            {
                double totalRating = 0;
                double average = 0;
                int totalRatings = 0;
                int count1 = 0, count2 = 0, count3 = 0, count4 = 0, count5 = 0;


                ratingStatistics = new RatingStatisticsDTO();
                count1 += Y.Count(r => r == 1);
                count2 += Y.Count(r => r == 2);
                count3 += Y.Count(r => r == 3);
                count4 += Y.Count(r => r == 4);
                count5 += Y.Count(r => r == 5);
                average = Y.Average();
                ratingStatistics.Count1 = count1;
                ratingStatistics.Count2 = count2;
                ratingStatistics.Count3 = count3;
                ratingStatistics.Count4 = count4;
                ratingStatistics.Count5 = count5;
                ratingStatistics.AverageRating = average;
                ratingStatistics.ServiceId = serviceId;
                ratingStatisticsList.Add(ratingStatistics);

            }
            return ratingStatisticsList;
        }

    }
}


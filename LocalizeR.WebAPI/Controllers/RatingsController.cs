using LocalizeR.Core.DTO;
using LocalizeR.Core.Entities;
using LocalizeR.Core.Identity;
using LocalizeR.Core.ServiceContracts;
using LocalizeR.Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace LocalizeR.WebAPI.Controllers
{
    [AllowAnonymous]
    public class RatingsController : CustomControllerBase
    {
        private readonly ISimilarityCalculator _similarityCalculator;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RatingsController> _logger;
        private readonly IRatingsStats _ratngStats;
        public RatingsController(ISimilarityCalculator similarityCalculator, ApplicationDbContext context, ILogger<RatingsController> logger, IRatingsStats ratngStats)
        {
            _similarityCalculator = similarityCalculator;
            _context = context;
            _logger = logger;
            _ratngStats = ratngStats;
        }
        [HttpPost("PearsonSimilarityCalculation")]
        public async Task<IActionResult> PearsonSimilarityCalculation(RatingDTO ratingDTO)
        {
            if (ModelState.IsValid == false)
            {
                string errorMessage = string.Join("|", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }
            Rating ratingData = new Rating()
            {
                Value = ratingDTO.ratingvalue,
                ServiceProviderId = ratingDTO.serviceid


            };
            string location = ratingDTO.servicelocation;
            int result = 0;
            if (location == null)
            {
                return Problem("Unable to Find Dedicated Service Profile");

            }
            else
            {
                await _context.RatingValues.AddAsync(ratingData);
                result = await _context.SaveChangesAsync();
            }
            List<SimilaritiesStatisticsDTO> allSimilarities = new List<SimilaritiesStatisticsDTO>();
            List<RatingStatisticsDTO> allStatistics = new List<RatingStatisticsDTO>();
            if (result > 0)
            {
                if (location == null)
                {
                    return Problem("Unable to Find Dedicated Service Profile");
                }
                else
                {
                    // Retrieve all values associated with the specific ServiceProviderId
                    _logger.LogInformation("About to debug Rating Values");
                    List<double> ratingValues = await _context.RatingValues
                        .Where(r => r.ServiceProviderId == ratingDTO.serviceid)
                        .Select(r => r.Value)
                        .ToListAsync().ConfigureAwait(false);
                    _logger.LogDebug("Rating Values: {RatingValues}", string.Join(", ", ratingValues));


                    if (ratingValues == null)
                    {
                        return Problem("Service Provider Ratings Not Found");
                    }
                    else
                    {
                        var serviceProviderRoleId = Guid.Parse("29DDECD3-E8F2-48EB-CED8-08DBF5A39C92");
                        _logger.LogInformation("About to check Matching Service Ids");

                        List<Guid> matchingServiceIds = await _context.Users
                            .Join(_context.UserRoles,
                                user => user.Id,
                                userRole => userRole.UserId,
                                (user, userRole) => new { user.Id, user.Location, userRole.RoleId })
                            .Where(joinResult => joinResult.Location == location && joinResult.RoleId == serviceProviderRoleId)
                            .Select(joinResult => joinResult.Id)
                            .ToListAsync();
                        _logger.LogInformation("MatchingServiceIds: {matchingServiceIds}", matchingServiceIds);

                        List<List<double>> valuestoCalculate = new List<List<double>>();
                        if (matchingServiceIds == null)
                        {
                            return Problem("No Services Found Under This Location");
                        }
                        else
                        {
                            matchingServiceIds = matchingServiceIds.Where(item => item != ratingDTO.serviceid).ToList();
                        }

                        foreach (var serviceId in matchingServiceIds)
                        {
                            List<double> ratingsForService = await _context.RatingValues
                               .Where(r => r.ServiceProviderId == serviceId)
                          .Select(r => r.Value)
                              .ToListAsync();
                            if (ratingsForService == null)
                            {
                                return Problem("Error while collecting Rating values");
                            }

                            if (valuestoCalculate == null)
                            {
                                return Problem("No Associated Values Found");
                            }
                            var similarities = await _similarityCalculator.CalculateSimilarity(ratingValues, new List<(List<double> Values, Guid ServiceId)> { (ratingsForService, serviceId) });
                            var ratingStats = await _ratngStats.CalculateRatingStatisticsAsync(new List<(List<double> Values, Guid ServiceId)> { (ratingsForService, serviceId) });
                            allSimilarities.AddRange(similarities);
                            allStatistics.AddRange(ratingStats);
                        }


                    }
                    var serviceProviderIds = allSimilarities.Select(s => s.ServiceId).ToList();
                    List<ApplicationUser> filteredUsers = _context.Users.Where(user => serviceProviderIds.Contains(user.Id)).ToList();
                    if (allSimilarities != null && allStatistics != null)
                    {
                        var responseDTO = new RatingResponseDTO
                        {
                            AllSimilarities = allSimilarities,
                            RatingStats = allStatistics,
                            serviceProvider = filteredUsers
                        };

                        return Ok(responseDTO);
                    }

                }
            }

            return Problem("Process Failed");
        }
    }
}
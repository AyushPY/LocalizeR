using LocalizeR.Core.ServiceContracts;
using RepositoryContracts;
using RepositoryContracts.Models;

namespace LocalizeR.Core.Services
{
    public class JobSequencer : IJobSequencer
    {
        private readonly IUserRequestsRepository _requestUsers;
        public JobSequencer(IUserRequestsRepository requestUsers)
        {
            _requestUsers = requestUsers;
        }
        public async Task<List<BudgetDTO>> SequenceJobs(Guid serviceId)
        {
            var input = new List<BudgetDTO>();
            input = await _requestUsers.GetUserRequestsByServiceID(serviceId);

            // Perform job sequencing logic
            var optimizedResult = new List<BudgetDTO>();

            // Sort input based on both budget (ascending) and deadline (ascending)
            var sortedInput = input.OrderBy(r => r.budget).ThenBy(r => r.deadline).ToList();

            // Perform job sequencing logic (a simple example)
            var completionTimes = new List<DateTime>();

            foreach (var request in sortedInput)
            {
                var earliestTimeSlot = FindEarliestTimeSlot((DateTime)request.deadline, completionTimes);

                // Perform any additional sequencing logic here

                // Update completion times
                completionTimes.Add(earliestTimeSlot.AddHours(1)); // Assuming each job takes 1 hour

                // Create optimized BudgetDTO
                var optimizedItem = new BudgetDTO
                {
                    budget = request.budget,
                    deadline = request.deadline,
                    requesterID = request.requesterID,
                    severity = request.severity,
                    requesterUsername = request.requesterUsername
                };

                optimizedResult.Add(optimizedItem);
            }

            return optimizedResult;

        }
        private DateTime FindEarliestTimeSlot(DateTime deadline, List<DateTime> completionTimes)
        {
            // Find the earliest time slot after the completion time of the last scheduled job
            var earliestTimeSlot = completionTimes.Count > 0
                ? completionTimes.Max()
                : DateTime.Now; // Use current time if no jobs have been scheduled yet

            // Ensure the deadline is met
            return earliestTimeSlot > deadline ? earliestTimeSlot : deadline;
        }
    }
}

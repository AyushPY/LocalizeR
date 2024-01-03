using LocalizeR.Core.Models;
using LocalizeR.Core.ServiceContracts;

namespace LocalizeR.Core.Services
{
    public class JobSequencer : IJobSequencer
    {
        public async Task<List<(BudgetDeadlinePair Job, Guid RequesterId)>> SequenceJobs(List<BudgetDeadlinePair> input)
        {
            var sortedInput = input.OrderByDescending(item => item.budget)
                           .ThenBy(item => item.deadline)
                           .ToList();

            // Initialize variables to track scheduled jobs and their completion times
            var scheduledJobs = new List<(BudgetDeadlinePair Job, Guid RequesterId)>();
            var completionTimes = new List<DateTime>();

            foreach (var job in sortedInput)
            {
                // Find the earliest available time slot for the job
                var earliestTimeSlot = FindEarliestTimeSlot((DateTime)job.deadline, completionTimes);

                // Schedule the job and update the completion time
                scheduledJobs.Add((new BudgetDeadlinePair
                {
                    budget = job.budget,
                    deadline = job.deadline,
                    requesterID = job.requesterID
                }, job.requesterID));

                completionTimes.Add(earliestTimeSlot);
            }

            return scheduledJobs;
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

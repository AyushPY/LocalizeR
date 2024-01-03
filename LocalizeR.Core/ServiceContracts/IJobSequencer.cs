using LocalizeR.Core.Models;

namespace LocalizeR.Core.ServiceContracts
{
    public interface IJobSequencer
    {
        Task<List<(BudgetDeadlinePair Job, Guid RequesterId)>> SequenceJobs(List<BudgetDeadlinePair> input);
    }
}

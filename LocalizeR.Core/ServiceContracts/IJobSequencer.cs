using RepositoryContracts.Models;

namespace LocalizeR.Core.ServiceContracts
{
    public interface IJobSequencer
    {
        Task<List<BudgetDTO>> SequenceJobs(Guid serviceId);
    }
}

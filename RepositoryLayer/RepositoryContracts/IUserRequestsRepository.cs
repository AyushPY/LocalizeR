using LocalizeR.Core.Models;

namespace RepositoryLayer.RepositoryContracts
{
    public interface IUserRequestsRepository
    {
        Task<List<BudgetDeadlinePair>> GetUserRequestsByServiceID(Guid serviceid);
    }
}

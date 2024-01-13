using RepositoryContracts.Models;

namespace RepositoryContracts
{
    public interface IUserRequestsRepository
    {
        Task<List<BudgetDTO>> GetUserRequestsByServiceID(Guid serviceid);
    }
}

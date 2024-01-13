namespace RepositoryLayers.RepositoryContracts
{
    public interface IUserRequestsRepository
    {
        Task<List<BudgetDeadlinePair>> GetUserRequestsByServiceID(Guid serviceid);
    }
}

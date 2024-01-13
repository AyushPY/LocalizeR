using RepositoryContracts.Models;

namespace RepositoryContracts
{
    public interface IServiceProviderInfo
    {
        Task<List<ServiceProviderInfo>> GetAllServices(Guid roleId);
    }
}

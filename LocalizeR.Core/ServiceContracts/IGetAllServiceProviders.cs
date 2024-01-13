using RepositoryContracts.Models;

namespace LocalizeR.Core.ServiceContracts
{
    public interface IGetAllServiceProviders
    {
        Task<List<ServiceProviderInfo>> GetServiceProviders(Guid roleId);
    }
}

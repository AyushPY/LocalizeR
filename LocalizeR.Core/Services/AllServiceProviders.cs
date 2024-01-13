using LocalizeR.Core.ServiceContracts;
using RepositoryContracts;
using RepositoryContracts.Models;

namespace LocalizeR.Core.Services
{
    public class AllServiceProviders : IGetAllServiceProviders
    {
        private readonly IServiceProviderInfo _serviceProviderInfo;
        public AllServiceProviders(IServiceProviderInfo serviceProviderInfo)
        {
            _serviceProviderInfo = serviceProviderInfo;
        }
        public async Task<List<ServiceProviderInfo>> GetServiceProviders(Guid roleId)
        {
            var AllInput = new List<ServiceProviderInfo>();
            AllInput = await _serviceProviderInfo.GetAllServices(roleId);
            return AllInput;
        }
    }
}

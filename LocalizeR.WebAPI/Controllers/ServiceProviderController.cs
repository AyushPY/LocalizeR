using LocalizeR.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace LocalizeR.WebAPI.Controllers
{
    [AllowAnonymous]
    public class ServiceProviderController : CustomControllerBase
    {
        private readonly IJobSequencer _jobSequencer;
        private readonly IServiceProviderInfo _serviceProviderInfo;
        public ServiceProviderController(IJobSequencer jobSequencer, IServiceProviderInfo getAllServiceProviderInfo)
        {
            _jobSequencer = jobSequencer;
            _serviceProviderInfo = getAllServiceProviderInfo;
        }
        [HttpGet("GetListOfUsers")]
        public async Task<IActionResult> GetListOfUsers(Guid serviceId)
        {
            var UsersList = await _jobSequencer.SequenceJobs(serviceId);
            return Ok(UsersList);
        }
        [HttpGet("GetAllServiceProviders")]
        public async Task<IActionResult> GetAllServiceProviders()
        {

            var AllServiceProviders = await _serviceProviderInfo.GetAllServices(Guid.Parse("29DDECD3-E8F2-48EB-CED8-08DBF5A39C92"));
            return Ok(AllServiceProviders);
        }
    }
}

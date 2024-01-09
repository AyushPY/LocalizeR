using LocalizeR.Core.DTO;
using LocalizeR.Core.Identity;

namespace LocalizeR.Core.ServiceContracts
{
    public interface IJwtService
    {
        AuthenticationResponse CreateJwtToken(ApplicationUser user);
    }
}

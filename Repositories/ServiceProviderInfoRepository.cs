using LocalizeR.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using RepositoryContracts.Models;

namespace Repositories
{
    public class ServiceProviderInfoRepository : IServiceProviderInfo
    {
        private readonly ApplicationDbContext _context;
        public ServiceProviderInfoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<ServiceProviderInfo>> GetAllServices(Guid roleId)
        {
            var userIds = await _context.UserRoles
            .Where(ur => ur.RoleId == roleId)
            .Select(ur => ur.UserId)
            .ToListAsync();

            var users = await _context.Users
            .Where(u => userIds.Contains(u.Id)).Select(u => new ServiceProviderInfo
            {
                Id = u.Id,
                BusinessName = u.BusinessName,
                Location = u.Location,
                ServiceType = u.ServiceType,
                UserName = u.UserName,
                ContactNo = u.PhoneNumber

            })
            .ToListAsync();

            return users;
        }
    }
}

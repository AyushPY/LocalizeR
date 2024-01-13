using LocalizeR.Core.Models;
using LocalizeR.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.RepositoryContracts;

namespace RepositoryLayer.RepositoryServices
{
    public class UserRequestsRepository : IUserRequestsRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRequestsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<BudgetDeadlinePair>> GetUserRequestsByServiceID(Guid serviceid)
        {
            var recordsForServiceId = await _context.RequestRecords
             .Where(r => r.ServiceId == serviceid)
             .Select(r => new BudgetDeadlinePair { budget = r.budget, deadline = r.deadline, requesterID = r.RequesterId, requesterUsername = r.RequesterProfile.UserName, severity = r.Severity })
             .ToListAsync();
            return recordsForServiceId;
        }
    }
}

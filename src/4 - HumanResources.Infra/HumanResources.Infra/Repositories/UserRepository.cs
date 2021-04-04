using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HumanResources.Domain.Entities;
using HumanResources.Infra.Context;
using HumanResources.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.Infra.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly HumanResourcesContext _context;
        public UserRepository(HumanResourcesContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users.Where(x => x.Email.ToLower() == email.ToLower()).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<List<User>> SearchByEmail(string email)
        {
            return await _context.Users.Where(x => x.Email.ToLower().Contains(email.ToLower())).AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<User>> SearchByName(string name)
        {
            return await _context.Users.Where(x => x.Name.ToLower().Contains(name.ToLower())).AsNoTracking()
                .ToListAsync();
        }
    }
}

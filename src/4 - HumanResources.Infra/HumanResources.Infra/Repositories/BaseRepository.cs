using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HumanResources.Domain.Entities;
using HumanResources.Infra.Context;
using HumanResources.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.Infra.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Base
    {
        private readonly HumanResourcesContext _context;

        public BaseRepository(HumanResourcesContext context)
        {
            _context = context;
        }
        public virtual async Task<T> Create(T obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();

            return obj;
        }

        public virtual async Task<T> Update(T obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return obj;
        }

        public virtual async Task Remove(long id)
        {
            var obj = await Get(id);

            if (obj != null)
            {
                _context.Remove(obj);
                await _context.SaveChangesAsync();
            }

        }

        public virtual async Task<T> Get(long id)
        {
            var obj = await _context.Set<T>()
                                                    .AsNoTracking()
                                                    .Where(x => x.Id == id)
                                                    .FirstOrDefaultAsync();
            return obj;
        }

        public virtual async Task<List<T>> Get()
        {
            var obj = await _context.Set<T>()
                .AsNoTracking()
                .ToListAsync();

            return obj;
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using HumanResources.Domain.Entities;

namespace HumanResources.Infra.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByEmail(string email);
        Task<List<User>> SearchByEmail(string email);
        Task<List<User>> SearchByName(string name);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using HumanResources.Services.DTO;

namespace HumanResources.Services.Interfaces
{
    public interface IUserService : IBaseService<UserDTO>
    {
        Task<List<UserDTO>> SearchByName(string name);
        Task<List<UserDTO>> SearchByEmail(string email);
        Task<UserDTO> GetByEmail(string email);
    }
}

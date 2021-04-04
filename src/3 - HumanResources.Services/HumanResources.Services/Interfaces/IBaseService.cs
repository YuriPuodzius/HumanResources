using System.Collections.Generic;
using System.Threading.Tasks;

namespace HumanResources.Services.Interfaces
{
    public interface IBaseService<T>
    {
        Task<T> Create(T obj);
        Task<T> Update(T obj);
        Task Remove(long id);
        Task<T> Get(long id);
        Task<List<T>> Get();
    }
}

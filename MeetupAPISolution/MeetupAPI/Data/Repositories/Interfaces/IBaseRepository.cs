using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.Data.Repositories.Interfaces
{
    public interface IBaseRepository<T>
    {
        public Task<IEnumerable<T>> GetAll();

        public Task<T?> GetById(int id);

        public Task Add(T entity);

        public Task<bool> DeleteById(int id);

        public Task<bool> Update(T entity);

        public Task<bool> IsExist(int id);
    }
}

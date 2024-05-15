using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Berlin.Domain.Entities
{
    public interface IGenericService<T>
    {
        Task<T> Get(int id, params Expression<Func<T, object>>[] includeProperties);
        T GetWithListsMembers(int id, params Expression<Func<T, object>>[] includeProperties);
        Task<List<T>> GetAll();

        Task<List<T>> GetAll(params Expression<Func<T, object>>[] includeProperties);

        Task<List<T>> FindAll(string findStr);

        Task<List<T>> FindAll(Expression<Func<T, bool>> filter);
        Task<List<T>> FindAll(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties);


        Task<T> Add(T article);

        Task<T> Find(int id, params Expression<Func<T, object>>[] includeProperties);

        Task<T> Remove(T article);

        Task<T> Remove(int id);

        Task<T> Update(T article);
    }
}

/////////////////////////////////////
// generated IGenericRepository.cs //
/////////////////////////////////////
using System.Linq.Expressions;

namespace Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        ValueTask<T?> GetById(int id);
        ValueTask<T?> GetById(string id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression);
        Task Add(T entity);
        void Remove(T entitiy);
        void Update(T entitiy);

    }
}

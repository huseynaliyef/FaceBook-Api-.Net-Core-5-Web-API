using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FaceBookAPP.Services.Abstractions
{
    public interface IGenericRepository<T> where T : class
    {
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task Commit();
        Task AddAndCommit(T entity);

        Task<T> GetTableByExpration(Expression<Func<T, bool>> expression);
    }
}

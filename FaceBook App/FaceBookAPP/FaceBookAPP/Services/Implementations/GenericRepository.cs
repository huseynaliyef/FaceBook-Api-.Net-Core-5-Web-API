using FaceBookAPP.DAL;
using FaceBookAPP.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FaceBookAPP.Services.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly FaceBookDbContext _dataBase;
        public GenericRepository(FaceBookDbContext dataBase)
        {
            _dataBase = dataBase;
        }
        public DbSet<T> Table => _dataBase.Set<T>();
        public async Task Add(T entity)
        {
            await Table.AddAsync(entity);
        }

        public async Task AddAndCommit(T entity)
        {
            await Add(entity);
            await Commit();
        }

        public async Task Commit()
        {
            await _dataBase.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
             Table.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return (await Table.ToListAsync());
        }

        public async Task<T> GetById(int id)
        {
            return (await Table.FindAsync(id));
        }

        public async Task<T> GetTableByExpration(Expression<Func<T, bool>> expression)
        {
            return await Table.Where(expression).FirstOrDefaultAsync();
        }

        public void Update(T entity)
        {
            _dataBase.Update(entity);
        }

    }
}

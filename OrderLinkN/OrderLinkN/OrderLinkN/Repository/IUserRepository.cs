using OrderLinkN.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderLinkN.Repository
{
    public interface IUserRepository
    {
        Task CreateTableAsync<T>()
              where T : class, IEntityBase, new();

        Task<List<T>> GetAllAsync<T>()
            where T : class, IEntityBase, new();

        Task<T> GetSingleByIdAsync<T>(int id)
            where T : class, IEntityBase, new();

        Task<T> GetSingleAsync<T>(Expression<Func<T, bool>> predicate)
            where T : class, IEntityBase, new();

        Task<List<T>> FindWhereAsync<T>(Expression<Func<T, bool>> predicate)
            where T : class, IEntityBase, new();

        Task DeleteAsync<T>(T entity)
            where T : class, IEntityBase, new();

        Task DeleteWhereAsync<T>(Expression<Func<T, bool>> predicate)
            where T : class, IEntityBase, new();

        Task DeleteAllAsync<T>()
            where T : class, IEntityBase, new();

        Task DeleteAllAsync<T>(IEnumerable<T> entities)
            where T : class, IEntityBase, new();

       Task<int> AddOrUpdateAsync<T>(T entity)
            where T : class, IEntityBase, new();

        Task AddOrUpdateWithChildrenAsync<T>(T entity)
            where T : class, IEntityBase, new();

        Task AddOrUpdateRangeAsync<T>(IEnumerable<T> entities)
            where T : class, IEntityBase, new();

        Task AddOrUpdateWithChildrenRangeAsync<T>(IEnumerable<T> entities)
            where T : class, IEntityBase, new();

        Task<int> CountAsync<T>()
            where T : class, IEntityBase, new();

        Task<int> AddAsync<T>(T entity)
            where T : class, IEntityBase, new();
    }
}

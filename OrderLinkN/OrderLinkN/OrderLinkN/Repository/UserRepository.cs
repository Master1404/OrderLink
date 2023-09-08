
using OrderLinkN.Models;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderLinkN.Repository
{
    public class UserRepository : IUserRepository
    {
       
        private SQLiteAsyncConnection _database;

        public UserRepository()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "OrderLink.db");
            _database = new SQLiteAsyncConnection(path);

            _database.CreateTableAsync<User>().Wait();
        }

        public Task CreateTableAsync<T>()
             where T : class, IEntityBase, new()
        {
            return _database.CreateTableAsync<T>();
        }

        public async Task DeleteAllAsync<T>()
            where T : class, IEntityBase, new()
        {
            await _database.DropTableAsync<T>();
            await _database.CreateTableAsync<T>();
        }

        public async Task<int> CountAsync<T>()
            where T : class, IEntityBase, new()
        {
            await _database.CreateTableAsync<T>();
            return await _database.Table<T>().CountAsync();
        }

        public async Task<List<T>> GetAllAsync<T>()
            where T : class, IEntityBase, new()
        {
            return await _database.Table<T>().ToListAsync();
        }

        public Task<T> GetSingleByIdAsync<T>(int id)
            where T : class, IEntityBase, new()
        {
            return GetSingleAsync<T>(x => x.Id == id);
        }

        public async Task<int> AddAsync<T>(T entity)
            where T : class, IEntityBase, new()
        {
            var row = -1;

            if (entity != null)
            {
                row = await _database.InsertAsync(entity);
            }

            return row;
        }

        public async Task<int> AddOrUpdateAsync<T>(T entity)
            where T : class, IEntityBase, new()
        {
            var row = -1;

            if (entity != null)
            {
                var existEntity = await GetSingleByIdAsync<T>(entity.Id);

                if (existEntity == null)
                {
                    row = await _database.InsertAsync(entity);
                }
                else
                {
                    row = await _database.UpdateAsync(entity);
                }
            }

            return row;
        }

        public async Task AddOrUpdateWithChildrenAsync<T>(T entity)
            where T : class, IEntityBase, new()
        {
            if (entity != null)
            {
                var existEntity = await GetSingleByIdAsync<T>(entity.Id);

                if (existEntity == null)
                {
                    //await _database.InsertWithChildrenAsync(entity, recursive: true);
                    await _database.InsertAsync(entity);
                }
                else
                {
                   // await _database.UpdateWithChildrenAsync(entity);
                    await _database.UpdateAsync(entity);

                }
            }
        }

        public async Task DeleteAsync<T>(T entity)
            where T : class, IEntityBase, new()
        {
            if (entity != null)
            {
                await _database.DeleteAsync(entity);
            }
        }

        public async Task AddOrUpdateRangeAsync<T>(IEnumerable<T> entities)
            where T : class, IEntityBase, new()
        {
            if (entities != null && entities.Any())
            {
                foreach (var entity in entities)
                {
                    await AddOrUpdateAsync(entity);
                }
            }
        }

        public async Task AddOrUpdateWithChildrenRangeAsync<T>(IEnumerable<T> entities)
            where T : class, IEntityBase, new()
        {
            if (entities != null && entities.Any())
            {
                foreach (var entity in entities)
                {
                    await AddOrUpdateWithChildrenAsync(entity);
                }
            }
        }

        public async Task DeleteAllAsync<T>(IEnumerable<T> entities)
            where T : class, IEntityBase, new()
        {
            if (entities != null && entities.Any())
            {
                foreach (var entity in entities)
                {
                    await DeleteAsync(entity);
                }
            }
        }

        public async Task<T> GetSingleAsync<T>(Expression<Func<T, bool>> predicate)
            where T : class, IEntityBase, new()
        {
            var allMatched = await FindWhereAsync(predicate);

            return allMatched.FirstOrDefault();
        }

        public async Task<List<T>> FindWhereAsync<T>(Expression<Func<T, bool>> predicate)
            where T : class, IEntityBase, new()
        {
            return await _database.Table<T>().Where(predicate).ToListAsync();
        }

        public async Task DeleteWhereAsync<T>(Expression<Func<T, bool>> predicate)
            where T : class, IEntityBase, new()
        {
            var result = await FindWhereAsync(predicate);

            await DeleteAllAsync(result);
        }

      
    }
}

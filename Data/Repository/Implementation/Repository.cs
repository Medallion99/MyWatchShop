using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MyWatchShop.Data;
using MyWatchShop.Data.Repository.Interface;
using MyWatchShop.Models.Entity;

namespace MyWatchShop.Data.Repository.Implementation
{
    public class Repository : IRepository
    {
        private readonly ShopDbContext _ctx;

        public Repository(ShopDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<int> Add<T>(T entity) where T : class
        {
            _ctx.Set<T>().Add(entity);
            return await _ctx.SaveChangesAsync();
        }

        public async Task<int> Delete<T>(T entity) where T : class
        {
            _ctx.Set<T>().Remove(entity);
            return await _ctx.SaveChangesAsync();
        }

        public async Task<int> RemoveRange<T>(List<T> entity) where T : class
        {
            _ctx.Set<T>().RemoveRange(entity);
            return await _ctx.SaveChangesAsync();
        }

        public async Task<IList<T>> GetAll<T>() where T : class
        {
           var result = await _ctx.Set<T>().ToListAsync();
            return result;
        }

        public async Task<T> GetById<T>(string id) where T : class
        {
            var result =  await _ctx.Set<T>().FindAsync(id);
            return result;
            
        }

        public async Task<int> Update<T>(T entity) where T : class
        {
            _ctx.Set<T>().Update(entity);
            return await _ctx.SaveChangesAsync();
        }
    }
}

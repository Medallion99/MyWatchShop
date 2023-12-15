using Microsoft.AspNetCore.Http.HttpResults;
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

        public Task<int> Add<T>(T entity) where T : class
        {
            _ctx.Set<T>().Add(entity);
            return _ctx.SaveChangesAsync();
        }

        public Task<int> Delete<T>(T entity) where T : class
        {
            _ctx.Set<T>().Remove(entity);
            return _ctx.SaveChangesAsync();
        }

        public async Task<IList<T>> GetAll<T>() where T : class
        {
            return _ctx.Set<T>().ToList();
        }

        public async Task<T> GetById<T>(string id) where T : class
        {
            var result =  _ctx.Set<T>().Find(id);
            return result;
            
        }

        public Task<int> Update<T>(T entity) where T : class
        {
            _ctx.Set<T>().Update(entity);
            return _ctx.SaveChangesAsync();
        }
    }
}

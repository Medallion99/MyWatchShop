using MyWatchShop.Models.Entity;

namespace MyWatchShop.Data.Repository.Interface
{
    public interface IRepository
    {
        public Task<int> Add<T>(T entity) where T : class;
        public Task<int> Update<T>(T entity) where T: class;
        public Task<int> Delete<T>(T entity) where T : class;
        public Task<T> GetById<T>(string id) where T : class;
        public Task<IQueryable<T>> GetAll<T>() where T : class;
    }
}

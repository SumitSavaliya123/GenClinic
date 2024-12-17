using System.Linq.Expressions;
using GenClinic_Repository.IRepositories;
using GenClinic_Service.IServices;

namespace GenClinic_Service.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly IBaseRepository<T> _repository;
        public BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual async Task AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
            await SaveAsync();
        }

        public virtual async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter) => await _repository.GetFirstOrDefaultAsync(filter);

        public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, IEnumerable<Expression<Func<T, object>>> includes)
        {
            return await _repository.GetFirstOrDefaultAsync(filter, includes);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            await _repository.UpdateAsync(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _repository.SaveChangesAsync();
        }


        public virtual async Task AddRangeAsync(IEnumerable<T> models) => await _repository.AddRangeAsync(models);

        public async Task UpdateRange(IEnumerable<T> models)
            => await _repository.UpdateRange(models);

        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> filter) => await _repository.AnyAsync(filter);

        public async Task<IEnumerable<T>> GetAllAsync()
           => await _repository.GetAllAsync();

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, Expression<Func<T, T>> select) => await _repository.GetAllAsync(filter, select);

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter) => await _repository.GetAllAsync(filter);

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, IEnumerable<Expression<Func<T, object>>> includes) => await _repository.GetAllAsync(filter, includes);

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, IEnumerable<Expression<Func<T, object>>> includes, string[] thenIncludeExpressions) => await _repository.GetAllAsync(filter, includes, thenIncludeExpressions);

        public async Task<IEnumerable<T>> GetAllAsyncWithExpressions(Expression<Func<T, bool>> filter,
        IEnumerable<Expression<Func<T, object>>> includes,
       Dictionary<Expression<Func<T, object>>, IEnumerable<Expression<Func<object, object>>>> thenIncludes) => await _repository.GetAllAsyncWithExpressions(filter, includes, thenIncludes);

        // public async Task<(int count, IEnumerable<T> data)> GetPaginationWithExpressions(FilterCriteria<T> criteria) => await _repository.GetPaginationWithExpressions(criteria);
    }
}
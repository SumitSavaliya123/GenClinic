using System.Linq.Expressions;

namespace GenClinic_Service.IServices
{
    public interface IBaseService<T> where T : class
    {
        Task AddAsync(T entity);

        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter);

        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, IEnumerable<Expression<Func<T, object>>> includes);

        Task UpdateAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> models);

        Task UpdateRange(IEnumerable<T> models);
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter);

        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, Expression<Func<T, T>> select);

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter);

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, IEnumerable<Expression<Func<T, object>>> includes);

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, IEnumerable<Expression<Func<T, object>>> includes, string[] thenIncludeExpressions);

        Task<IEnumerable<T>> GetAllAsyncWithExpressions(Expression<Func<T, bool>> filter,
         IEnumerable<Expression<Func<T, object>>> includes,
        Dictionary<Expression<Func<T, object>>, IEnumerable<Expression<Func<object, object>>>> thenIncludes);

        // Task<(int count, IEnumerable<T> data)> GetPaginationWithExpressions(FilterCriteria<T> criteria);
    }
}
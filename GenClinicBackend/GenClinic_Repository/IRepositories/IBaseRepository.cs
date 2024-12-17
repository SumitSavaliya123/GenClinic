using System.Linq.Expressions;

namespace GenClinic_Repository.IRepositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task AddAsync(T entity, CancellationToken cancellationToken = default);

        Task SaveChangesAsync();

        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);

        IQueryable<T> GetAll();

        Task<Task> UpdateAsync(T entity, CancellationToken cancellationToken = default);

        Task AddRangeAsync(IEnumerable<T> models);

        Task UpdateRange(IEnumerable<T> models);

        // Task<(int count, IEnumerable<T> data)> GetPaginationWithExpressions(FilterCriteria<T> criteria);

        Task<bool> AnyAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> GetAllAsyncSelect(Expression<Func<T, bool>> filter, Expression<Func<T, T>> select, CancellationToken cancellationToken = default);
        Task<int> CountAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, IEnumerable<Expression<Func<T, object>>> includes);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, IEnumerable<Expression<Func<T, object>>> includes, string[] thenIncludeExpressions);

        Task<IEnumerable<T>> GetAllAsyncWithExpressions(
        Expression<Func<T, bool>> filter,
        IEnumerable<Expression<Func<T, object>>> includes = null,
        Dictionary<Expression<Func<T, object>>, IEnumerable<Expression<Func<object, object>>>> thenIncludes = null);
        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> expression);

        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, IEnumerable<Expression<Func<T, object>>> includes);

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, Expression<Func<T, T>> select);

        Task<T> GetIncludeAsync(Expression<Func<T, object>> include, Expression<Func<T, bool>> filter);

        void DeleteRange(IEnumerable<T> moduleAccessPermissions);
        Task<int> CountAsync(Expression<Func<T, bool>>? filter = null);
        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, IEnumerable<Expression<Func<T, object>>> includes, string[]? thenIncludeExpressions);
        Task<IEnumerable<TResult>> GetAllAsyncSelect<TResult>(
                   Expression<Func<T, bool>> filter,
                   Expression<Func<T, TResult>> select,
                   CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);

        Task ExecuteSqlCommandAsync(string query, params object[] parameters);
    }
}
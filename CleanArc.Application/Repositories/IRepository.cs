using CleanArc.Application.DTOs;

namespace CleanArc.Application.Repositories
{
    public interface IRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
        public Task<IEnumerable<T>> GetAllFilteredAsync(QueryParameters queryParameters, CancellationToken cancellationToken = default);

        public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        public Task<T?> AddAsync(T entity, CancellationToken cancellationToken = default);
        public Task<T?> UpdateAsync(T entity, CancellationToken cancellationToken = default);
        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        public Task<int> CountAsync(CancellationToken cancellationToken = default);

    }
}

namespace CleanArc.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        void Rollback(CancellationToken cancellationToken = default);

    }
}

using CleanArc.Application.Interfaces;
using CleanArc.Infrastructure.Context;

namespace CleanArc.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        public void Rollback(CancellationToken cancellationToken = default)
        {
            // Implement rollback logic if needed
            // For example, you can use a transaction to rollback changes
            // _context.Database.RollbackTransaction();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync();
        }

    }
}

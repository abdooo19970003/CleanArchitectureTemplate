namespace CleanArc.Domain.Common
{
    public abstract class Entity
    {
        private readonly List<IDomainEvent> _domainEvents = new();
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;

        public void UpdateTimestamp() => UpdatedAt = DateTime.UtcNow;
        public void MarkAsDeleted() => IsDeleted = true;

        public IReadOnlyCollection<IDomainEvent> GetDomainEvents => _domainEvents.AsReadOnly();
        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}

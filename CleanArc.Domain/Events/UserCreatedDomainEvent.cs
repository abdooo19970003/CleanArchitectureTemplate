using CleanArc.Domain.Common;

namespace CleanArc.Domain.Events
{
    internal class UserCreatedDomainEvent : IDomainEvent
    {
        private Guid id;

        public UserCreatedDomainEvent(Guid id)
        {
            this.id = id;
        }
    }
}
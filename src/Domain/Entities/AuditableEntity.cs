using System;

namespace SNGBarrett.BookStore.Domain.Entities
{
    public abstract class AuditableEntity
    {
        public DateTime Created { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? LastModified { get; set; }

        public int? LastModifiedBy { get; set; }
    }

    public abstract class AuditableEntity<TKey> : AuditableEntity
    {
        public TKey Id { get; set; }
    }
}

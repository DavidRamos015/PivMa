using System;

namespace MiniAmazon.Domain.Entities
{
    public class EmailsSent : IEntity
    {
        public virtual long Id { get; set; }
        public virtual DateTime CreateDateTime { get; set; }
        public virtual string Description { get; set; }
        public virtual int MailOperationId { get; set; }
        public virtual bool Successfully { get; set; }
    }
}
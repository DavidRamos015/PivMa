using System;

namespace MiniAmazon.Domain.Entities
{
    public class Sale : IEntity
    {
        public virtual long Id { get; set; }

        public virtual DateTime CreateDateTime { get; set; }

        public virtual string Title { get; set; }

        public virtual string Description { get; set; }

        public virtual decimal Price { get; set; }
    }
    
}
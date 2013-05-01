using System;

namespace MiniAmazon.Domain.Entities
{
    public class Categories : IEntity
    {
        public virtual long Id { get; set; }

        public virtual string CreateDateTime { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual int IdParent { get; set; }

        public virtual bool Active { get; set; }
    }
}
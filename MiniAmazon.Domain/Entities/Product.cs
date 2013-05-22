using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniAmazon.Domain.Entities
{
    public class Product : IEntity
    {
        public virtual long Id {get; set;}
        

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual decimal Price { get; set; }

        public virtual DateTime CreateDateTime { get; set; }

        public virtual string Picture1 { get; set; }
        public virtual string Picture2 { get; set; }
        public virtual string Picture3 { get; set; }
        public virtual string Picture4 { get; set; }

        public virtual string YoutubeLink { get; set; }

        public virtual int Inventory { get; set; }

        public virtual bool PostOnFacebook { get; set; }

        public virtual bool Active { get; set; }

        public virtual bool PendingChange { get; set; }

        public virtual int CategoryId { get; set; }

        public virtual int Rating { get; set; }

        public virtual int TotalRaters { get; set; }

        public virtual long AccountId { get; set; }
        

    }
}

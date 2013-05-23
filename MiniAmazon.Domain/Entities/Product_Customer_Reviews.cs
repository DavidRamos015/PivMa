using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniAmazon.Domain.Entities
{
    public class Product_Customer_Reviews : IEntity
    {
        public virtual long Id { get; set; }
        public virtual string Comment { get; set; }
        public virtual DateTime DateReview { get; set; }
        public virtual int ValueReview { get; set; }
        public virtual bool Active { get; set; }
        public virtual long Account_Id { get; set; }
        public virtual long Entity_Id { get; set; }
        public virtual int Rate_Type { get; set; }
    }

 
}

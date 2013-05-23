using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniAmazon.Domain.Entities
{
    public class Product_Post_Provider : IEntity
    {
        public virtual long Id { get; set; }
        public virtual long Product_Id { get; set; }
        public virtual DateTime RegisterDate { get; set; }
        public virtual ExternalProvider Provider_Id { get; set; }
        public virtual string Post_Id { get; set; }
    }
}

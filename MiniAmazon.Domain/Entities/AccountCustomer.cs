using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniAmazon.Domain.Entities
{
    public class AccountCustomer : IEntity
    {
        public virtual long Id { get; set; }
        public virtual long AccountId_C { get; set; }
        public virtual long AccountId_V { get; set; }
    }
}

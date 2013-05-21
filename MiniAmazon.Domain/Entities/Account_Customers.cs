using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniAmazon.Domain.Entities
{
    public class Account_Customers:IEntity
    {
        public virtual long Id { get; set; }


        public virtual long Account_Id { get; set; }

        public virtual long Customer_Id { get; set; }

        
    }
}

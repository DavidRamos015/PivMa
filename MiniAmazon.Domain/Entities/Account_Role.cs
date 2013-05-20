using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniAmazon.Domain.Entities
{
    public class Account_Role :IEntity
    {
        public virtual long Id { get; set; }

        public virtual long Role_Id { get; set; }

        public virtual long Account_Id { get; set; }

    }
}

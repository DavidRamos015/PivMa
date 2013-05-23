using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniAmazon.Domain.Entities
{
    public class Contacts : IEntity
    {
        public virtual long Id { get; set; }
        public virtual long Account_Id { get; set; }
        public virtual long Customer_Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual string Subject { get; set; }
        public virtual string Messages { get; set; }
        public virtual ContactType contactType { get; set; }
    }
}

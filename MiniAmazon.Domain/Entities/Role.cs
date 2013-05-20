using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniAmazon.Domain.Entities
{
    public class Role :IEntity
    {
        public virtual long Id { get; set; }

        public virtual string Name { get; set; }
    }
}

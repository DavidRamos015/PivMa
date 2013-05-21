using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniAmazon.Web.Models
{
    public class GenericButtonInputModel
    {
        public virtual long Id { get; set; }

        public GenericButtonInputModel()
        { Id = -1; }

        public GenericButtonInputModel(object _id)
        {
            if (_id != null)
                Id = Convert.ToInt64(_id);
            else
                Id = -1;
        }
    }
}
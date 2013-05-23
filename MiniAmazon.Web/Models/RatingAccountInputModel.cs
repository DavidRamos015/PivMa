using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniAmazon.Web.Models
{
    public class RatingAccountInputModel
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string MyRate { get; set; }
        public virtual string RateAverage { get; set; }

        public RatingAccountInputModel(object _id, object _name, object _MyRate, object _RateAverage)
        {
            Id = Convert.ToInt64(_id == null ? 0 : _id);
            Name = Convert.ToString(_name == null ? "-" : _name);
            MyRate = Convert.ToString(_MyRate == null ? "0" : _MyRate);
            RateAverage = Convert.ToString(_RateAverage == null ? "0" : _RateAverage);
        }
    }
}
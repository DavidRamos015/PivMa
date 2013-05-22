using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniAmazon.Web.Models
{

    public class ArticleRating
    {
        public virtual long ArticleID { get; set; }
        public virtual int Rating { get; set; }
        public virtual int TotalRaters { get; set; }
        public virtual double AverageRating { get; set; }

        public ArticleRating(long _ArticleID, int _Rating, int _TotalRaters)
        {
            ArticleID = _ArticleID;
            Rating = _Rating;
            TotalRaters = _TotalRaters;
            AverageRating = Convert.ToDouble(Rating) / Convert.ToDouble(TotalRaters);

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//namespace MiniAmazon.Web.Infrastructure
//{
    public class Utils
    {
        public static string Check(double lower, double upper, double toCheck)
        {
            return toCheck > lower && toCheck <= upper ? " checked=\"checked\"" : null;
        }
    }
//}
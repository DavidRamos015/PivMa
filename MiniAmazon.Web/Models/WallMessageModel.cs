using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacebookLogin.Models
{
    public class WallMessageModel
    {
        public string name { get; set; }
        public string link { get; set; }
        public string caption { get; set; }
        public string description { get; set; }
        public string source { get; set; }
        public string actions { get; set; }
        public string privacy { get; set; }
        public string message { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniAmazon.Web.Models
{
    public class MyAccount_SendMailInputModel
    {
        public virtual long Id { get; set; }
        public virtual string Email { get; set; }
        public virtual string Subject { get; set; }
        public virtual string Messages { get; set; }
    }
}
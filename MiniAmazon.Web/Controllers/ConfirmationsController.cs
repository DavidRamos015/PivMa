using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniAmazon.Web.Controllers
{
    public class ConfirmationsController : Controller
    {
        //
        // GET: /Confirmations/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ConfirmOperation()
        {
            return View();
        }
    }
}

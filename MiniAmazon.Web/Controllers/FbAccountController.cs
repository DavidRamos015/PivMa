using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Facebook;


namespace FacebookLogin
{
    public class FbAccountController : Controller
    {

        [HttpPost]
        public JsonResult FacebookLogin(FacebookLoginModel model)
        {
            Session["uid"] = model.uid;
            Session["accessToken"] = model.accessToken;

            return Json(new { success = true });
        }

        [HttpGet]
        public ActionResult UserDetails()
        {
            var client = new FacebookClient(Session["accessToken"].ToString());
            dynamic fbresult = client.Get("me?fields=id,email,first_name,last_name,gender,locale,link,username,timezone,location,picture");
            FacebookUserModel facebookUser = Newtonsoft.Json.JsonConvert.DeserializeObject<FacebookUserModel>(fbresult.ToString());

            return View(facebookUser);
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}

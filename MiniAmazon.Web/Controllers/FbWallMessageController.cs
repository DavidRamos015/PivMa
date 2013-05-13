using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FacebookLogin.Models;

namespace FacebookLogin
{
    public class FbWallMessageController : Controller
    {
        //
        // GET: /WallMessage/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult CreatedSuccessfully()
        {
            return View();
        }


        public ActionResult WallMessageError()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(WallMessageModel model)
        {
            try
            {
                var postValues = new Dictionary<string, string>();

                // list of available parameters available @ http://developers.facebook.com/docs/reference/api/post

                if (Session["accessToken"] == null)
                {
                    ViewBag.Error = "Necesita iniciar sesion en facebook";
                    return RedirectToAction("WallMessageError");
                }

                postValues.Add("access_token", Session["accessToken"].ToString());
                postValues.Add("message", model.message);

                string facebookWallMsgId = string.Empty;
                string response;
                MethodResult header = Helper.SubmitPost(string.Format("https://graph.facebook.com/{0}/feed", Session["uid"].ToString()),
                                                            Helper.BuildPostString(postValues),
                                                            out response);

                if (header.returnCode == MethodResult.ReturnCode.Success)
                {
                    var deserialised =
                        Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
                    facebookWallMsgId = deserialised["id"];
                    return RedirectToAction("CreatedSuccessfully");
                }

            }
            catch
            {

            }

            return RedirectToAction("WallMessageError");
        }


    }
}

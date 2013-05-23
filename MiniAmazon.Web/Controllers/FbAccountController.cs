using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using Facebook;
using MiniAmazon.Data;
using MiniAmazon.Domain;
using MiniAmazon.Domain.Entities;
using MiniAmazon.Web.Controllers;


namespace FacebookLogin
{
    public class FbAccountController : BootstrapBaseController
    {

        private readonly IRepository _repository;
        private readonly IMappingEngine _mappingEngine;

        public FbAccountController(IRepository repository, IMappingEngine mappingEngine)
        {
            _repository = repository;
            _mappingEngine = mappingEngine;
        }

        //[HttpPost]
        //public JsonResult FacebookLogin(FacebookLoginModel model)
        //{
        //    Session["uid"] = model.uid;
        //    Session["accessToken"] = model.accessToken;

        //    return Json(new { success = true });
        //}

        //[HttpGet]
        //public ActionResult UserDetails()
        //{
        //    var client = new FacebookClient(Session["accessToken"].ToString());
        //    dynamic fbresult = client.Get("me?fields=id,email,first_name,last_name,gender,locale,link,username,timezone,location,picture");
        //    FacebookUserModel facebookUser = Newtonsoft.Json.JsonConvert.DeserializeObject<FacebookUserModel>(fbresult.ToString());

        //    return View(facebookUser);
        //}

        //public ActionResult Login()
        //{
        //    return View();
        //}

        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        public ActionResult Facebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = FacebookUtility.ApplicationID,
                client_secret = FacebookUtility.ApplicationSecretCode,
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email" // Add other permissions as needed
            });

            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = FacebookUtility.ApplicationID,
                client_secret = FacebookUtility.ApplicationSecretCode,
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });

            var accessToken = result.access_token;

            // Store the access token in the session
            HttpContext.Session[FacebookUtility.SessionTokenName] = accessToken;

            // update the facebook client with the access token so
            // we can make requests on behalf of the user
            fb.AccessToken = accessToken;

            // Get the user's information
            dynamic me = fb.Get("me?fields=first_name,last_name,id,email,picture");
            string email = me.email;

            Account user = _repository.First<Account>(x => x.Email == email && x.Active);
            List<string> roles = new List<string>();

            if (user != null)
            {
                user.Name = me.first_name + " " + me.last_name;

                _repository.Update(user);
                var roles_user = _repository.Query<Account_Role>(x => x.Account_Id == user.Id);
                var roles_db = _repository.Query<Role>(x => x.Id == x.Id);

                foreach (var ru in roles_user)
                {
                    foreach (var r in roles_db)
                    {
                        if (ru.Role_Id == r.Id)
                            roles.Add(r.Name);
                    }
                }
            }
            else
            {
                roles.Add(MiniAmazon.Data.Utility.UserRole);
                Account account = new Account();
                account.Active = true;
                
                account.Email = me.email;
                account.Name = me.first_name + " " + me.last_name;
                account.Password = "123456789";
                account.PendingConfirmation = false;
                account.PublicEmail = me.id;
                _repository.Create(account);

                Account_Role rol = new Account_Role();
                rol.Account_Id = account.Id;
                rol.Role_Id = 2;
                _repository.Create(rol);

            }

            FormsAuthentication.SetAuthCookie(email, true);
            SetAuthenticationCookie(email, roles);
            return RedirectToAction("Index", "DashBoard");

        }
    }
}

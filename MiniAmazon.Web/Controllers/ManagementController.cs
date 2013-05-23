using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using MiniAmazon.Domain;
using MiniAmazon.Domain.Entities;
using MiniAmazon.Web.Models;

namespace MiniAmazon.Web.Controllers
{
    public class ManagementController : BootstrapBaseController
    {
        //
        // GET: /Management/
        private readonly IRepository _repository;
        private readonly IMappingEngine _mappingEngine;

        public ManagementController(IRepository repository, IMappingEngine mappingEngine)
        {
            _repository = repository;
            _mappingEngine = mappingEngine;
        }


        public ActionResult Menu()
        {
            return View();
        }


        #region Validations

        public bool IsCategoryInUse(int categoryId)
        {
            var item = _repository.First<Product>(x => x.CategoryId == categoryId && x.Active == true);
            if (item != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

   

        public ActionResult GenericButton()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GenericButton(GenericButtonInputModel model)
        {
            return View();
        }

        public ActionResult jsIsCategoryInUse(int categoryId)
        {
            return Json(!IsCategoryInUse(categoryId), JsonRequestBehavior.AllowGet);
        }




        public bool ExistingCategoryName(string name, int categoryIdToSkip)
        {
            var item = _repository.First<Categories>(x => x.Name == name && x.Active == true && x.Id != categoryIdToSkip);
            if (item != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ActionResult jsExistingCategoryName(string name)
        {
            return Json(ExistingCategoryName(name, -1), JsonRequestBehavior.AllowGet);
        }

        public static bool UserIsLogin(System.Security.Principal.IPrincipal controller)
        {
            if (controller.Identity.IsAuthenticated)
                return true;

            return false;
        }


        public static string GetCategoryName(int CategoryId, IRepository repository)
        {
            var cat = repository.First<Categories>(x => x.Id == CategoryId && x.Active);

            if (cat == null)
                return "Todas";

            return cat.Name;

        }

        public static string GetAccountName(System.Security.Principal.IPrincipal user, IRepository repository)
        {
            string anonymous = "Anonimo";

            if (user == null)
                return anonymous;

            if (!user.Identity.IsAuthenticated)
                return anonymous;

            var account = repository.Query<Account>(x => x.Email == user.Identity.Name && x.Active);

            if (account != null)
            {
                if (account.Count() > 0)
                    return account.First<Account>().Name;
            }

            return anonymous;
        }

        public static Account GetAccount(System.Security.Principal.IPrincipal user, IRepository repository)
        {
            var account = repository.Query<Account>(x => x.Email == user.Identity.Name && x.Active);

            if (account != null)
            {
                if (account.Count() > 0)
                    return account.First<Account>();
            }

            return new Account();
        }

        public static long GetAccountID(System.Security.Principal.IPrincipal user, IRepository repository)
        {
            if (user == null)
                return -1;

            if (!user.Identity.IsAuthenticated)
                return -1;

            var account = repository.Query<Account>(x => x.Email == user.Identity.Name && x.Active);

            if (account != null)
            {
                if (account.Count() > 0)
                    return account.First<Account>().Id;
            }

            return -1;
        }

        public bool ExistingCategoryID(int categoryId)
        {
            var item = _repository.First<Categories>(x => x.Id == categoryId && x.Active == true);
            if (item != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ActionResult jsExistingCategoryID(int categoryId)
        {
            return Json(ExistingCategoryID(categoryId), JsonRequestBehavior.AllowGet);
        }




        public bool ExistingUserEmail(string email)
        {
            var account = _repository.First<Account>(x => x.Email == email && x.Active == true && x.Locked == false);
            if (account != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ActionResult jsExistingUserEmail(string email)
        {
            return Json(ExistingUserEmail(email), JsonRequestBehavior.AllowGet);
        }

        #endregion





    }
}

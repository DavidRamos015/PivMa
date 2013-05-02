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
            var account = _repository.First<Account>(x => x.Email == email);
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

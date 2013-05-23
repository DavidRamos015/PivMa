using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using MiniAmazon.Domain;
using MiniAmazon.Domain.Entities;
using MiniAmazon.Web.Models;

namespace MiniAmazon.Web.Controllers
{
    public class VendorController : BootstrapBaseController
    {
        //
        // GET: /Vendor/
        private readonly IRepository _repository;
        private readonly IMappingEngine _mappingEngine;

        public VendorController(IRepository repository, IMappingEngine mappingEngine)
        {
            _repository = repository;
            _mappingEngine = mappingEngine;
            ViewBag.Title = "Vendedores";
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult VendorProfile(long id)
        {

            //long id = ManagementController.GetAccountID(User, _repository);

            var account_vendor = _repository.First<Account>(x => x.Id == id && x.Active);


            if (account_vendor == null)
            {
                Error("No se pudo completar la operación.");
                return RedirectToAction("ProductDetail", "DashBoard", id);
            }


            var reviews_count = 0;
            var reviews = _repository.Query<Product_Customer_Reviews>(x => x.Entity_Id == id && x.Rate_Type == Convert.ToInt32(RateType.Account));
            var reviews_sum = 0;
            var reviews_avg = 0;

            if (reviews != null)
            {
                reviews_count = reviews.Count();
                foreach (var r in reviews)
                {
                    reviews_sum += r.ValueReview;
                }
            }

            reviews_avg = reviews_sum / (reviews_count <= 0 ? 1 : reviews_count);
            ViewBag.Account_Reviews_Count = reviews_count.ToString();
            ViewBag.Account_Reviews_Avg = reviews_avg.ToString();


            ViewBag.Account_Picture = account_vendor.Picture.ToNullSafeString();
            ViewBag.Account_Name = account_vendor.Name.ToNullSafeString();
            ViewBag.Account_PublicEmail = account_vendor.PublicEmail.ToNullSafeString();



            ViewBag.Account_ID = account_vendor.Id;
            ViewBag.Account_WenSite1 = account_vendor.WenSite1.ToNullSafeString();
            ViewBag.Account_WenSite2 = account_vendor.WenSite2.ToNullSafeString();
            ViewBag.Account_WenSite3 = account_vendor.WenSite3.ToNullSafeString();
            ViewBag.Account_WenSite4 = account_vendor.WenSite4.ToNullSafeString();

            ViewBag.About = account_vendor.About.ToNullSafeString();



            var QueryResult = _repository.Query<Product>(x => x.Active && x.AccountId == account_vendor.Id);

            List<SearchSimpleInputModel> result = new List<SearchSimpleInputModel>();

            foreach (var p in QueryResult)
            {
                string category = "Indefinido";
                string vendor = "Desconocido";
                var cat = _repository.GetById<Categories>(p.CategoryId);
                var vend = _repository.GetById<Account>(p.AccountId);

                if (cat != null)
                    category = cat.Description;

                if (vend != null)
                    vendor = cat.Name;

                var r = new SearchSimpleInputModel(p.Id, p.Name, category, vendor, p.Description, p.Price.ToString(), p.Picture1, p.Inventory.ToString());
                result.Add(r);
            }

            var query = result.AsQueryable<SearchSimpleInputModel>();

            return View(query);


        }

        public ActionResult AddToFollower(int id)
        {
            var account_vendor = _repository.First<Account>(x => x.Id == id && x.Active);


            if (account_vendor == null)
            {
                Error("No se pudo completar la operación");
                return RedirectToAction("ProductDetail", "DashBoard", id);
            }

            var Account_Id = ManagementController.GetAccountID(User, _repository);

            var existingAce = _repository.First<Account_Customers>(x => x.Account_Id == Account_Id &&
                                                                     x.Customer_Id == account_vendor.Id);

            if (existingAce != null)
            {
                Attention("Ya eres cliente de " + account_vendor.Name);
                return RedirectToAction("ProductDetail", "DashBoard", new { id = id });
            }

            Account_Customers ac = new Account_Customers();
            ac.Customer_Id = id;
            ac.Account_Id = Account_Id;
            _repository.Create(ac);

            Information("Ahora eres cliente de " + account_vendor.Name);
            return RedirectToAction("ProductDetail", "DashBoard", new { id = id });
        }

        public ActionResult Contact(int id)
        {
            var account = ManagementController.GetAccount(User, _repository);

            var model = new ContactsInputModel();
            model.Customer_Id = account.Id;
            model.Account_Id = id;
            model.Email = account.Email;
            model.FirstName = account.Name;

            return View(model);
        }

        [HttpPost]
        public ActionResult Contact(ContactsInputModel model)
        {
            if (ModelState.IsValid)
            {
                var Account_Id = model.Account_Id;

                var item = _mappingEngine.Map<ContactsInputModel, Contacts>(model);
                item.contactType = ContactType.ContactToSeller;

                _repository.Create(item);

                var vendor = _repository.First<Account>(x => x.Id == Account_Id && x.Active);

                if (vendor != null)
                    if (vendor.Email != null && vendor.Email.Trim().Length > 0)
                    {
                        MiniAmazon.Web.Infrastructure.EmailUtility.SendEmail(_repository, vendor.Email, vendor.Name, model.Messages, model.Subject, MailOperationType.ContactToSeller, true);

                    }

                Success("Mensaje enviado");
                //return RedirectToAction("VendorProfile", "Vendor", new { id = Account_Id });
                return Redirect("javascript:history.back()");



            }
            else
            {
                Error("Ha ocurrido al procesar su petición.");
            }

            return View(model);
        }
    }
}

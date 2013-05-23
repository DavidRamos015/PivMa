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
    public class DashBoardController : BootstrapBaseController
    {

        private readonly IRepository _repository;
        private readonly IMappingEngine _mappingEngine;

        public DashBoardController(IRepository repository, IMappingEngine mappingEngine)
        {
            _repository = repository;
            _mappingEngine = mappingEngine;

        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SimpleFilter()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SimpleFilter(SearchFilterInputModel model)
        {
            ViewBag.Title = "Resultados de tu busqueda:";
            ViewBag.Filter = model.BasicFilter;

            string filter = model.BasicFilter;

            var QueryResult = _repository.Query<Product>(x => x.Active && x.ActiveForSales && (x.Description.Contains(filter) || x.Name.Contains(filter)));

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


        public ActionResult ProductDetail(int id)
        {

            var item = _repository.First<Product>(x => x.Id == Convert.ToInt64(id) && x.Active && x.ActiveForSales);

            if (item == null)
            {
                Error("La información del producto seleccionado no se ha podido obtener.");
                return View();
            }

            string category = "Indefinido";
            string vendor = "Desconocido";
            var cat = _repository.GetById<Categories>(item.CategoryId);
            var vend = _repository.GetById<Account>(item.AccountId);

            if (cat != null)
                category = cat.Description;

            if (vend != null)
                vendor = cat.Name;



            var reviews_count = 0;
            var reviews = _repository.Query<Product_Customer_Reviews>(x => x.Entity_Id == id && x.Rate_Type == Convert.ToInt32(RateType.Product));
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
            ViewBag.Product_Reviews_Count = reviews_count.ToString();
            ViewBag.Product_Reviews_Avg = reviews_avg.ToString();


            ViewBag.ProductTitle = item.Name.ToNullSafeString();
            ViewBag.ProductDesc = item.Description.ToNullSafeString();
            ViewBag.ProductCat = category.ToNullSafeString();
            ViewBag.ProductVendor = vendor.ToNullSafeString();
            ViewBag.ProductAccountID = item.AccountId.ToNullSafeString();
            ViewBag.ProductInventory = item.Inventory.ToNullSafeString();
            ViewBag.ProductPrice = decimal.Round(item.Price, 2, MidpointRounding.AwayFromZero).ToNullSafeString();
            ViewBag.ProductPicture1 = item.Picture1.ToNullSafeString();
            ViewBag.ProductPicture2 = item.Picture2.ToNullSafeString();
            ViewBag.ProductPicture3 = item.Picture3.ToNullSafeString();
            ViewBag.ProductPicture14 = item.Picture4.ToNullSafeString();


            var result = new SearchInputModel(item.Id, item.Name.ToNullSafeString(), category.ToNullSafeString(), vendor.ToNullSafeString(), item.Description.ToNullSafeString(), item.Price.ToString(),
                                              item.Picture1.ToNullSafeString(),
                                              item.Picture2.ToNullSafeString(),
                                              item.Picture3.ToNullSafeString(),
                                              item.Picture4.ToNullSafeString(),
                                              item.YoutubeLink.ToNullSafeString(),
                                              item.Inventory.ToNullSafeString(),
                                              item.AccountId.ToNullSafeString()
                                              );


            return View(result);

        }

        [HttpPost]
        public ActionResult ProductDetail(SearchInputModel model)
        {

            return View();
        }

        public ActionResult RateProduct(int id, int rate)
        {
            bool success = false;
            string error = "";

            try
            {
                var newRating = new Product_Customer_Reviews();
                newRating.Account_Id = ManagementController.GetAccountID(User, _repository);
                newRating.DateReview = DateTime.Now;
                newRating.Active = true;
                newRating.Comment = "";
                newRating.Entity_Id = id;
                newRating.Rate_Type = Convert.ToInt32(RateType.Product);
                newRating.ValueReview = rate;

                _repository.Create(newRating);
                success = true;
            }
            catch (System.Exception ex)
            {
                if (ex.InnerException != null)
                    while (ex.InnerException != null)
                        ex = ex.InnerException;

                error = ex.Message;
            }

            return Json(new { error = error, success = success, pid = id }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult RateAccount(int id, int rate)
        {
            bool success = false;
            string error = "";

            try
            {
                var newRating = new Product_Customer_Reviews();
                newRating.Account_Id = ManagementController.GetAccountID(User, _repository);
                newRating.DateReview = DateTime.Now;
                newRating.Active = true;
                newRating.Comment = "";
                newRating.Entity_Id = id;
                newRating.Rate_Type = Convert.ToInt32(RateType.Account);
                newRating.ValueReview = rate;

                _repository.Create(newRating);
                success = true;
            }
            catch (System.Exception ex)
            {
                if (ex.InnerException != null)
                    while (ex.InnerException != null)
                        ex = ex.InnerException;

                error = ex.Message;
            }

            return Json(new { error = error, success = success, pid = id }, JsonRequestBehavior.AllowGet);
        }

    }
}

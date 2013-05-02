using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MiniAmazon.Domain;
using MiniAmazon.Domain.Entities;
using MiniAmazon.Web.Models;

namespace MiniAmazon.Web.Controllers
{
    public class ProductController : BootstrapBaseController
    {
        //
        // GET: /Item/

        private readonly IRepository _repository;
        private readonly IMappingEngine _mappingEngine;

        public ActionResult Index_Record()
        {
            var datosAView = _repository.Query<Product>(x => x.Active == true);
            var items = datosAView.Project().To<ProductInputModel>();

            ViewBag.Title = "Productos";
            return View(items);
        }

        public ProductController(IRepository repository, IMappingEngine mappingEngine)
        {
            _repository = repository;
            _mappingEngine = mappingEngine;
        }


        public ActionResult Create_Record()
        {
            var item = new ProductInputModel {CreateDateTime = DateTime.Now, PostOnFacebook = true};
            return View(item);
        }

        [HttpPost]
        public ActionResult Create_Record(ProductInputModel model)
        {
            var account = _mappingEngine.Map<ProductInputModel, Product>(model);
            account.Active = true;
            _repository.Create(account);

            return RedirectToAction("Index_Record");
        }

        public ActionResult Delete_Record(int id)
        {
            var item = _repository.GetById<Product>(id);

            if (item != null)
            {
                item.Active = false;
                _repository.Update(item);

                Information("Registro eliminado");
            }

            return RedirectToAction("Index_Record");
        }


        public ActionResult Edit_Record(int id)
        {
            var item = _repository.First<Product>(x => x.Id == id);
            if (item == null)
            {
                return RedirectToAction("Index_Record");
            }
            var itemInputModel = _mappingEngine.Map<Product, ProductInputModel>(item);

            return View(itemInputModel);
        }

        public ActionResult Details_Record(int id)
        {
            var item = _repository.GetById<Product>(id);
            return View(item);
        }
    }
}

using AutoMapper;
using AutoMapper.QueryableExtensions;
using MiniAmazon.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiniAmazon.Domain.Entities;
using MiniAmazon.Web.Models;

namespace MiniAmazon.Web.Controllers
{
    public class CategoriesController : BootstrapBaseController
    {
        //
        // GET: /Categories/

        private readonly IRepository _repository;
        private readonly IMappingEngine _mappingEngine;

        public ActionResult Index()
        {
            var datosAView = _repository.Query<Categories>(x => x.Active == true);
            var category = datosAView.Project().To<CategoriesInputModel>();

            ViewBag.Title = "Categorias";
            return View(category);

        }

        public CategoriesController(IRepository repository, IMappingEngine mappingEngine)
        {
            _repository = repository;
            _mappingEngine = mappingEngine;
        }


        public ActionResult Create()
        {
            return View(new CategoriesInputModel());
        }

        [HttpPost]
        public ActionResult Create(CategoriesInputModel model)
        {
            ViewBag.Title = "Crear categoria";
            var account = _mappingEngine.Map<CategoriesInputModel, Categories>(model);
            account.Active = true;
            _repository.Create(account);

            return RedirectToAction("index");
        }


        public ActionResult Delete(int id)
        {
            var item = _repository.GetById<Categories>(id);

            if (item != null)
            {
                item.Active = false;
                _repository.Update(item);

                Information("Registro eliminado");
            }

            return RedirectToAction("index");
        }


        public ActionResult Edit(int id)
        {
            var category = _repository.First<Categories>(x => x.Id == id);
            if (category == null)
            {
                return RedirectToAction("Index");
            }
            var categoryInputModel = _mappingEngine.Map<Categories, CategoriesInputModel>(category);

            ViewBag.Title = "Editar categoria";
            return View(categoryInputModel);
        }

        [HttpPost]
        public ActionResult Edit(CategoriesInputModel categoryInputModel)
        {
            if (ModelState.IsValid)
            {
                var category = _mappingEngine.Map<CategoriesInputModel, Categories>(categoryInputModel);
                category.Active = true;
                _repository.Update(category);
                Information("Registro modificado");


                return RedirectToAction("Index");
            }

            return View(categoryInputModel);
        }

        public ActionResult Details(int id)
        {
            ViewBag.Title = "Detalle";
            var item = _repository.GetById<Categories>(id);
            return View(item);
        }


    }
}

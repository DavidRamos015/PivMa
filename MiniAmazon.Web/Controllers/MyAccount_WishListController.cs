using System;
using System.Collections.Generic;
using AutoMapper.QueryableExtensions;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using MiniAmazon.Domain;
using MiniAmazon.Domain.Entities;
using MiniAmazon.Web.Models;

namespace MiniAmazon.Web.Controllers
{
    public class MyAccount_WishListController : BootstrapBaseController
    {
        //
        // GET: /MyAccount_WishList/

        private readonly IRepository _repository;
        private readonly IMappingEngine _mappingEngine;

        public MyAccount_WishListController(IRepository repository, IMappingEngine mappingEngine)
        {
            _repository = repository;
            _mappingEngine = mappingEngine;
            ViewBag.Title = "Usuarios";
        }

        public ActionResult Index()
        {
            var datosAView = _repository.Query<AccountWishList>(x => x.Active == true);
            var items = datosAView.Project().To<MyAccountWishListInputModel>();
            
            ViewBag.Title = "Mis Listas de deseos";
            return View(items);
            
        }

        public ActionResult Create()
        {
            ViewBag.Title = "Nueva lista de deseos";
            var item = new MyAccountWishListInputModel();

            return View(item);
        }

        [HttpPost]
        public ActionResult Create(MyAccountWishListInputModel model)
        {

            ViewBag.Title = "Nueva lista de deseos";
            var item = _mappingEngine.Map<MyAccountWishListInputModel, AccountWishList>(model);

            item.Active = true;
            item.Account_Id = ManagementController.GetAccountID(User, _repository);

            _repository.Create(item);

            return RedirectToAction("index");
        }


        public ActionResult Edit(int id)
        {
            var item = _repository.First<AccountWishList>(x => x.Id == id);
            if (item == null)
            {
                Error("Registro no encontrado.");
                return RedirectToAction("Index");
            }
            var itemInputModel = _mappingEngine.Map<AccountWishList, MyAccountWishListInputModel>(item);

            ViewBag.Title = "Editar Lista de deseos.";
            return View(itemInputModel);
        }

        [HttpPost]
        public ActionResult Edit(MyAccountWishListInputModel itemInputModel)
        {
            if (ModelState.IsValid)
            {
                var item = _mappingEngine.Map<MyAccountWishListInputModel, AccountWishList>(itemInputModel);

                item.Active = true;
                _repository.Update(item);
                Information("Registro modificado");
                
                return RedirectToAction("Index");
            }

            return View(itemInputModel);
        }

        public ActionResult Details(int id)
        {
            ViewBag.Title = "Detalle";
            var item = _repository.GetById<AccountWishList>(id);
            return View(item);
        }

        public ActionResult Delete(int id)
        {
            var item = _repository.GetById<AccountWishList>(id);

            if (item != null)
            {
                item.Active = false;
                _repository.Update(item);

                Information("Registro eliminado");
            }

            return RedirectToAction("index");
        }

    }
}

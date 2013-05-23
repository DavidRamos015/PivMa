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
    public class ProductDisableSaleController : BootstrapBaseController
    {
        private readonly IRepository _repository;
        private readonly IMappingEngine _mappingEngine;

        public ProductDisableSaleController(IRepository repository, IMappingEngine mappingEngine)
        {
            _repository = repository;
            _mappingEngine = mappingEngine;
            ViewBag.Title = "Usuarios";
        }

        public ActionResult List()
        {
            ViewBag.Title = "Mis productos";

            var accountID = ManagementController.GetAccountID(User, _repository);

            var list = new List<Product>();

            var datosAView = _repository.Query<Product>(x => x.Active && !x.PendingChange && x.AccountId == accountID);

            if (datosAView != null)
                return View(datosAView.ToList<Product>());

            return View(list);

        }

        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Habilitar/Desabilitar venta";
            var item = _repository.First<Product>(x => x.Id == id && x.Active && !x.PendingChange);
            if (item == null)
            {
                Error("Registro no encontrado.");
                return RedirectToAction("List");
            }

            item.ActiveForSales = !item.ActiveForSales;
            _repository.Update(item);

            return RedirectToAction("List");
        }
               

    }
}

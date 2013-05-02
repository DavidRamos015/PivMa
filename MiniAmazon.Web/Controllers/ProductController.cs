using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MiniAmazon.Domain;
using MiniAmazon.Domain.Entities;
using MiniAmazon.Web.Infrastructure;
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
            var item = new ProductInputModel { CreateDateTime = DateTime.Now, PostOnFacebook = true };
            return View(item);
        }

        [HttpPost]
        public ActionResult Create_Record(ProductInputModel model)
        {
            var account = _mappingEngine.Map<ProductInputModel, Product>(model);
            account.Active = true;
            account.AccountId = 1;
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
            var item = _repository.First<Product>(x => x.Id == id && x.Active == true);
            if (item == null)
            {
                return RedirectToAction("Index_Record");
            }

            var productChange = ProductPendingChanges.CopyData(item);
            var model = _mappingEngine.Map<ProductPendingChanges, ProductUpdateInputModel>(productChange);


            if (item.PendingChange)
            {
                Attention("Su producto tiene una modificación pendiente de aprobar.");
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit_Record(ProductUpdateInputModel itemModel)
        {


            if (ModelState.IsValid)
            {

                var item = _mappingEngine.Map<ProductUpdateInputModel, ProductPendingChanges>(itemModel);
                var product = _repository.First<Product>(x => x.Id == itemModel.ProductId && x.Active == true);

                if (product == null)
                {
                    Error("No se encontro el producto relacionado con esta solicitud.");
                    return View(item);
                }

                if (product.PendingChange)
                {
                    Attention("Su producto tiene una modificación pendiente de aprobar, Este aún no puede ser modificado.");
                    return RedirectToAction("Index_Record");
                }

                // EmailUtility.SaveRegisterEmailConfirmationOperation(_repository, "Confirmations", "ConfirmOperation", MailOperationType.ProductDataChange, item.Id);
                
                item.PendingChange = true;
                product.PendingChange = true;

                _repository.Create(item);
                _repository.Update(product);

                Information("Su cambio esta pendiente de aplicar, una solicitud a sido enviada a los administradores de nuestro sitio.");

                EmailUtility.SendEmail(_repository, Utility.AdminEmail, "Vendedor en Pivma",
                                        body: "Estimados sres.<br> <br> He ingresado erroneamente la información de mi producto y me gustaria que aprueben mis cambios:<br><br> Razon:" + item.Comments + "<br> Codigo de producto:" + item.ProductId.ToString() + "<br> Codigo solicitud:" + item.Id.ToString() + "<br><br>Muchas gracias de antemano y disculpan las molestias",
                                        subject: "Modificacion de publicación",
                                        mailOperationType: MailOperationType.ProductDataChange,
                                        async: true
                                       );




                return RedirectToAction("Index_Record");
            }

            return View(itemModel);
        }

        public ActionResult Details_Record(int id)
        {
            var item = _repository.GetById<Product>(id);
            return View(item);
        }
    }
}

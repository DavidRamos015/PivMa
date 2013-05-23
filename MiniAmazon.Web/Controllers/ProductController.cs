using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Facebook;
using MiniAmazon.Data;
using MiniAmazon.Domain;
using MiniAmazon.Domain.Entities;
using MiniAmazon.Web.Infrastructure;
using MiniAmazon.Web.Model;
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
            var Account_Id = ManagementController.GetAccountID(User, _repository);

            var datosAView = _repository.Query<Product>(x => x.Active == true && x.AccountId == Account_Id);
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
            //if (!User.Identity.IsAuthenticated)
            //{
            //    Attention(Utility.MsjNeedToLogin);
            //    return RedirectToAction("Index", "DashBoard");
            //}

            ViewBag.Title = "Crear nuevo producto";
            var item = new ProductInputModel { CreateDateTime = DateTime.Now, PostOnFacebook = true };
            return View(item);
        }

        [HttpPost]
        public ActionResult Create_Record(ProductInputModel model)
        {
            ViewBag.Title = "Crear nuevo producto";
            var producto = _mappingEngine.Map<ProductInputModel, Product>(model);
            producto.Active = true;
            producto.AccountId = ManagementController.GetAccountID(User, _repository);
            _repository.Create(producto);


            if (model.PostOnFacebook)
            {

                dynamic messagePost = new ExpandoObject();
                messagePost.picture = model.Picture1;
                messagePost.link = Utility.UrlAplication; // FacebookUtility.GenerateUrlProduct(producto.Id.ToNullSafeString());
                messagePost.video = model.YoutubeLink;
                messagePost.name = model.Name;

                messagePost.caption = "Categoria: " + ManagementController.GetCategoryName(model.CategoryId, _repository);
                messagePost.description = model.Description + Environment.NewLine +
                                         "Precio:" + producto.Price.ToNullSafeString() + Environment.NewLine +
                                         " Inventario:" + producto.Inventory.ToNullSafeString();

                messagePost.message = ManagementController.GetAccountName(User, _repository) + " ha creado un nuevo articulo.";

                if (HttpContext.Session[FacebookUtility.SessionTokenName] == null)
                {
                    HttpContext.Session[FacebookUtility.SessionTokenName] = FacebookUtility.Token;
                }

                if (HttpContext.Session[FacebookUtility.SessionTokenName] != null)
                {
                    string acccessToken = HttpContext.Session[FacebookUtility.SessionTokenName].ToString();
                    FacebookClient appp = new FacebookClient(acccessToken);
                    appp.AppId = FacebookUtility.ApplicationID;
                    appp.AppSecret = FacebookUtility.ApplicationSecretCode;
                    var s = FacebookUtility.Token;

                    try
                    {
                        var postId = appp.Post("me/feed?", messagePost);

                        var postData = new Product_Post_Provider();
                        postData.Product_Id = producto.Id;
                        postData.Provider_Id = ExternalProvider.Facebook;
                        postData.RegisterDate = DateTime.Now;
                        postData.Post_Id = postId.ToString();
                        _repository.Create(postData);
                        Success("El producto ha sido publicado en Facebook");
                    }
                    catch (Exception ex)
                    {
                        Error("El articulo no se ha pudo publicar." + ex.Message);
                    }
                }
                else
                {
                    Error("Debe iniciar sesión con su cuenta de Facebook");
                }
            }

            /*var WallModel = new WallMessageModel();
            WallModel.description = producto.Description;
            WallModel.caption = producto.Name;
            WallModel.message = "Compartiendo:" + producto.Name + ", " + producto.Description + " a " + producto.Price.ToString() + "$, solamente " + producto.Inventory + " en inventario";
            WallModel.name = producto.Name;
            WallModel.link = "http://www.pivma.com/" + producto.Name;

            var result = new FbWallMessageController().Post(WallModel, FacebookUtility.Token, FacebookUtility.UserId);
            */

            return RedirectToAction("Index_Record");
        }

        public ActionResult Delete_Record(int id)
        {
            ViewBag.Title = "Eliminar producto";
            var item = _repository.GetById<Product>(id);

            if (item != null)
            {
                item.Active = false;
                _repository.Update(item);

                Information("Registro eliminado");
            }

            return RedirectToAction("Index_Record");
        }



        public ActionResult GetPendingApprovalProductList()
        {
            ViewBag.Title = "Modificaciones de productos pendientes de aprobación.";

            var datosAView = _repository.Query<ProductPendingChanges>(x => x.PendingChange == true);
            datosAView = datosAView.OrderBy(x => x.CreateDateTimePendingChange);

            var items = datosAView.Project().To<ProductUpdateApprovalInputModel>();

            return View(items);
        }

        public ActionResult ApprovalOrDeniedChanges(int id)
        {
            ViewBag.Title = "Aprobar/Denegar cambio";
            var item = _repository.First<ProductPendingChanges>(x => x.Id == id && x.PendingChange == true);
            if (item == null)
            {
                Attention("Solicitud no encontrada");
                return RedirectToAction("GetPendingApprovalProductList");
            }

            var inputModel = _mappingEngine.Map<ProductPendingChanges, ProductUpdateApprovalInputModel>(item);

            ViewBag.Title = "Editar categoria";
            return View(inputModel);

        }

        [HttpPost]
        public ActionResult ApprovalOrDeniedChanges(ProductUpdateApprovalInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                var change = _repository.First<ProductPendingChanges>(x => x.Id == inputModel.Id && x.PendingChange == true);
                if (change == null)
                {
                    Attention("Solicitud no encontrada");
                    return RedirectToAction("GetPendingApprovalProductList");
                }

                var product = _repository.First<Product>(x => x.Id == inputModel.ProductId && x.PendingChange == true && x.Active == true);
                //var account = _repository.First<Account>(x => x.Id == product.AccountId && x.Active == true && x.Locked == false);

                var email = Utility.AdminEmail;// account == null ? Utility.AdminEmail : account.Email;
                var name = Utility.AdminName;// account == null ? "Usuario" : account.Name;
                if (!inputModel.Approved)
                {
                    EmailUtility.SendEmail(_repository,
                                            email,
                                            name, body: "Estimado cliente, <br> <br>Su solicitud ha sido rechazada debido a incumplimientos con nuestras politicas" +
                                            "<br>Información de producto rechazado: <br><br>" +
                                            "<br>Codigo: " + product.Id.ToString() +
                                            "<br>Nombre: " + product.Name +
                                            "<br>Descripción: " + product.Description +
                                            "<br>Codigo de solicitud: " + change.Id.ToString() +
                                            "<br><br>Comentarios del rechazo:" + change.CommentsWhyNotApproved, subject: "Solicitud de cambios en producto rechazada", mailOperationType: MailOperationType.ProductDataChange, async: true);

                }

                product.PendingChange = false;
                change.PendingChange = false;
                change.Approved = inputModel.Approved;
                change.CommentsWhyNotApproved = inputModel.CommentsWhyNotApproved;

                _repository.Update(product);
                _repository.Update(change);
                Information("Producto aprobado.");
            }

            return RedirectToAction("GetPendingApprovalProductList");
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

using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MiniAmazon.Domain;
using MiniAmazon.Domain.Entities;
using MiniAmazon.Web.Infrastructure;
using MiniAmazon.Web.Models;

namespace MiniAmazon.Web.Controllers
{
    public class AccountController : BootstrapBaseController
    {
        private readonly IRepository _repository;
        private readonly IMappingEngine _mappingEngine;

        public AccountController(IRepository repository, IMappingEngine mappingEngine)
        {
            _repository = repository;
            _mappingEngine = mappingEngine;
            ViewBag.Title = "Usuarios";
        }

        public ActionResult SignIn()
        {
            ViewBag.Title = "Iniciar sesion";
            return View(new AccountSignInModel());
        }

        [HttpPost]
        public ActionResult SignIn(AccountSignInModel accountSignInModel)
        {
            ViewBag.Title = "Iniciar sesion";
            var account =
                _repository.First<Account>(
                    x => x.Email == accountSignInModel.Email && x.Password == accountSignInModel.Password && x.Locked == false && x.Active == true && x.PendingConfirmation == true);

            if (account != null)
            {
                return RedirectToAction("Index", "DashBoard");
            }

            return View(accountSignInModel);
        }






        public ActionResult Index()
        {
            return RedirectToAction("Index", "DashBoard");
        }

        public ActionResult Create_Record()
        {
            ViewBag.Title = "Nuevo usuario";
            var item = new AccountInputModel();

            return View(item);
        }

        [HttpPost]
        public ActionResult Create_Record(AccountInputModel accountInputModel)
        {
            var account = _mappingEngine.Map<AccountInputModel, Account>(accountInputModel);
            account.PendingConfirmation = true;
            account.Active = true;
            account.Locked = false;

            var acc = _repository.First<Account>(x => x.Email == accountInputModel.Email);

            if (acc != null)
            {
                Attention("La cuenta de correo ya existe");
                return View();
            }

            _repository.Create(account);

            return RedirectToAction("Index", "DashBoard");

        }


        public ActionResult UserAdminControl()
        {
            var datosAView = _repository.Query<Account>(x => x.Id > 0);
            var listRecord = datosAView.Project().To<AccountLockedInputModel>();

            ViewBag.Title = "Administración de usuarios";
            return View(listRecord);

        }

        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Eliminar/Bloquear";
            var item = _repository.First<Account>(x => x.Id == id);
            if (item == null)
            {
                return RedirectToAction("UserAdminControl");
            }
            var accountLockedInputModel = _mappingEngine.Map<Account, AccountLockedInputModel>(item);


            return View(accountLockedInputModel);
        }

        [HttpPost]
        public ActionResult Edit(AccountLockedInputModel InputModel)
        {
            ViewBag.Title = "Eliminar/Bloquear";
            if (ModelState.IsValid)
            {
                var item = _mappingEngine.Map<AccountLockedInputModel, Account>(InputModel);
                item.Active = InputModel.Active;
                item.Locked = InputModel.Locked;
                var message = "";
                _repository.Update(item);


                if (!item.Active)
                    message = ("Usuario eliminado");

                if (item.Locked)
                    message = ("Usuario bloqueado");

                if (message.Trim().Length > 0)
                {
                    Information(message + "\r\n" + "Un correo electronico ha sido enviado al usuario.");
                    EmailUtility.SendEmail(item.Email, item.Name, InputModel.Comments, message, MailOperationType.UserDisableorLocked, true);
                }
                else
                    Information("Cambios realizados");


                return RedirectToAction("UserAdminControl","Account");
            }

            return View(InputModel);
        }

    }
}
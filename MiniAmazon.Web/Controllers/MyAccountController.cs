using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MiniAmazon.Domain;
using MiniAmazon.Domain.Entities;
using MiniAmazon.Web.Infrastructure;
using MiniAmazon.Web.Models;

namespace MiniAmazon.Web.Controllers
{
    public class MyAccountController : BootstrapBaseController
    {
        private readonly IRepository _repository;
        private readonly IMappingEngine _mappingEngine;

        public MyAccountController(IRepository repository, IMappingEngine mappingEngine)
        {
            _repository = repository;
            _mappingEngine = mappingEngine;
            ViewBag.Title = "Usuarios";
        }

        public ActionResult PasswordRecovery()
        {
            ViewBag.Title = "Recuperar contraseña";
            return View(new MyAccountRecoveryPasswordInputModel());
        }

        [HttpPost]
        public ActionResult PasswordRecovery(MyAccountRecoveryPasswordInputModel inpuModel)
        {
            ViewBag.Title = "Recuperar contraseña";


            var magt = new ManagementController(_repository, _mappingEngine);

            if (!magt.ExistingUserEmail(inpuModel.Email))
            {
                Attention("No se ha encontrado ninguna cuenta para el correo electronico especificado.");
                return View(inpuModel);
            }

            var account = _repository.First<Account>(x => x.Email == inpuModel.Email && x.Locked == false && x.Active == true);


            var codetoconfirm = EmailUtility.SaveRegisterEmailConfirmationOperation(_repository, "DashBoard", "Index",
                                                                MailOperationType.PasswordChange, account.Id);

            account.PendingConfirmation = true;
            _repository.Update(account);

            EmailUtility.SendEmail(_repository,
                                   account.Email,
                                   account.Name,
                                   "Por favor haz click en la direccion especificada he ingresa el siguiente codigo:<br> <br> Codigo:" +
                                   codetoconfirm + "<br>Dirección" + Utility.UrlToConfirm,
                                   "Recuperación de contraseña",
                                   MailOperationType.PasswordChange,
                                   true);

            Information("Un mensaje con información para recuperar cambiar la clave ha sido enviado a su correo.");

            return RedirectToAction("Index", "DashBoard");
        }


        public ActionResult SignIn()
        {
            ViewBag.Title = "Iniciar sesion";
            return View(new MyAccountSignInModel());
        }

        [HttpPost]
        public ActionResult SignIn(MyAccountSignInModel accountSignInModel)
        {
            ViewBag.Title = "Iniciar sesion";
            var account =
                _repository.First<Account>(
                    x => x.Email == accountSignInModel.Email && x.Password == accountSignInModel.Password && x.Locked == false && x.Active == true);

            if (account != null)
            {
                if (account.PendingConfirmation == true)
                {
                    Attention("Su cuenta esta pendiente de confirmación, favor revise su correo electronico.");
                    return View(accountSignInModel);
                }

                return RedirectToAction("Index", "DashBoard");
            }

            Information("Credenciales incorrectas.");
            return View(accountSignInModel);
        }


        public ActionResult Index()
        {
            return RedirectToAction("Index", "DashBoard");
        }

        public ActionResult Create_Record()
        {
            ViewBag.Title = "Nuevo usuario";
            var item = new MyAccountInputModel { CountryId = 1 };

            return View(item);
        }

        [HttpPost]
        public ActionResult Create_Record(MyAccountInputModel accountInputModel)
        {

            if (accountInputModel.PasswordConfirm != accountInputModel.Password)
            {
                Attention("La contraseña no coincide.");
                return View(accountInputModel);
            }

            var account = _mappingEngine.Map<MyAccountInputModel, Account>(accountInputModel);
            account.PendingConfirmation = true;
            account.Active = true;
            account.Locked = false;
            account.PendingConfirmation = true;

            var magt = new ManagementController(_repository, _mappingEngine);

            if (magt.ExistingUserEmail(account.Email))
            {
                Attention("El correo ya esta registrado, intente con otro.");
                return View(accountInputModel);
            }

            _repository.Create(account);

            var codeToConfirm = EmailUtility.SaveRegisterEmailConfirmationOperation(_repository, "DashBoard", "Index",
                                                                MailOperationType.RegisterAccount, account.Id);

            EmailUtility.SendEmail(_repository, account.Email, account.Name,
                                   "Por favor ingresa este codigo en la siguiente\r\n Codigo:" + codeToConfirm +
                                   "\r\n url:" + Utility.UrlToConfirm, "Confirmación de cuenta",
                                   MailOperationType.RegisterAccount, true);

            Information("Un mensaje de confirmación de la cuenta ha sido enviado a su correo.");

            return RedirectToAction("Index", "DashBoard");

        }


        public ActionResult UserAdminControl()
        {
            var datosAView = _repository.Query<Account>(x => x.Id > 0);
            var listRecord = datosAView.Project().To<MyAccountLockedInputModel>();

            ViewBag.Title = "Administración de usuarios";
            return View(listRecord);

        }

        public ActionResult VerifyPasswordMatch(string password, string confirmPassword)
        {
            if (password == confirmPassword)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Eliminar/Bloquear";
            var item = _repository.First<Account>(x => x.Id == id);

            if (item == null)
            {
                return RedirectToAction("UserAdminControl");
            }
            var accountLockedInputModel = _mappingEngine.Map<Account, MyAccountLockedInputModel>(item);


            return View(accountLockedInputModel);
        }

        [HttpPost]
        public ActionResult Edit(MyAccountLockedInputModel InputModel)
        {
            ViewBag.Title = "Eliminar/Bloquear";
            if (ModelState.IsValid)
            {
                var item = _mappingEngine.Map<MyAccountLockedInputModel, Account>(InputModel);
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
                    EmailUtility.SendEmail(_repository, item.Email, item.Name, InputModel.Comments, message, MailOperationType.UserDisableorLocked, true);
                }
                else
                    Information("Cambios realizados");


                return RedirectToAction("UserAdminControl", "MyAccount");
            }

            return View(InputModel);
        }

    }
}
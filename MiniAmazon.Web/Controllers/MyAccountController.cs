using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MiniAmazon.Domain;
using MiniAmazon.Domain.Entities;
using MiniAmazon.Web.Infrastructure;
using MiniAmazon.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web.Security;
using System;
using MiniAmazon.Data;

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

            Information("Un mensaje con la información necesaria para recuperar cambiar la clave ha sido enviado a su correo.");

            return RedirectToAction("Index", "DashBoard");
        }


        //public ActionResult SignIn()
        //{
        //    ViewBag.Title = "Iniciar sesion";
        //    return View(new MyAccountSignInModel());
        //}

        public ActionResult SignIn(string error = null)
        {
            if (!String.IsNullOrEmpty(error))
            {
                Error(error);
            }
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


                List<string> roles = new List<string>();
                var roles_user = _repository.Query<Account_Role>(x => x.Account_Id == account.Id);//.Select(x => x.Role_Id).ToList();
                var roles_db = _repository.Query<Role>(x => x.Id == x.Id);

                foreach (var ru in roles_user)
                {
                    foreach (var r in roles_db)
                    {
                        if (ru.Role_Id == r.Id)
                            roles.Add(r.Name);
                    }
                }

                FormsAuthentication.SetAuthCookie(accountSignInModel.Email, accountSignInModel.RememberMe);
                //SetAuthenticationCookie(accountSignInModel.Email, new List<string> { "Admin", "Patito" });
                SetAuthenticationCookie(accountSignInModel.Email, roles);
                return RedirectToAction("Index", "DashBoard");
            }

            Information("Credenciales incorrectas.");
            return View(accountSignInModel);
        }

        [Authorize]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "DashBoard");
        }


        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("SignIn");
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

            var role = new Account_Role();
            role.Account_Id = -1;

            role.Role_Id = _repository.Query<Role>(x => x.Name == Utility.UserRole).First<Role>().Id;
            var user = _repository.Query<Account>(x => x.Email == account.Email && x.Active == true);

            if (user != null)
            {
                role.Account_Id = user.First<Account>().Id;
            }


            _repository.Create(role);


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

        public ActionResult Profile(string  email)
        {
            ViewBag.Title = "Tu Perfil";
            var item = _repository.First<Account>(x => x.Email ==email  && x.Active);

            if (item == null)
            {
                Error("El perfil no pudo ser cargado.");
                return RedirectToAction("Index", "DashBoard");
            }
            var accountLockedInputModel = _mappingEngine.Map<Account, MyAccountLockedInputModel>(item);


            return View(accountLockedInputModel);
        }

        [HttpPost]
        public ActionResult Profile(MyAccountLockedInputModel InputModel)
        {
            ViewBag.Title = "Tu Perfil";
            if (ModelState.IsValid)
            {
                var account = _repository.First<Account>(x => x.Id == InputModel.Id);

                if (account == null)
                {
                    Error("El perfil no pudo ser cargado.");
                    return RedirectToAction("Index", "DashBoard");
                }

                var item = _mappingEngine.Map<MyAccountLockedInputModel, Account>(InputModel);
                item.Active = account.Active;
                item.Locked = account.Locked;
                item.PendingConfirmation = account.PendingConfirmation;

                if (account.Password != item.Password)
                {
                    EmailUtility.SendEmail(_repository, item.Email,
                                                        item.Name,
                                                        InputModel.Comments,
                                                        "Haz cambiado la contraseña de tu cuenta el" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(),
                                                        MailOperationType.PasswordChangedWhenUpdatedProfile, true);
                }

                _repository.Update(item);

                Information("Tu perfil ha sido actualizado.");


                return RedirectToAction("Index", "DashBoard");
            }

            return View(InputModel);
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using MiniAmazon.Domain;
using MiniAmazon.Domain.Entities;
using MiniAmazon.Web.Infrastructure;
using MiniAmazon.Web.Models;

namespace MiniAmazon.Web.Controllers
{
    public class ConfirmationsController : BootstrapBaseController
    {
        //
        // GET: /Confirmations/

        private readonly IRepository _repository;
        private readonly IMappingEngine _mappingEngine;
        public ConfirmationsController(IRepository repository, IMappingEngine mappingEngine)
        {
            _repository = repository;
            _mappingEngine = mappingEngine;
        }

        public ActionResult Index_Record()
        {
            return RedirectToAction("Index", "DashBoard");
        }

        public ActionResult Create_Record()
        {
            ViewBag.Title = "Confirmación de operación";
            var item = new ConfirmationsInputModel();
            return View(item);
        }

        [HttpPost]
        public ActionResult Create_Record(ConfirmationsInputModel confirmationsInputModel)
        {
            ViewBag.Title = "Confirmación de operación";

            var operation =
                _repository.First<EmailsConfirmationOperation>(
                    x => x.CodeToConfirm == confirmationsInputModel.CodeToConfirm);

            if (operation == null)
            {
                Error("El codigo de confirmación es incorrecto.");
                return View(confirmationsInputModel);
            }

            var operationType = (MailOperationType)operation.MailOperationTypeId;


            switch (operationType)
            {
                case MailOperationType.PasswordChange:
                    {
                        var account = _repository.First<Account>(x => x.Id == operation.ObjectID);
                        if (account == null)
                        {
                            Error("La recuperación de contraseña no se ha podido realizar, esta operación ha expirado.");
                            return RedirectToAction("Index", "DashBoard");
                        }

                        account.PendingConfirmation = false;
                        _repository.Update(account);
                        EmailUtility.SendEmail(_repository, account.Email, account.Name,
                                               "Su contraseña ha sido restablecida el " +
                                               DateTime.Now.ToLongDateString(), "Recuperación de contraseña",
                                               MailOperationType.PasswordChange, true);

                        return RedirectToAction("Index", "DashBoard");


                    }
                case MailOperationType.RegisterAccount:
                    {
                        var accountToConfirm = _repository.First<Account>(x => x.Id == operation.ObjectID);
                        if (accountToConfirm == null)
                        {
                            Error("La confirmación de su cuenta no se ha podido realizar, esta operación ha expirado.");
                            return RedirectToAction("Index", "DashBoard");
                        }

                        accountToConfirm.PendingConfirmation = false;
                        accountToConfirm.Active = true;
                        accountToConfirm.Locked = false;
                        _repository.Update(accountToConfirm);
                        EmailUtility.SendEmail(_repository, accountToConfirm.Email, accountToConfirm.Name,
                                               "Su cuenta ha sido activada exitosamente " +
                                               DateTime.Now.ToLongDateString(), "Recuperación de contraseña",
                                               MailOperationType.RegisterAccount, true);

                        return RedirectToAction("Index", "DashBoard");
                    }
                default:
                    Information("Proceso para: " + operationType.ToString() + " aún no ha sido implementada.");
                    return RedirectToAction("Index", "DashBoard");

            }

            //var record = _mappingEngine.Map<ConfirmationsInputModel, EmailsConfirmationOperation>(confirmationsInputModel);

        }
    }
}

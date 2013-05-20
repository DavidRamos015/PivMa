using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using MiniAmazon.Domain;
using MiniAmazon.Domain.Entities;
using MiniAmazon.Web.Models;

namespace MiniAmazon.Web.Controllers
{
    public class DashBoardController : Controller
    {

          private readonly IRepository _repository;
        private readonly IMappingEngine _mappingEngine;

        public DashBoardController(IRepository repository, IMappingEngine mappingEngine)
        {
            _repository = repository;
            _mappingEngine = mappingEngine;
            
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SimpleFilter()
        {
            ViewBag.Title = "Resultados";

            string filter = "";

            var datosAView = _repository.Query<Product>(x => x.Active == true && (x.Description.Contains(filter)));

            List<SearchInputModel> result = new List<SearchInputModel>();

            foreach (var p in datosAView)
            {
                string category = "Indefinido";
                string vendor = "Desconocido";
                var cat = _repository.GetById<Categories>(p.CategoryId);
                var vend = _repository.GetById<Account>(p.AccountId);

                if (cat != null)
                    category = cat.Description;

                if (vend != null)
                    vendor = cat.Name;

                var r = new SearchInputModel(p.Name, category, vendor, p.Description, p.Price.ToString(), p.Picture1, p.Picture2, p.Picture3, p.Picture4, p.YoutubeLink, p.Inventory.ToString(), p.AccountId.ToString());
                result.Add(r);
            }

            return View(result);
        }


    }
}

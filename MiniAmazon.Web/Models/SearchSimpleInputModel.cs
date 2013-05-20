using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace MiniAmazon.Web.Models
{
    public class SearchSimpleInputModel
    {
        public virtual long Id { get; set; }

        [Display(Name="Imagen")]
        public virtual string Picture1 { get; set; }

        [Display(Name = "Titulo")]
        public virtual string ProductName { get; set; }

        [Display(Name = "Descripción")]
        public virtual string Description { get; set; }

        [Display(Name = "Categoria")]
        public virtual string CategoryName { get; set; }

        [Display(Name = "Vendedor")]
        public virtual string VendorName { get; set; }


        [Display(Name = "Precio")]
        public virtual string Price { get; set; }

        [Display(Name = "Disponibles")]
        public virtual string Inventory { get; set; }

        public SearchSimpleInputModel(long _id, string _ProductName,
                                string _CategoryName,
                                string _VendorName,
                                string _Description,
                                string _Price,
                                string _Picture1,
                                string _Inventory
                                )
        {
            Id = _id;
            ProductName = _ProductName;
            CategoryName = _CategoryName;
            VendorName = _VendorName;

            Description = _Description;

            Price = _Price;

            Picture1 = _Picture1;

            Inventory = _Inventory;

        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniAmazon.Web.Models
{
    public class ProductInputModel
    {
        public virtual long Id { get; set; }


        [Required]
        [StringLength(50, ErrorMessage = "El nombre es requerido.")]
        [Display(Name = "Nombre Articulo")]
        public virtual string Name { get; set; }

        [Required]
        [Display(Name = "Descripción")]
        [StringLength(255, ErrorMessage = "La descripción es requerida.")]
        public virtual string Description { get; set; }


        [Required]
        [Display(Name = "Precio")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        [DataType(DataType.Currency, ErrorMessage = "Formato incorrecto.")]
        [Range(0, 5000, ErrorMessage = "Rango incorrecto, debe estar entre 0- 5000")]
        public virtual decimal Price { get; set; }

        [Display(Name = "Fecha de registro")]
        //[DisplayFormat(DataFormatString = "{0:d}")]
        [Required(ErrorMessage = "La fecha es requerida.")]
        [DataType(DataType.Date, ErrorMessage = "Formato de fecha incorrecto.")]
        public virtual DateTime CreateDateTime { get; set; }


        [Display(Name = "Imagen")]
        public virtual string Picture { get; set; }

        [Display(Name = "Enlace video")]
        public virtual string YoutubeLink { get; set; }


        [DataType(DataType.Currency, ErrorMessage = "Formato incorrecto.")]
        [Required(ErrorMessage = "Existencia en inventario es requerida.")]
        [Range(0, 5000, ErrorMessage = "Rango incorrecto, debe estar entre 0- 5000")]
        [Display(Name = "Cant. en Inventario")]
        public virtual int Inventory { get; set; }


        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "Especificar Categoria.")]
        [Remote("jsExistingCategoryID", "Management", ErrorMessage = "Especificar una categoria existente.")]
        public virtual int CategoryId { get; set; }

        [Display(Name = "Publicar en Facebook")]
        public virtual bool PostOnFacebook { get; set; }
        //public virtual bool Active { get; set; }

    }
}
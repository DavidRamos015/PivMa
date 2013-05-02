using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MiniAmazon.Web.Models
{
    public class CategoriesInputModel
    {
        public virtual long Id { get; set; }


        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es requerido.")]
        //[Remote("jsIsCategoryInUse", "Management", ErrorMessage = "El nombre de la categoria ya existe")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Debe ser mayor a 2 caracteres y menor de 50")]
        public virtual string Name { get; set; }
        
        
        [Display(Name = "Descripción")]
        public virtual string Description { get; set; }

        [Display(Name = "Fecha de registro")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Required(ErrorMessage = "La fecha es requerida.")]
        [DataType(DataType.Date, ErrorMessage = "Formato de fecha incorrecto.")]
        public virtual DateTime CreateDateTime { get; set; }

        //public virtual int IdParent { get; set; }


        //public virtual bool Active { get; set; }
    }
}
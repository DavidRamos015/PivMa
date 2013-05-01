using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MiniAmazon.Web.Models
{
    public class CategoriesInputModel
    {
        public virtual long Id { get; set; }
        

        [Required]
        [Display(Name = "Nombre")]
        [StringLength(50, ErrorMessage = "El titulo es requerido. Debe ser mayor a 2 caracteres y menor de 50")]
        public virtual string Name { get; set; }


        [Required]
        [Display(Name = "Descripción")]
        [StringLength(500, ErrorMessage = "La descripción es requerida. Debe ser menor de 300 caractereres")]
        public virtual string Description { get; set; }


        [Display(Name = "Fecha de registro")]
        //[DisplayFormat(DataFormatString = "{0:d}")]
        [Required(ErrorMessage = "La fecha es requerida.")]
        [DataType(DataType.Date, ErrorMessage = "Formato de fecha incorrecto.")]
        public virtual DateTime CreateDateTime { get; set; }

        //public virtual int IdParent { get; set; }


        //public virtual bool Active { get; set; }
    }
}
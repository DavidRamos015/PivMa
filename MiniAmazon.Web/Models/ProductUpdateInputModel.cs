using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MiniAmazon.Web.Models
{
    public class ProductUpdateInputModel : ProductInputModel
    {
        //public new virtual long Id { get; set; }

        [Display(Name = "Fecha de solicitud")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Required(ErrorMessage = "La fecha es requerida.")]
        [Editable(false)]
        [DataType(DataType.Date, ErrorMessage = "Formato de fecha incorrecto.")]
        public virtual DateTime CreateDateTimePendingChange { get; set; }


        [Display(Name = "Razón del cambio")]
        [Required(ErrorMessage = "Los comentarios sobre el cambio son requeridos.")]
        [StringLength(1000, ErrorMessage = "Contenido no debe sobrepasar los 1000 caracteres.")]
        public virtual string Comments { get; set; }

        [Display(Name = "Producto")]
        [Required(ErrorMessage = "El Producto es requerido")]
        [Editable(false)]
        public virtual long ProductId { get; set; }
        
    }
}
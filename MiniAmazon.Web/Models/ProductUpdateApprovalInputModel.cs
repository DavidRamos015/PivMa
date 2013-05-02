using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MiniAmazon.Web.Models
{
    public class ProductUpdateApprovalInputModel : ProductUpdateInputModel
    {
        [Display(Name = "Aprobar")]
        [Required(ErrorMessage = "Es requerido.")]
        public virtual bool Approved { get; set; }


        [Display(Name = "Comentarios")]
        [Required(ErrorMessage = "Comentarios son requeridos")]
        [StringLength(1000, ErrorMessage = "Contenido no debe sobrepasar los 1000 caracteres.")]
        public virtual string CommentsWhyNotApproved { get; set; }

    }
}
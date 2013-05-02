using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MiniAmazon.Web.Models
{
    public class ConfirmationsInputModel
    {
        [Display(Name = "Codigo de confirmación")]
        [Required(ErrorMessage = "Codigo de verificación es requerido.")]
        public virtual string CodeToConfirm { get; set; }
    }
}
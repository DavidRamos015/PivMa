using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MiniAmazon.Web.Models
{
    public class MyAccountWishListInputModel
    {
        public virtual long Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es requerido. ")]
        [StringLength(30, ErrorMessage = "Debe ser menor de 30 caractereres")]
        public virtual string Name { get; set; }
    }
}
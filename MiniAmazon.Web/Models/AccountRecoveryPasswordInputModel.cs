using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MiniAmazon.Web.Models
{
    public class AccountRecoveryPasswordInputModel
    {
        [Required]
        [Display(Name = "Correo electronico")]
        [Editable(true)]
        public virtual string Email { get; set; }


    }
}
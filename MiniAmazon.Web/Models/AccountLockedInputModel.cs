using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MiniAmazon.Web.Models
{
    public class AccountLockedInputModel
    {
        public virtual long Id { get; set; }


        [Required]
        [Display(Name = "Nombre")]
        [Editable(false)]
        public virtual string Name { get; set; }

        [Required]
        [Display(Name = "Correo electronico")]
        [Editable(false)]
        public virtual string Email { get; set; }


        [Required]
        [Display(Name = "Eliminar")]
        public virtual bool Active { get; set; }


        [Required]
        [Display(Name = "Bloqueado", ShortName = "Bloquear")]
        public virtual bool Locked { get; set; }

        [Required]
        [Editable(false)]
        [Display(Name = "Confirmación Pendiente", ShortName = "Confirmación")]
        public virtual bool PendingConfirmation { get; set; }

        [Required(ErrorMessage = "Campo requerido.")]
        [Display(Name = "Razones/Observaciones")]
        public virtual string Comments { get; set; }

    }
}
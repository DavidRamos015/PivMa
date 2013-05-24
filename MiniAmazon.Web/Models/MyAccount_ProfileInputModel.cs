using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MiniAmazon.Web.Models
{
    public class MyAccount_ProfileInputModel
    {
       
            public long Id { get; set; }

            [Display(Name = "Nombre")]
            [Required(ErrorMessage = "El nombre es requerido. ")]
            [StringLength(30, ErrorMessage = "Debe ser menor de 30 caractereres")]
            public string Name { get; set; }


            [Display(Name = "Edad")]
            [Required(ErrorMessage = "Edad es requerida.")]
            [DataType(DataType.Currency, ErrorMessage = "Formato incorrecto")]
            [Range(12, 150)]
            public int Age { get; set; }

            [Required(ErrorMessage = "Genero es requerido.")]
            [Display(Name = "Genero")]
            [StringLength(1, ErrorMessage = "Genero es requerido, Escriba F ó M")]
            public string Genre { get; set; }

            [Required(ErrorMessage = "Pais es requerido.")]
            [Display(Name = "País")]
            public int CountryId { get; set; }


            [Display(Name = "Contraseña actual")]
            [DataType(DataType.Password)]
            public string Password_Old { get; set; }

            [Display(Name = "Contraseña")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Confirmar contraseña")]
            [DataType(DataType.Password)]
            public string PasswordConfirm { get; set; }

            [Display(Name = "Foto de perfil")]
            public string Picture { get; set; }

            [Display(Name = "Enlaces web")]
            public string WenSite1 { get; set; }

            [Display(Name = "Enlaces web")]
            public string WenSite2 { get; set; }

            [Display(Name = "Enlaces web")]
            public string WenSite3 { get; set; }

            [Display(Name = "Enlaces web")]
            public string WenSite4 { get; set; }

            [Display(Name = "Datos de contacto publicos")]
            public string PublicEmail { get; set; }


            [Display(Name = "Acerca")]
            public string About { get; set; }

        
    }
}
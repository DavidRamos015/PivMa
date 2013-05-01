using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace MiniAmazon.Web.Models
{
    public class AccountInputModel
    {

        public long Id { get; set; }


        [Required]
        [Display(Name = "Nombre")]
        [StringLength(30, ErrorMessage = "El nombre es requerido. Debe ser menor de 30 caractereres")]
        public string Name { get; set; }


        [Required]
        [Display(Name = "Correo electronico")]
        [StringLength(100)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Correo es requerido, El correo tiene un formato invalido,Debe ser menor de 100 caractereres.")]
        //[Remote("CheckIfExist", "Account")]
        [Remote("ValidateEmail", "Account", ErrorMessage = "El correo ya esta registrado, intente con otro.")]
        public string Email { get; set; }


        [Display(Name = "Edad")]
        [Required]
        [Range(12, 150)]
        [DataType(DataType.Currency, ErrorMessage = "Edad es requerida.")]
        public int Age { get; set; }

        [Display(Name = "Genero")]
        [Required]
        [StringLength(1, ErrorMessage = "Genero es requerido, Escriba F ó M")]
        public string Genre { get; set; }


        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Contraseña requerida")]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Longitud de la contraseña es invalida")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirmar contraseña")]
        [Required(ErrorMessage = "Confirmacion de contraseña es requerida")]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Longitud de la contraseña es invalida")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

    }
}
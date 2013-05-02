using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace MiniAmazon.Web.Models
{
    public class AccountInputModel
    {

        public long Id { get; set; }


        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es requerido. ")]
        [StringLength(30, ErrorMessage = "Debe ser menor de 30 caractereres")]
        public string Name { get; set; }


        [Display(Name = "Correo electronico")]
        [Required(ErrorMessage = "Correo es requerido.")]
        [StringLength(100)]
        [DataType(DataType.EmailAddress, ErrorMessage = "El correo tiene un formato invalido,Debe ser menor de 100 caractereres.")]
//        [Remote("jsExistingUserEmail", "Management", ErrorMessage = "El correo ya esta registrado, intente con otro.")]
        public string Email { get; set; }


        [Display(Name = "Edad")]
        [Required(ErrorMessage = "Edad es requerida.")]
        [DataType(DataType.Currency, ErrorMessage = "Formato incorrecto")]
        [Range(12, 150)]
        public int Age { get; set; }

        [Required(ErrorMessage = "Genero es requerido.")]
        [Display(Name = "Genero")]
        [StringLength(1, ErrorMessage = "Genero es requerido, Escriba F � M")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "Pais es requerido.")]
        [Display(Name = "Pa�s")]
        public int CountryId { get; set; }


        [Display(Name = "Contrase�a")]
        [Required(ErrorMessage = "Contrase�a requerida")]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Longitud de la contrase�a es invalida")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirmar contrase�a")]
        [Required(ErrorMessage = "Confirmacion de contrase�a es requerida")]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Longitud de la contrase�a es invalida")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

    }
}
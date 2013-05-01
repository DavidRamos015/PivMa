namespace MiniAmazon.Web.Models
{
    public class AccountSignInModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
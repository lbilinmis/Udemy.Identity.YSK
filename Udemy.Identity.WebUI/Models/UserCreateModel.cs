using System.ComponentModel.DataAnnotations;

namespace Udemy.Identity.WebUI.Models
{
    public class UserCreateModel
    {
        [Required(ErrorMessage ="Kullanıcı adını giriniz")]
        public string UserName { get; set; }
        [EmailAddress(ErrorMessage ="Doğru formatta email girniz")]
        [Required(ErrorMessage = "Email alanın giriniz")]

        public string EMail { get; set; }

        [Required(ErrorMessage = "Password")]

        public string Password { get; set; }

        [Compare("Password",ErrorMessage ="Parolalar eşleşmiyor")]
        public string ConfirmPassword { get; set; }
        public string Gender { get; set; }

    }
}
    
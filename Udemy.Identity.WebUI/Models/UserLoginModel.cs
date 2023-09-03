using System.ComponentModel.DataAnnotations;

namespace Udemy.Identity.WebUI.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage ="Kullanıcı adını giriniz")]
        public string UserName { get; set; }
      
        [Required(ErrorMessage = "Password giriniz")]

        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }


    }
}
    
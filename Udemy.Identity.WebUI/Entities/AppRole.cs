using Microsoft.AspNetCore.Identity;

namespace Udemy.Identity.WebUI.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public DateTime CreatedTime { get; set; }
    }
}

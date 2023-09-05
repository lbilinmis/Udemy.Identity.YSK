using Microsoft.AspNetCore.Identity;

namespace Udemy.Identity.WebUI.CustomDescriber
{
    public class CustomErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError PasswordTooShort(int length)
        {
            return new()
            {
                Code = "PasswordTooShort",
                Description = $"Parola en az {length} karakterden oluşmalıdır"
            };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new()
            {
                Code = "PasswordRequiresNonAlphanumeric",
                Description = $"Parola en az  bir alfa numeric (~!+- vs.) karakter içermelidir"
            };

        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new()
            {
                Code = "DuplicateUserName",
                Description = $"Bu {userName} kullanıcı adı daha önce kullanılmış"
            };
        }
    }
}

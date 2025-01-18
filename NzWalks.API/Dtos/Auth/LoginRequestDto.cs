using System.ComponentModel.DataAnnotations;

namespace NzWalks.API.Dtos.Auth
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Username is required")]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

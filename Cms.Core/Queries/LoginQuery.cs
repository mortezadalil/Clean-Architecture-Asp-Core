using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Cms.Core.Queries
{
    public class LoginInputVm
    {
        [Required(ErrorMessage = "ایمیل لازم است.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "کلمه عبور لازم است.")]
        public string Password { get; set; }
    }


    public class LoginInputQuery : IRequest<LoginResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public string Token { get; set; }
    }
}

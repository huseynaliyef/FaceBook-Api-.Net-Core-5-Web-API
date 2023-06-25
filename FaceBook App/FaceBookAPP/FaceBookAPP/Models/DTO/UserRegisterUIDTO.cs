using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FaceBookAPP.Models.DTO
{
    public class UserRegisterUIDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [RegularExpression("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public IFormFile UserPhoto { get; set; }    

    }
}

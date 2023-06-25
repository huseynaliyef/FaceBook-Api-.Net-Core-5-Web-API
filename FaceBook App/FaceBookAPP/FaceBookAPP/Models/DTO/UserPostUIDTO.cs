using Microsoft.AspNetCore.Http;

namespace FaceBookAPP.Models.DTO
{
    public class UserPostUIDTO
    {
        public IFormFile Photo { get; set; }
        public string Description { get; set; }
    }
}

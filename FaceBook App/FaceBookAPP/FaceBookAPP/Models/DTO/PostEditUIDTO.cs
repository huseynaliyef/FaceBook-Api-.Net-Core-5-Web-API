using Microsoft.AspNetCore.Http;

namespace FaceBookAPP.Models.DTO
{
    public class PostEditUIDTO
    {
        public int PostId { get; set; }
        public IFormFile Photo { get; set; }
        public string Desription { get; set; }
    }
}

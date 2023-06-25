using Microsoft.AspNetCore.Identity;

namespace FaceBookAPP.Entities
{
    public class Istifadeci:IdentityUser
    {
        public string? ProfilPhoto { get; set; }
    }
}

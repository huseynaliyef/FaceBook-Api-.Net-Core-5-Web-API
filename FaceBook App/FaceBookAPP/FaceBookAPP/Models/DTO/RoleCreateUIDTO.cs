using System.ComponentModel.DataAnnotations;

namespace FaceBookAPP.Models.DTO
{
    public class RoleCreateUIDTO
    {
        [Required]
        public string RoleName { get ; set; }
    }
}

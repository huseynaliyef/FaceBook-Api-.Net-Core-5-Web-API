using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace FaceBookAPP.Entities
{
    public class UserPost
    {
        public int Id { get; set; }
        [ForeignKey("Istifadeci")]
        public string IstifadeciId { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public Istifadeci Istifadeci { get; set; }
    }
}

using System.Threading.Tasks;

namespace FaceBookAPP.Services.Abstractions
{
    public interface IGenericPost
    {
        Task<string> GetPostBase64(int Id);
    }
}

using DALL_E.Models;

namespace DALL_E.Services
{
    public interface IConsulta
    {
        Task<ResponseModel> GenerateImage(Input input);
    }
}

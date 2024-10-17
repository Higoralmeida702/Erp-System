using ERP_System.Dto;
using ERP_System.Model;

namespace ERP_System.Services.AuthServices
{
    public interface IUsuarioAuthService
    {
        Task<Response<UsuarioDto>> Registrar (UsuarioDto usuarioDto);
        Task<Response<string>> Login(LoginDto loginDto);
    }
}

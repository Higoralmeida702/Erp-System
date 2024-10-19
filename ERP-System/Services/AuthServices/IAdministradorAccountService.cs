using ERP_System.Enum;
using ERP_System.Model;

namespace ERP_System.Services.AuthServices
{
    public interface IAdministradorAccount
    {
        Task<Response<string>> AtribuirCargo(int usuarioId, CargoEnum novoCargo);
        Task InicializarAdm();
    }
}

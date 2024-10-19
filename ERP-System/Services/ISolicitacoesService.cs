using ERP_System.Enum;
using ERP_System.Model;

namespace ERP_System.Services
{
    public interface ISolicitacoesService
    {
        Task<Response<string>> SolicitarMudancaDeCargo(int usuarioId, CargoEnum novoCargo);
        Task<Response<string>> AprovarSolicitacao(int solicitacaoId, bool aprovado);
        Task<List<SolicitacaoDeCargo>> ListarSolicitacoes();
    }
}

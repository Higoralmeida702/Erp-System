using ERP_System.Data;
using ERP_System.Enum;
using ERP_System.Model;
using Microsoft.EntityFrameworkCore;

namespace ERP_System.Services
{
    public class SolicitacoesService : ISolicitacoesService
    {
        private readonly ApplicationDbContext _context;

        public SolicitacoesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<string>> SolicitarMudancaDeCargo(int usuarioId, CargoEnum novoCargo)
        {
            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null)
            {
                return new Response<string> { Status = false, Mensagem = "Usuário não encontrado" };
            }

            var solicitacao = new SolicitacaoDeCargo
            {
                UsuarioId = usuarioId,
                CargoSolicitado = novoCargo,
                Status = SolicitacaoCargoEnum.Pendente
            };

            _context.Solicitacoes.Add(solicitacao);
            await _context.SaveChangesAsync();

            return new Response<string> { Status = true, Mensagem = "Solicitação enviada" };
        }

        public async Task<Response<string>> AprovarSolicitacao(int solicitacaoId, bool aprovado)
        {
            var solicitacao = await _context.Solicitacoes.FindAsync(solicitacaoId);
            if (solicitacao == null)
            {
                return new Response<string> { Status = false, Mensagem = "Solicitação não encontrada" };
            }

            solicitacao.Status = aprovado ? SolicitacaoCargoEnum.Aprovado : SolicitacaoCargoEnum.Rejeitado;

            if (aprovado)
            {
                var usuario = await _context.Usuarios.FindAsync(solicitacao.UsuarioId);
                if (usuario != null)
                {
                    usuario.Cargo = solicitacao.CargoSolicitado;
                }
            }

            _context.Solicitacoes.Update(solicitacao);
            await _context.SaveChangesAsync();

            return new Response<string> { Status = true, Mensagem = aprovado ? "Solicitação aprovada" : "Solicitação rejeitada" };
        }

        public async Task<List<SolicitacaoDeCargo>> ListarSolicitacoes()
        {
            return await _context.Solicitacoes.ToListAsync();
        }
    }
}
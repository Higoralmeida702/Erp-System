using ERP_System.Enum;
using ERP_System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP_System.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitacoesController : ControllerBase
    {
        private readonly ISolicitacoesService _solicitacoesService;

        public SolicitacoesController(ISolicitacoesService solicitacoesService)
        {
            _solicitacoesService = solicitacoesService;
        }

        [HttpPost("solicitar")]
        [Authorize(Roles = "Paciente,Médico,Recepcionista,Fornecedor,Funcionário,Administrador")]
        public async Task<IActionResult> SolicitarMudancaDeCargo(int usuarioId, CargoEnum novoCargo)
        {
            var response = await _solicitacoesService.SolicitarMudancaDeCargo(usuarioId, novoCargo);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpPost("aprovar/{solicitacaoId}")]
        [Authorize(Roles = "Administrador")]

        public async Task<IActionResult> AprovarSolicitacao(int solicitacaoId, bool aprovado)
        {
            var response = await _solicitacoesService.AprovarSolicitacao(solicitacaoId, aprovado);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("listar")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ListarSolicitacoes()
        {
            var solicitacoes = await _solicitacoesService.ListarSolicitacoes();
            return Ok(solicitacoes);
        }
    }
}

using ERP_System.Dto;
using ERP_System.Enum;
using ERP_System.Services.AuthServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP_System.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioAuthService _usuarioAuthService;
        private readonly IAdministradorAccount _administradorAccount;

        public UsuarioController(IUsuarioAuthService usuarioAuthService, IAdministradorAccount administradorAccount)
        {
            _usuarioAuthService = usuarioAuthService;
            _administradorAccount = administradorAccount;
        }

        [HttpPost("Registrar/Usuario")]
        public async Task<IActionResult> Register(UsuarioDto usuarioDto)
        {
            var resposta = await _usuarioAuthService.Registrar(usuarioDto);
            if (!resposta.Status)
            {
                return BadRequest(resposta);
            }
            return Ok(resposta);
        }

        [HttpPost("Login/Usuario")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var resposta = await _usuarioAuthService.Login(loginDto);
            if (!resposta.Status)
            {
                return BadRequest(resposta);
            }
            return Ok(resposta);
        }

        [HttpPut("AtribuirNovoCargo/{usuarioId}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> AtribuirCargo (int usuarioId, CargoEnum novoCargo)
        {
            var resposta = await _administradorAccount.AtribuirCargo(usuarioId, novoCargo);
            if (!resposta.Status)
            {
                return BadRequest(resposta);
            }
            return Ok(resposta);
        }
    }
}

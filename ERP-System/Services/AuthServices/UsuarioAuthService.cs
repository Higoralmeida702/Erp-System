using ERP_System.Data;
using ERP_System.Dto;
using ERP_System.Enum;
using ERP_System.Model;
using ERP_System.Services.SenhaAuthService;
using Microsoft.EntityFrameworkCore;

namespace ERP_System.Services.AuthServices
{
    public class UsuarioAuthService : IUsuarioAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly ISenhaService _senhaService;

        public UsuarioAuthService(ApplicationDbContext context, ISenhaService senhaService)
        {
            _context = context;
            _senhaService = senhaService;
        }

        public async Task<Response<string>> Login(LoginDto loginDto)
        {
            Response<string> respostaServico = new Response<string>();

            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(userBanco => userBanco.Email == loginDto.Email);
                if (usuario == null)
                {
                    respostaServico.Mensagem = "Credenciais inválidas!";
                    respostaServico.Status = false;
                    return respostaServico;
                }
                if (!_senhaService.VerificaSenhaHash(loginDto.Senha, usuario.PasswordHash, usuario.PasswordSalt))
                {
                    respostaServico.Mensagem = "Credenciais inválidas";
                    respostaServico.Status = false;
                    return respostaServico;
                }
                var token = _senhaService.CriarToken(usuario);
                respostaServico.Dados = token;
                respostaServico.Mensagem = "Usuário logado com sucesso!";
            }
            catch (Exception error)
            {
                respostaServico.Dados = null;
                respostaServico.Mensagem = error.Message;
                respostaServico.Status = false;
            }
            return respostaServico;
        }

        public async Task<Response<UsuarioDto>> Registrar(UsuarioDto usuarioDto)
        {
            Response<UsuarioDto> respostaServico = new Response<UsuarioDto>();
            try
            {
                if (await VerificaEmailJaCadastrado(usuarioDto))
                {
                    respostaServico.Dados = null;
                    respostaServico.Status = false;
                    respostaServico.Mensagem = "Usuário já cadastrado";
                    return respostaServico;
                }
                _senhaService.CriarSenhaHash(usuarioDto.Senha, out byte[] senhaHash, out byte[] senhaSalt);

                Usuario usuario = new Usuario()
                {
                    Nome = usuarioDto.Nome,
                    Sobrenome = usuarioDto.Sobrenome,
                    Email = usuarioDto.Email,
                    NumeroTelefone = usuarioDto.NumeroTelefone,
                    Endereco = usuarioDto.Endereco,
                    Cargo = CargoEnum.Paciente,
                    PasswordHash = senhaHash,
                    PasswordSalt = senhaSalt,
                };

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                respostaServico.Mensagem = "Usuário criado com sucesso";
                respostaServico.Status = true;
                respostaServico.Dados = usuarioDto;
            }
            catch (Exception error)
            {
                respostaServico.Dados = null;
                respostaServico.Mensagem = error.Message;
                respostaServico.Status = false;
            }

            return respostaServico;
        }

        public async Task<bool> VerificaEmailJaCadastrado(UsuarioDto usuarioDto)
        {
            return await _context.Usuarios.AnyAsync(userbanco => userbanco.Email == usuarioDto.Email);
        }
    }
}

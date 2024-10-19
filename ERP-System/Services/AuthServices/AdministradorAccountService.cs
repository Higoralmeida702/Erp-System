using ERP_System.Data;
using ERP_System.Enum;
using ERP_System.Model;
using ERP_System.Services.SenhaAuthService;

namespace ERP_System.Services.AuthServices
{
    public class AdministradorAccountService : IAdministradorAccount
    {
        private readonly ApplicationDbContext _context;
        private readonly ISenhaService _senhaService;

        public AdministradorAccountService(ApplicationDbContext context, ISenhaService senhaService)
        {
            _context = context;
            _senhaService = senhaService;
        }

        public async Task<Response<string>> AtribuirCargo(int usuarioId, CargoEnum novoCargo)
        {
            var resposta = new Response<string>();

            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null)
            {
                resposta.Status = false;
                resposta.Mensagem = "Usuário não encontrado";
                return resposta;
            }

            usuario.Cargo = novoCargo;
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            resposta.Dados = "Cargo atribuido com sucesso";
            resposta.Mensagem = "Operação realizada com sucesso";
            return resposta;
        }

        public async Task InicializarAdm()
        {
            if (!_context.Usuarios.Any(u => u.Cargo == CargoEnum.Administrador))
            {
                var admin = new Usuario
                {
                    Nome = "Admin",
                    Sobrenome = "Adm",
                    Email = "admin@gmail.com",
                    NumeroTelefone = "123456789",
                    Endereco = "Casa segura",
                    Cargo = CargoEnum.Administrador,
                    CriacaoDeConta = DateTime.Now,
                };

                _senhaService.CriarSenhaHash("senhasegura123", out byte[] passwordHash, out byte[] passwordSalt);

                admin.PasswordHash = passwordHash;
                admin.PasswordSalt = passwordSalt;

                _context.Usuarios.Add(admin);
                await _context.SaveChangesAsync();
            }
        }
    }
}

namespace ERP_System.Services.SenhaAuthService
{
    public interface ISenhaService
    {
        void CriarSenhaHash (string senha, out byte[] senhaHash, out byte[] senhaSalt);
        bool VerificaSenhaHash(string senha, byte[] senhaHash, byte[] senhaSalt);
        string CriarToken<T>(T usuario) where T : class;
    }
}

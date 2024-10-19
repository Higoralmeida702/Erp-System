using ERP_System.Enum;

namespace ERP_System.Model
{
    public class SolicitacaoDeCargo
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public CargoEnum CargoSolicitado { get; set; }
        public bool Aprovado { get; set; } 
        public DateTime DataSolicitacao { get; set; } = DateTime.Now;
        public SolicitacaoCargoEnum Status { get; set; } = SolicitacaoCargoEnum.Pendente;

        public Usuario Usuario { get; set; }
    }
}

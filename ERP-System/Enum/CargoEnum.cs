using System.ComponentModel.DataAnnotations;

namespace ERP_System.Enum
{
    public enum CargoEnum
    {
        [Display(Name = "Paciente")]
        Paciente = 1,

        [Display(Name = "Médico")]
        Médico = 2,

        [Display(Name = "Recepcionista")]
        Recepcionista = 3,

        [Display(Name = "Fornecedor")]
        Fornecedor = 4,

        [Display(Name = "Funcionário")]
        Funcionário = 5,

        [Display(Name = "Administrador")]
        Administrador = 6,


    }
}

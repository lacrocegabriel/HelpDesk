using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Domain.Entities.Enums
{
    public enum TipoPessoa 
    {
        [Display(Name = "Pessoa Jurídica")]
        PessoaJuridica = 1,
        [Display(Name = "Pessoa Física")]
        PessoaFisica = 2
    }
}

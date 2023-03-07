using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Business.Models.Enums
{
    public enum TipoPessoa 
    {
        [Display(Name = "Pessoa Jurídica")]
        PessoaJuridica = 1,
        [Display(Name = "Pessoa Física")]
        PessoaFisica = 2
    }
}

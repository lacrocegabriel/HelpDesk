using HelpDesk.Business.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Business.Models
{
    public class Pessoa : Entity
    {
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimentoConstituicao { get; set; }
        public long IdTipoPessoa { get; set; }
        public Guid IdEndereco { get; set; }

        // EF Relations
        public Endereco Endereco { get; set; }
        public TipoPessoa TipoPessoa { get; set; }
    }
}

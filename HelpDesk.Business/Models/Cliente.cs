using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Business.Models
{
    public class Cliente : Pessoa
    {

        public Guid IdGerenciador { get; set; }

        // EF Relations
        public Pessoa Pessoa { get; set; }
        public Gerenciador Gerenciador { get; set; }
        public IEnumerable<UsuarioXCliente> UsuarioXClientes { get; set; }
        public IEnumerable<Usuario> Usuarios { get; set; }
        public IEnumerable<Chamado> Chamados { get; set; }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Business.Models
{
    public class Tramite : Entity
    {
        public string Descricao { get; set; }
        public Guid IdUsuarioGerador { get; set; }
        public Guid IdChamado { get; set; }
        public Chamado Chamado { get; set; }
        public Usuario UsuarioGerador { get; set; }
    }
}

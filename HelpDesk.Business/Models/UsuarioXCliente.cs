using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Business.Models
{
    public class UsuarioXCliente
    {
        public Guid IdUsuario { get; set; }
        public Guid IdCliente { get; set; }
        public Usuario Usuario { get; set; }
        public Cliente Cliente { get; set; }

    }


}

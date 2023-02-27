using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Models;
using HelpDesk.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Data.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(HelpDeskContext db) : base(db)
        {
        }
    }
}

using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Models;
using HelpDesk.Data.Context;

namespace HelpDesk.Data.Repository
{
    public class GerenciadorRepository : Repository<Gerenciador>, IGerenciadorRepository
    {
        public GerenciadorRepository(HelpDeskContext db) : base(db)
        {
        }
    }
}

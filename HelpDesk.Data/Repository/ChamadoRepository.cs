using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Models;
using HelpDesk.Data.Context;

namespace HelpDesk.Data.Repository
{
    public class ChamadoRepository : Repository<Chamado>, IChamadoRepository
    {
        public ChamadoRepository(HelpDeskContext db) : base(db){ }
    }
}

using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Models;
using HelpDesk.Data.Context;

namespace HelpDesk.Data.Repository
{
    public class TramiteRepository : Repository<Tramite>, ITramiteRepository
    {
        public TramiteRepository(HelpDeskContext db) : base(db)
        {
        }
    }
}

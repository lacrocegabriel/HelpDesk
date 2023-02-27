using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Models;
using HelpDesk.Data.Context;

namespace HelpDesk.Data.Repository
{
    public class SetorRepository : Repository<Setor>, ISetorRepository
    {
        public SetorRepository(HelpDeskContext db) : base(db)
        {
        }
    }
}

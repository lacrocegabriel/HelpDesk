using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Models;
using HelpDesk.Data.Context;

namespace HelpDesk.Data.Repository
{
    public class PessoaRepository : Repository<Pessoa>, IPessoaRepository
    {
        public PessoaRepository(HelpDeskContext context) : base(context)
        {
        }
    }
}

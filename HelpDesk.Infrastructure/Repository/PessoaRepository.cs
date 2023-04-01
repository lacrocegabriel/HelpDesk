using HelpDesk.Domain.Interfaces.Repositories;
using HelpDesk.Domain.Models;
using HelpDesk.Infrastructure.Data.Context;

namespace HelpDesk.Infrastructure.Data.Repository
{
    public class PessoaRepository : Repository<Pessoa>, IPessoaRepository
    {
        public PessoaRepository(HelpDeskContext context) : base(context)
        {
        }
    }
}

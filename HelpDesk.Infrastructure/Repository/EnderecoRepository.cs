using HelpDesk.Domain.Interfaces.Repositories;
using HelpDesk.Domain.Models;
using HelpDesk.Infrastructure.Data.Context;

namespace HelpDesk.Infrastructure.Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(HelpDeskContext db) : base(db)
        {
        }
    }
}

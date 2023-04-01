using HelpDesk.Domain.Entities;
using HelpDesk.Domain.Interfaces.Repositories;
using HelpDesk.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Infrastructure.Data.Repository
{
    public class SetorRepository : Repository<Setor>, ISetorRepository
    {
        public SetorRepository(HelpDeskContext db) : base(db)
        {
        }

        public async Task<IEnumerable<Setor>> ObterSetoresPorPermissao(List<Guid> idGerenciadores, int skip, int take)
        {
            return await Db.Setores.AsNoTracking()
                        .Where(x => idGerenciadores.Contains(x.IdGerenciador))
                        .Skip(skip)
                        .Take(take)
                        .ToListAsync();
        }
    }
}

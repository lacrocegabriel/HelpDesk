using HelpDesk.Domain.Entities;
using HelpDesk.Domain.Interfaces.Repositories;
using HelpDesk.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Infrastructure.Data.Repository
{
    public class GerenciadorRepository : Repository<Gerenciador>, IGerenciadorRepository
    {
        public GerenciadorRepository(HelpDeskContext db) : base(db)
        {
        }

        public async Task<IEnumerable<Gerenciador>> ObterGerenciadoresPorPermissao(List<Guid> idGerenciadores, int skip, int take)
        {
            return await Db.Gerenciadores.AsNoTracking()
                        .Where(x => idGerenciadores.Contains(x.Id))
                        .Skip(skip)
                        .Take(take)
                        .ToListAsync();
        }

    }
}

using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Models;
using HelpDesk.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HelpDesk.Data.Repository
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

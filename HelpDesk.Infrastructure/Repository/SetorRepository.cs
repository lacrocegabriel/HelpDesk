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
                        .Include(s => s.Gerenciador)
                        .Skip(skip)
                        .Take(take)
                        .ToListAsync();
        }
        public async Task<Setor> ObterSetorGerenciador(Guid idSetor)
        {
            var setor = await Db.Setores.AsNoTracking()
                .Where(c => c.Id == idSetor)
                .Include(c => c.Gerenciador)
                .FirstOrDefaultAsync();

            return setor;

        }
    }
}

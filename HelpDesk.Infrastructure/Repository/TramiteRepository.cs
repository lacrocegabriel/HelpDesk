using HelpDesk.Domain.Entities;
using HelpDesk.Domain.Interfaces.Repositories;
using HelpDesk.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Infrastructure.Data.Repository
{
    public class TramiteRepository : Repository<Tramite>, ITramiteRepository
    {
        public TramiteRepository(HelpDeskContext db) : base(db)
        {
        }
        public async Task<IEnumerable<Tramite>> ObterTramitesPorPermissao(List<Guid> idGerenciadores, List<Guid> idClientes, int skip, int take)
        {
            return  await Db.Tramites.AsNoTracking()
                    .Include(t => t.Chamado)
                    .Include(t => t.UsuarioGerador)
                    .Where(x => idGerenciadores.Contains(x.Chamado.IdGerenciador) && idClientes.Contains(x.Chamado.IdCliente))
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync();
        }
        public async Task<Tramite> ObterTramiteChamado(Guid idTramite)
        {
            return await Db.Tramites.AsNoTracking()
                .Include(t => t.Chamado)
                .Include(t => t.UsuarioGerador)
                .Where(t => t.Id == idTramite)
                .FirstOrDefaultAsync();
        }
        public async Task AdicionarTramite(Tramite tramite)
        {
            Db.Chamados.Attach(tramite.Chamado);

            Db.Entry(tramite.Chamado).Property(c => c.IdSituacaoChamado).IsModified = true;
            
            Db.Tramites.Add(tramite);
            await SaveChanges();
            
        }

        public async Task AtualizarTramite(Tramite tramite)
        {
            Db.Chamados.Attach(tramite.Chamado);

            Db.Entry(tramite.Chamado).Property(c => c.IdSituacaoChamado).IsModified = true;

            Db.Tramites.Update(tramite);

            await SaveChanges();
        }
    }
}

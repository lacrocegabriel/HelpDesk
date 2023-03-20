using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Models;
using HelpDesk.Data.Context;
using HelpDesk.Data.Migrations;

namespace HelpDesk.Data.Repository
{
    public class TramiteRepository : Repository<Tramite>, ITramiteRepository
    {
        public TramiteRepository(HelpDeskContext db) : base(db)
        {
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

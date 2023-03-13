using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Models;
using HelpDesk.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Data.Repository
{
    public class ChamadoRepository : Repository<Chamado>, IChamadoRepository
    {
        public ChamadoRepository(HelpDeskContext db) : base(db){ }

        public async Task<IEnumerable<Chamado>> ObterChamadosPorUsuarioResponsavel(Guid idUsuario)
        {
            return await Db.Chamados.AsNoTracking()
                .Where(c => c.IdUsuarioResponsavel == idUsuario)
                .ToListAsync();
        }

        public async Task<IEnumerable<Chamado>> ObterChamadosPorUsuarioGerador(Guid idUsuario)
        {
            return await Db.Chamados.AsNoTracking()
                .Where(c => c.IdUsuarioGerador == idUsuario)
                .ToListAsync();
        }
    }
}

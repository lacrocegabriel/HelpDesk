using HelpDesk.Domain.Entities;
using HelpDesk.Domain.Interfaces.Repositories;
using HelpDesk.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Infrastructure.Data.Repository
{
    public class ChamadoRepository : Repository<Chamado>, IChamadoRepository
    {
        public ChamadoRepository(HelpDeskContext db) : base(db){ }

        public async Task<IEnumerable<Chamado>> ObterChamadosPorPermissao(List<Guid> idGerenciadores, List<Guid> idClientes, int skip, int take)
        {
            return await Db.Chamados.AsNoTracking()
                        .Where(x => idGerenciadores.Contains(x.IdGerenciador) && idClientes.Contains(x.IdCliente))
                        .Include(c => c.Gerenciador)
                        .Include(c => c.Cliente)
                        .Include(c => c.UsuarioGerador)
                        .Include(c => c.UsuarioResponsavel)
                        .Include(c => c.SituacaoChamado)
                        .Skip(skip)
                        .Take(take)
                        .ToListAsync();            
        }

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
        public async Task<Chamado> ObterChamadoGeradorClienteUsuario(Guid idChamado)
        {
            var chamado = await Db.Chamados.AsNoTracking()
                .Where(c => c.Id == idChamado)
                .Include(c => c.Gerenciador)
                .Include(c => c.Cliente)
                .Include(c => c.UsuarioGerador)
                .Include(c => c.UsuarioResponsavel)
                .Include(c => c.SituacaoChamado)
                .FirstOrDefaultAsync();
            
            return chamado;

        }

    }
}

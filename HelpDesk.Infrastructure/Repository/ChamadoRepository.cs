using HelpDesk.Domain.Interfaces.Repositories;
using HelpDesk.Domain.Models;
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
        public async Task<Chamado> ObterChamadoGeradorClienteUsuario(Chamado chamado)
        {
            chamado.Gerenciador = await Db.Gerenciadores.AsNoTracking().Where(g => g.Id == chamado.IdGerenciador).FirstAsync();
            chamado.Cliente = await Db.Clientes.AsNoTracking().Where(c => c.Id == chamado.IdCliente).FirstAsync();
            chamado.UsuarioGerador = await Db.Usuarios.AsNoTracking().Where(u => u.Id == chamado.IdUsuarioGerador).FirstAsync();
            chamado.UsuarioResponsavel = await Db.Usuarios.AsNoTracking().Where(u => u.Id == chamado.IdUsuarioResponsavel).FirstAsync();

            return chamado;

        }
    }
}

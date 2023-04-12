using HelpDesk.Domain.Entities;
using HelpDesk.Domain.Interfaces.Repositories;
using HelpDesk.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Infrastructure.Data.Repository
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(HelpDeskContext db) : base(db){}

        public async Task<IEnumerable<Cliente>> ObterClientesPorPermissao(List<Guid> idGerenciadores, int skip, int take)
        {
            return await Db.Clientes.AsNoTracking()
                        .Where(x => idGerenciadores.Contains(x.IdGerenciador))
                        .Include(c => c.Gerenciador)
                        .Skip(skip)
                        .Take(take)
                        .ToListAsync();
        }
        public async Task<Cliente> ObterClienteGerenciador(Guid idCliente)
        {
            var cliente = await Db.Clientes.AsNoTracking()
                .Where(c => c.Id == idCliente)
                .Include(c => c.Gerenciador)
                .FirstOrDefaultAsync();

            return cliente;

        }
    }
}

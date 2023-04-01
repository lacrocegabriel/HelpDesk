using HelpDesk.Domain.Models;

namespace HelpDesk.Domain.Interfaces.Repositories
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<IEnumerable<Cliente>> ObterClientesPorPermissao(List<Guid> idGerenciadores, int skip, int take);
    }
}

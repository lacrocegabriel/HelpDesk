using HelpDesk.Business.Models;

namespace HelpDesk.Business.Interfaces.Repositories
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<IEnumerable<Cliente>> ObterClientesPorPermissao(List<Guid> idGerenciadores, int skip, int take);
    }
}

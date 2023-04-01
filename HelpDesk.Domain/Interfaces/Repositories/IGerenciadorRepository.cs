using HelpDesk.Domain.Entities;

namespace HelpDesk.Domain.Interfaces.Repositories
{
    public interface IGerenciadorRepository : IRepository<Gerenciador>
    {
        Task<IEnumerable<Gerenciador>> ObterGerenciadoresPorPermissao(List<Guid> idGerenciadores, int skip, int take);
    }
}

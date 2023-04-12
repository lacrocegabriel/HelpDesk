using HelpDesk.Domain.Entities;

namespace HelpDesk.Domain.Interfaces.Repositories
{
    public interface ISetorRepository : IRepository<Setor>
    {
        Task<IEnumerable<Setor>> ObterSetoresPorPermissao(List<Guid> idGerenciadores, int skip, int take);
        Task<Setor> ObterSetorGerenciador(Guid idSetor);
    }
}

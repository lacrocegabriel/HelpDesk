using HelpDesk.Business.Models;

namespace HelpDesk.Business.Interfaces.Repositories
{
    public interface ISetorRepository : IRepository<Setor>
    {
        Task<IEnumerable<Setor>> ObterSetoresPorPermissao(List<Guid> idGerenciadores, int skip, int take);
    }
}

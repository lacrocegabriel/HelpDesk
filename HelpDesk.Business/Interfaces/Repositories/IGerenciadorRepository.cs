using HelpDesk.Business.Models;
using System.Linq.Expressions;

namespace HelpDesk.Business.Interfaces.Repositories
{
    public interface IGerenciadorRepository : IRepository<Gerenciador>
    {
        Task<IEnumerable<Gerenciador>> ObterGerenciadoresPorPermissao(List<Guid> idGerenciadores, int skip, int take);
    }
}

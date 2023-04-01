using HelpDesk.Domain.Entities;

namespace HelpDesk.Application.Interface
{
    public interface ISetorAppService : IAppServiceBase<Setor>
    {
        Task Adicionar(Setor setor);
        Task Atualizar(Setor setor);
        Task Remover(Guid id);
        Task<IEnumerable<Setor>> ObterTodos(int skip, int take);
        Task<Setor?> ObterPorId(Guid id);
    }
}

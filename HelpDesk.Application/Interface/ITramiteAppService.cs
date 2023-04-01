using HelpDesk.Domain.Entities;

namespace HelpDesk.Application.Interface
{
    public interface ITramiteAppService : IAppServiceBase<Tramite>
    {
        Task Adicionar(Tramite tramite);
        Task Atualizar(Tramite tramite);
        Task<IEnumerable<Tramite>> ObterTodos(int skip, int take);
        Task<Tramite?> ObterPorId(Guid id);
    }
}

using HelpDesk.Domain.Entities;

namespace HelpDesk.Application.Interface
{
    public interface IChamadoAppService : IAppServiceBase<Chamado>
    {
        Task Adicionar(Chamado chamado);
        Task Atualizar(Chamado chamado);
        Task<IEnumerable<Chamado>> ObterTodos(int skip, int take);
        Task<Chamado?> ObterPorId(Guid id);
    }
}

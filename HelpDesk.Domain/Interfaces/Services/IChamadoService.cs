using HelpDesk.Domain.Models;

namespace HelpDesk.Domain.Interfaces.Services
{
    public interface IChamadoService : IDisposable
    {
        Task Adicionar(Chamado chamado);
        Task Atualizar(Chamado chamado);
        Task<IEnumerable<Chamado>> ObterTodos(int skip, int take);
        Task<Chamado?> ObterPorId(Guid id);
    }
}

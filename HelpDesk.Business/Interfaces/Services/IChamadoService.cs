using HelpDesk.Business.Models;

namespace HelpDesk.Business.Interfaces.Services
{
    public interface IChamadoService : IDisposable
    {
        Task Adicionar(Chamado chamado);
        Task Atualizar(Chamado chamado);
        Task<IEnumerable<Chamado>> ObterTodos(int skip, int take);
    }
}

using HelpDesk.Business.Models;

namespace HelpDesk.Business.Interfaces.Services
{
    public interface ISetorService : IDisposable
    {
        Task Adicionar(Setor setor);
        Task Atualizar(Setor setor);
        Task Remover(Guid id);
        Task<IEnumerable<Setor>> ObterTodos(int skip, int take);
        Task<Setor?> ObterPorId(Guid id);

    }
}

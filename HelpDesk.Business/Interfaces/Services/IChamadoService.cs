using HelpDesk.Business.Models;

namespace HelpDesk.Business.Interfaces.Services
{
    public interface IChamadoService : IDisposable
    {
        Task Adicionar(Chamado chamado);
        Task Atualizar(Chamado chamado);
        Task Remover(Guid id);
    }
}

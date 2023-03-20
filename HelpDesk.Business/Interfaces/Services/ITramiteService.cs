using HelpDesk.Business.Models;

namespace HelpDesk.Business.Interfaces.Services
{
    public interface ITramiteService : IDisposable
    {
        Task Adicionar(Tramite tramite);
        Task Atualizar(Tramite tramite);
    }
}

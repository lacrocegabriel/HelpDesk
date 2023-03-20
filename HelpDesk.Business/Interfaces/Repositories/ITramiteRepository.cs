using HelpDesk.Business.Models;

namespace HelpDesk.Business.Interfaces.Repositories
{
    public interface ITramiteRepository : IRepository<Tramite>
    {
        Task AdicionarTramite(Tramite tramite);
        Task AtualizarTramite(Tramite tramite);
    }
}


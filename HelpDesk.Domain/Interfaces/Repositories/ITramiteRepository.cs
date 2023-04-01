using HelpDesk.Domain.Entities;

namespace HelpDesk.Domain.Interfaces.Repositories
{
    public interface ITramiteRepository : IRepository<Tramite>
    {
        Task AdicionarTramite(Tramite tramite);
        Task AtualizarTramite(Tramite tramite);
        Task<IEnumerable<Tramite>> ObterTramitesPorPermissao(List<Guid> idGerenciadores, List<Guid> idClientes, int skip, int take);
        Task<Tramite> ObterTramiteChamado(Guid idTramite);
    }
}


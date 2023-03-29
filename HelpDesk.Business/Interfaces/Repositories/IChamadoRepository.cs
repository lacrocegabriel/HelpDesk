using HelpDesk.Business.Models;

namespace HelpDesk.Business.Interfaces.Repositories
{
    public interface IChamadoRepository : IRepository<Chamado>
    {
        Task<IEnumerable<Chamado>> ObterChamadosPorPermissao(List<Guid> idGerenciadores, List<Guid> idClientes, int skip, int take);
        Task<IEnumerable<Chamado>> ObterChamadosPorUsuarioResponsavel(Guid idUsuario);
        Task<IEnumerable<Chamado>> ObterChamadosPorUsuarioGerador(Guid idUsuario);
        Task<Chamado> ObterChamadoGeradorClienteUsuario(Chamado chamado);
    }
}

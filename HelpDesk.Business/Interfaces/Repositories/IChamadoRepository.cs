using HelpDesk.Business.Models;

namespace HelpDesk.Business.Interfaces.Repositories
{
    public interface IChamadoRepository : IRepository<Chamado>
    {
        Task<IEnumerable<Chamado>> ObterChamadosPorPermissao(Usuario usuario);
        Task<IEnumerable<Chamado>> ObterChamadosPorUsuarioResponsavel(Guid idUsuario);
        Task<IEnumerable<Chamado>> ObterChamadosPorUsuarioGerador(Guid idUsuario);
        Task<Chamado> ObterChamadoGeradorClienteUsuario(Chamado chamado);
    }
}

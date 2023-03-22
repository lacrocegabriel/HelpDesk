using HelpDesk.Business.Models;

namespace HelpDesk.Business.Interfaces.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<(List<string> Erros,bool Adicionado)> AdicionarUsuario(Usuario usuario);
        Task AtualizarUsuario(Usuario usuario);
        Task<IEnumerable<Usuario>> ObterChamadosGeradorUsuario(Guid idUsuario);
        Task<IEnumerable<Usuario>> ObterChamadosResponsavelUsuario(Guid idUsuario);
        Task<IEnumerable<Usuario>> ObterTodosChamadosUsuario(Guid idUsuario);
        Task<IEnumerable<Usuario>> ObterUsuariosPorGerenciador(Guid idGerenciador);
        Task<IEnumerable<Usuario>> ObterUsuariosPorCliente(Guid idCliente);
        Task<IEnumerable<Usuario>> ObterUsuariosPorSetor(Guid idSetor);

    }
}

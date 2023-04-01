using HelpDesk.Domain.Entities;

namespace HelpDesk.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task AdicionarUsuario(Usuario usuario);
        Task AtualizarUsuario(Usuario usuario);
        Task<IEnumerable<Usuario>> ObterUsuariosPorPermissao(Usuario usuario, int skip, int take);
        Task<(List<Guid> IdGerenciadores, List<Guid> IdClientes)> ObterGerenciadoresClientesPermitidos(Guid idUsuario);
        Task<Usuario> ObterUsuarioGerenciadoresClientes(Guid id);
        Task<IEnumerable<Usuario>> ObterChamadosGeradorUsuario(Guid idUsuario);
        Task<IEnumerable<Usuario>> ObterChamadosResponsavelUsuario(Guid idUsuario);
        Task<IEnumerable<Usuario>> ObterTodosChamadosUsuario(Guid idUsuario);
        Task<IEnumerable<Usuario>> ObterUsuariosPorGerenciador(Guid idGerenciador);
        Task<IEnumerable<Usuario>> ObterUsuariosPorCliente(Guid idCliente);
        Task<IEnumerable<Usuario>> ObterUsuariosPorSetor(Guid idSetor);        
    }
}

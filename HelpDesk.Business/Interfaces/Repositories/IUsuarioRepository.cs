using HelpDesk.Business.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HelpDesk.Business.Interfaces.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<(List<string> Erros,bool Adicionado)> AdicionarUsuario(Usuario usuario);
        Task<(List<string> Erros, bool Atualizado)> AtualizarUsuario(Usuario usuario);
        Task<(IdentityUser User, IList<Claim> Claims, IList<string> Roles)> ObterUsuarioClaimsRoles(string login);
        Task<IEnumerable<Usuario>> ObterChamadosGeradorUsuario(Guid idUsuario);
        Task<IEnumerable<Usuario>> ObterChamadosResponsavelUsuario(Guid idUsuario);
        Task<IEnumerable<Usuario>> ObterTodosChamadosUsuario(Guid idUsuario);
        Task<IEnumerable<Usuario>> ObterUsuariosPorGerenciador(Guid idGerenciador);
        Task<IEnumerable<Usuario>> ObterUsuariosPorCliente(Guid idCliente);
        Task<IEnumerable<Usuario>> ObterUsuariosPorSetor(Guid idSetor);

    }
}

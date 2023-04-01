using HelpDesk.Domain.Entities;

namespace HelpDesk.Domain.Interfaces.Validators
{
    public interface IUsuarioValidator : IPessoaValidator<Usuario>, IDisposable
    {
        bool ValidaPermissaoVisualizacao(Usuario usuario, List<Guid> idGerenciadoresUsuario,List<Guid> idClientesUsuario);
        Task<bool> ValidaGerenciadoresClientesUsuario(IEnumerable<Gerenciador> Gerenciadores, IEnumerable<Cliente> Clientes);
        Task<bool> ValidaExclusaoUsuario(Guid idUsuario);
    }
}

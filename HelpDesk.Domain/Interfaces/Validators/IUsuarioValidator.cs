using HelpDesk.Domain.Models;

namespace HelpDesk.Domain.Interfaces.Validators
{
    public interface IUsuarioValidator : IPessoaValidator<Usuario>, IDisposable
    {
        Task<bool> ValidaGerenciadoresClientesUsuario(IEnumerable<Gerenciador> Gerenciadores, IEnumerable<Cliente> Clientes);
        Task<bool> ValidaExclusaoUsuario(Guid idUsuario);
    }
}

using HelpDesk.Business.Models;

namespace HelpDesk.Business.Interfaces.Validators
{
    public interface IUsuarioValidator : IPessoaValidator<Usuario>, IDisposable
    {
        Task<bool> ValidaGerenciadoresClientesUsuario(IEnumerable<Gerenciador> Gerenciadores, IEnumerable<Cliente> Clientes);
        Task<bool> ValidaExclusaoUsuario(Guid idUsuario);
    }
}

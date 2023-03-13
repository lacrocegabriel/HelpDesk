using HelpDesk.Business.Models;

namespace HelpDesk.Business.Interfaces.Validators
{
    public interface IUsuarioValidator : IPessoaValidator<Usuario>, IDisposable
    {
        Task<bool> ValidaExclusaoUsuario(Guid idUsuario);
    }
}

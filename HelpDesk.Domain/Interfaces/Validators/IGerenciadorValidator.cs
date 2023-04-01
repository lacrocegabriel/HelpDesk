using HelpDesk.Domain.Entities;

namespace HelpDesk.Domain.Interfaces.Validators
{
    public interface IGerenciadorValidator : IPessoaValidator<Gerenciador>, IDisposable
    {
        bool ValidaPermissaoVisualizacao(Gerenciador gerenciador, List<Guid> idGerenciadoresUsuario);
        Task<bool> ValidaExclusaoGerenciador(Guid idGerenciador);
    }
}

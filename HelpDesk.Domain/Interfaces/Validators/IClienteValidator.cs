using HelpDesk.Domain.Entities;

namespace HelpDesk.Domain.Interfaces.Validators
{
    public interface IClienteValidator : IPessoaValidator<Cliente>, IDisposable
    {
        Task<bool> ValidaExclusaoCliente(Guid idCliente);
        bool ValidaPermissaoVisualizacao(Cliente cliente, List<Guid> idGerenciadoresUsuario);
        bool ValidaPermissaoInsercaoEdicao(Cliente cliente, List<Guid> idGerenciadoresUsuario);
    }
}

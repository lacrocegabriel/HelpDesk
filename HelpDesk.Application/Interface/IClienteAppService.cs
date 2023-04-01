using HelpDesk.Domain.Entities;

namespace HelpDesk.Application.Interface
{
    public interface IClienteAppService : IAppServiceBase<Cliente>
    {
        Task Adicionar(Cliente cliente);
        Task Atualizar(Cliente cliente);
        Task Remover(Guid id);
        Task AtualizarEndereco(Endereco endereco);
        Task<IEnumerable<Cliente>> ObterTodos(int skip, int take);
        Task<Cliente?> ObterPorId(Guid id);
    }
}

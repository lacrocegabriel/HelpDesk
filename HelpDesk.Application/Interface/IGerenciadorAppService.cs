using HelpDesk.Domain.Entities;

namespace HelpDesk.Application.Interface
{
    public interface IGerenciadorAppService : IAppServiceBase<Gerenciador>
    {
        Task Adicionar(Gerenciador gerenciador);
        Task Atualizar(Gerenciador gerenciador);
        Task Remover(Guid id);
        Task AtualizarEndereco(Endereco endereco);
        Task<IEnumerable<Gerenciador>> ObterTodos(int skip, int take);
        Task<Gerenciador?> ObterPorId(Guid id);
    }
}

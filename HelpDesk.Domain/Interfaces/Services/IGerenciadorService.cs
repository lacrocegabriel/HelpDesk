using HelpDesk.Domain.Entities;

namespace HelpDesk.Domain.Interfaces.Services
{
    public interface IGerenciadorService : IDisposable
    {
        Task Adicionar(Gerenciador gerenciador);
        Task Atualizar(Gerenciador gerenciador);
        Task Remover(Guid id);
        Task AtualizarEndereco(Endereco endereco);
        Task<IEnumerable<Gerenciador>> ObterTodos(int skip, int take);
        Task<Gerenciador?> ObterPorId(Guid id);
    }
}

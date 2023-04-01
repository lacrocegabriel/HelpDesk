using HelpDesk.Domain.Entities;

namespace HelpDesk.Application.Interface
{
    public interface IUsuarioAppService : IAppServiceBase<Usuario>
    {
        Task Adicionar(Usuario usuario);
        Task Atualizar(Usuario usuario);
        Task Remover(Guid id);
        Task AtualizarEndereco(Endereco endereco);
        Task<Usuario?> ObterPorId(Guid id);
        Task<IEnumerable<Usuario>> ObterTodos(int skip, int take);
    }
}

using HelpDesk.Domain.Models;

namespace HelpDesk.Domain.Interfaces.Services
{
    public interface IUsuarioService : IDisposable
    {
        Task Adicionar(Usuario usuario);
        Task Atualizar(Usuario usuario);
        Task Remover(Guid id);
        Task AtualizarEndereco(Endereco endereco);
    }
}

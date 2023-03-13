using HelpDesk.Business.Models;

namespace HelpDesk.Business.Interfaces.Services
{
    public interface IUsuarioService : IDisposable
    {
        Task Adicionar(Usuario usuario, IEnumerable<Guid> idGerenciadores, IEnumerable<Guid> idClientes);
        Task Atualizar(Usuario usuario, IEnumerable<Guid> gerenciadores, IEnumerable<Guid> clientes);
        Task Remover(Guid id);
        Task AtualizarEndereco(Endereco endereco);
    }
}

using HelpDesk.Business.Models;

namespace HelpDesk.Business.Interfaces.Services
{
    public interface IClienteService : IDisposable
    {
        Task Adicionar(Cliente cliente);
        Task Atualizar(Cliente cliente);
        Task Remover(Guid id);

        Task AtualizarEndereco(Endereco endereco);
    }
}

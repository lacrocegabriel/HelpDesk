using HelpDesk.Business.Models;

namespace HelpDesk.Business.Interfaces.Services
{
    public interface IPessoaService : IDisposable
    {
        Task Adicionar(Pessoa pessoa);
        Task Atualizar(Pessoa pessoa);
        Task Remover(Guid id);

        Task AtualizarEndereco(Endereco endereco);
    }
}

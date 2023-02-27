using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Interfaces.Services;
using HelpDesk.Business.Models;

namespace HelpDesk.Business.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task Adicionar(Cliente cliente)
        {
            await _clienteRepository.Adicionar(cliente);
        }

        public async Task Atualizar(Cliente cliente)
        {
            await _clienteRepository.Atualizar(cliente);
        }

        public Task AtualizarEndereco(Endereco endereco)
        {
            throw new NotImplementedException();
        }

        public async Task Remover(Guid id)
        {
            await _clienteRepository.Remover(id);
        }

        public void Dispose()
        {
            _clienteRepository?.Dispose();
        }
    }
}

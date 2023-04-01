using HelpDesk.Application.Interface;
using HelpDesk.Domain.Entities;
using HelpDesk.Domain.Interfaces.Services;

namespace HelpDesk.Application.AppService
{
    public class ClienteAppService : AppServiceBase<Cliente>, IClienteAppService
    {
        private readonly IClienteService _clienteService;

        public ClienteAppService(IClienteService clienteService) : base(clienteService)
        {
            _clienteService = clienteService;
        }

        public async Task Adicionar(Cliente cliente)
        {
            await _clienteService.Adicionar(cliente);
        }

        public async Task Atualizar(Cliente cliente)
        {
            await _clienteService.Atualizar(cliente);
        }

        public async Task Remover(Guid id)
        {
            await _clienteService.Remover(id); 
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            await _clienteService.AtualizarEndereco(endereco);
        }

        public async Task<IEnumerable<Cliente>> ObterTodos(int skip, int take)
        {
            return await _clienteService.ObterTodos(skip, take);
        }

        public async Task<Cliente?> ObterPorId(Guid id)
        {
            return await _clienteService.ObterPorId(id);
        }
    }
}

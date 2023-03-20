using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Interfaces.Services;
using HelpDesk.Business.Interfaces.Validators;
using HelpDesk.Business.Models;
using HelpDesk.Business.Validator.Validators;

namespace HelpDesk.Business.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IClienteValidator _clienteValidator;
        private readonly IEnderecoRepository _enderecoRepository;

        public ClienteService(IClienteRepository clienteRepository,
                              IClienteValidator clienteValidator,
                              IEnderecoRepository enderecoRepository)
        {
            _clienteRepository = clienteRepository;
            _clienteValidator = clienteValidator;
            _enderecoRepository = enderecoRepository;
        }

        public async Task Adicionar(Cliente cliente)
        {
            if (await _clienteValidator.ValidaExistenciaPessoa(cliente.Id) 
                || !await _clienteValidator.ValidaPessoa(new ClienteValidation(), cliente)) return;

            await _clienteRepository.Adicionar(cliente);
        }

        public async Task Atualizar(Cliente cliente)
        {
            if (!await _clienteValidator.ValidaPessoa(new ClienteValidation(), cliente)) return;

            await _clienteRepository.Atualizar(cliente);
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            if (!await _clienteValidator.ValidaEnderecoPessoa(new EnderecoValidaton(), endereco)) return;

            await _enderecoRepository.Atualizar(endereco);
        }

        public async Task Remover(Guid idCliente)
        {
            if (!await _clienteValidator.ValidaExclusaoCliente(idCliente)) return;
            
            await _clienteRepository.Remover(idCliente);
        }

        public void Dispose()
        {
            _clienteRepository?.Dispose();
        }
    }
}

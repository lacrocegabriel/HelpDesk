using HelpDesk.Business.Interfaces.Others;
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
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUser _user;

        public ClienteService(IClienteRepository clienteRepository,
                              IClienteValidator clienteValidator,
                              IEnderecoRepository enderecoRepository,
                              IUsuarioRepository usuarioRepository,
                              IUser user)
        {
            _clienteRepository = clienteRepository;
            _clienteValidator = clienteValidator;
            _enderecoRepository = enderecoRepository;
            _usuarioRepository = usuarioRepository;
            _user = user;
        }
        public async Task<IEnumerable<Cliente>> ObterTodos(int skip, int take)
        {
            var usuario = await _usuarioRepository.ObterUsuarioPorAutenticacao(_user.GetUserId());

            var idGerenciadores = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(usuario.Id);

            return await _clienteRepository.ObterClientesPorPermissao(idGerenciadores.IdGerenciadores, skip, take);

        }

        public async Task<Cliente?> ObterPorId(Guid id)
        {
            var usuario = await _usuarioRepository.ObterUsuarioPorAutenticacao(_user.GetUserId());

            var idGerenciadoresUsuario = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(usuario.Id);

            var cliente = await _clienteRepository.ObterPorId(id);

            if (_clienteValidator.ValidaPermissaoVisualizacao(cliente, idGerenciadoresUsuario.IdGerenciadores))
            {
                return cliente;
            }

            return null;
        }
        public async Task Adicionar(Cliente cliente)
        {
            var usuario = await _usuarioRepository.ObterUsuarioPorAutenticacao(_user.GetUserId());

            var idGerenciadoresUsuario = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(usuario.Id);

            if (await _clienteValidator.ValidaExistenciaPessoa(cliente.Id) 
                || !await _clienteValidator.ValidaPessoa(new ClienteValidation(), cliente)
                || !_clienteValidator.ValidaPermissaoInsercaoEdicao(cliente,idGerenciadoresUsuario.IdGerenciadores)) return;

            await _clienteRepository.Adicionar(cliente);
        }

        public async Task Atualizar(Cliente cliente)
        {
            var usuario = await _usuarioRepository.ObterUsuarioPorAutenticacao(_user.GetUserId());

            var idGerenciadoresUsuario = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(usuario.Id);

            if (!await _clienteValidator.ValidaPessoa(new ClienteValidation(), cliente)
                || !_clienteValidator.ValidaPermissaoInsercaoEdicao(cliente, idGerenciadoresUsuario.IdGerenciadores)) return;

            await _clienteRepository.Atualizar(cliente);
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            if (!await _clienteValidator.ValidaEnderecoPessoa(new EnderecoValidaton(), endereco)) return;

            await _enderecoRepository.Atualizar(endereco);
        }

        public async Task Remover(Guid idCliente)
        {
            var usuario = await _usuarioRepository.ObterUsuarioPorAutenticacao(_user.GetUserId());

            var idGerenciadoresUsuario = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(usuario.Id);

            var cliente = await _clienteRepository.ObterPorId(idCliente);

            if (!await _clienteValidator.ValidaExclusaoCliente(idCliente)
                || !_clienteValidator.ValidaPermissaoInsercaoEdicao(cliente, idGerenciadoresUsuario.IdGerenciadores)) return;
            
            await _clienteRepository.Remover(idCliente);
        }

        public void Dispose()
        {
            _clienteRepository?.Dispose();
        }
    }
}

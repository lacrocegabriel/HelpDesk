using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Interfaces.Services;
using HelpDesk.Business.Interfaces.Validators;
using HelpDesk.Business.Models;
using HelpDesk.Business.Validator.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Business.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IGerenciadorRepository _gerenciadorRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IUsuarioValidator _usuarioValidator;
        

        public UsuarioService(IUsuarioRepository usuariorepository,
                              IUsuarioValidator usuarioValidator,
                              IEnderecoRepository enderecoRepository,
                              IGerenciadorRepository gerenciadorRepository,
                              IClienteRepository clienteRepository)
        {
            _usuarioRepository = usuariorepository;            
            _usuarioValidator = usuarioValidator;
            _enderecoRepository = enderecoRepository;
            _clienteRepository = clienteRepository;
            _gerenciadorRepository = gerenciadorRepository;
        }

        public async Task Adicionar(Usuario usuario, IEnumerable<Guid> idGerenciadores, IEnumerable<Guid> idClientes)
        {
            foreach (var g in idGerenciadores)
            {
                usuario.Gerenciadores = await _gerenciadorRepository.Buscar(c => c.Id == g);
            }

            foreach (var g in idClientes)
            {
                usuario.Clientes = await _clienteRepository.Buscar(c => c.Id == g);
            }

            if (!await _usuarioValidator.ValidaPessoa(new AdicionarUsuarioValidation(), usuario)) return;

            await _usuarioRepository.AdicionarUsuario(usuario);
        }

        public async Task Atualizar(Usuario usuario, IEnumerable<Guid> gerenciador, IEnumerable<Guid> cliente)
        {
            if (!await _usuarioValidator.ValidaPessoa(new AtualizarUsuarioValidation(), usuario)) return;

            await _usuarioRepository.AtualizarUsuario(usuario);
            
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            if (!await _usuarioValidator.ValidaEnderecoPessoa(new EnderecoValidaton(), endereco)) return;

            await _enderecoRepository.Atualizar(endereco);
        }

        public async Task Remover(Guid id)
        {
            if(!await _usuarioValidator.ValidaExclusaoUsuario(id)) return;

            await _usuarioRepository.Remover(id);
        }

        public void Dispose()
        {
            _usuarioRepository?.Dispose();
        }
    }
}

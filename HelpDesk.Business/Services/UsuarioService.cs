using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Interfaces.Services;
using HelpDesk.Business.Interfaces.Validators;
using HelpDesk.Business.Models;
using HelpDesk.Business.Validator.Validators;

namespace HelpDesk.Business.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IUsuarioValidator _usuarioValidator;
        

        public UsuarioService(IUsuarioRepository usuariorepository,
                              IUsuarioValidator usuarioValidator,
                              IEnderecoRepository enderecoRepository)
        {
            _usuarioRepository = usuariorepository;            
            _usuarioValidator = usuarioValidator;
            _enderecoRepository = enderecoRepository;
        }

        public async Task Adicionar(Usuario usuario)
        {
            if (!await _usuarioValidator.ValidaPessoa(new AdicionarUsuarioValidation(), usuario)
               || !await _usuarioValidator.ValidaGerenciadoresClientesUsuario(usuario.Gerenciadores, usuario.Clientes)) return;

            await _usuarioRepository.AdicionarUsuario(usuario);
        }

        public async Task Atualizar(Usuario usuario)
        {
            if (!await _usuarioValidator.ValidaPessoa(new AdicionarUsuarioValidation(), usuario)
               || !await _usuarioValidator.ValidaGerenciadoresClientesUsuario(usuario.Gerenciadores, usuario.Clientes)) return;

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

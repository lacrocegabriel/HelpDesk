using HelpDesk.Domain.Interfaces.Repositories;
using HelpDesk.Domain.Interfaces.Services;
using HelpDesk.Domain.Interfaces.Validators;
using HelpDesk.Domain.Models;
using HelpDesk.Domain.Validator.Validators;

namespace HelpDesk.Domain.Services
{
    public class UsuarioService : BaseValidator, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IUsuarioValidator _usuarioValidator;
        public UsuarioService(IUsuarioRepository usuariorepository,
                              IUsuarioValidator usuarioValidator,
                              IEnderecoRepository enderecoRepository,
                              INotificador notificador) : base(notificador) 
        {
            _usuarioRepository = usuariorepository;            
            _usuarioValidator = usuarioValidator;
            _enderecoRepository = enderecoRepository;
        }

        public async Task Adicionar(Usuario usuario)
        {
           if (await _usuarioValidator.ValidaExistenciaPessoa(usuario.Id) 
               || !await _usuarioValidator.ValidaPessoa(new UsuarioValidation(), usuario)
               || !await _usuarioValidator.ValidaGerenciadoresClientesUsuario(usuario.Gerenciadores, usuario.Clientes)) return;

           await _usuarioRepository.AdicionarUsuario(usuario);
        }

        public async Task Atualizar(Usuario usuario)
        {
            if (!await _usuarioValidator.ValidaPessoa(new UsuarioValidation(), usuario)
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

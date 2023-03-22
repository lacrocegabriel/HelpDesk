using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Interfaces.Services;
using HelpDesk.Business.Interfaces.Validators;
using HelpDesk.Business.Models;
using HelpDesk.Business.Validator.Validators;

namespace HelpDesk.Business.Services
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

           var result = await _usuarioRepository.AdicionarUsuario(usuario);

            if (!result.Adicionado)
            {
                foreach(var erro in result.Erros)
                {
                    Notificar(erro);
                }
            }
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

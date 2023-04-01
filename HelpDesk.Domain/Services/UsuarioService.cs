using HelpDesk.Domain.Entities;
using HelpDesk.Domain.Interfaces.Others;
using HelpDesk.Domain.Interfaces.Repositories;
using HelpDesk.Domain.Interfaces.Services;
using HelpDesk.Domain.Interfaces.Validators;
using HelpDesk.Domain.Validator.Validators;

namespace HelpDesk.Domain.Services
{
    public class UsuarioService : ServiceBase<Usuario>, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IUsuarioValidator _usuarioValidator;
        private readonly IUser _user;
        public UsuarioService(IUsuarioRepository usuariorepository,
                              IUsuarioValidator usuarioValidator,
                              IUser user,
                              IEnderecoRepository enderecoRepository) : base(usuariorepository)
        {
            _usuarioRepository = usuariorepository;            
            _usuarioValidator = usuarioValidator;
            _enderecoRepository = enderecoRepository;
            _user = user;
        }
        public async Task<IEnumerable<Usuario>> ObterTodos(int skip, int take)
        {
            var usuario = await _usuarioRepository.ObterUsuarioGerenciadoresClientes(_user.GetUserId());

            return await _usuarioRepository.ObterUsuariosPorPermissao(usuario, skip, take);

        }

        public async Task<Usuario?> ObterPorId(Guid id)
        {
            var usuario = await _usuarioRepository.ObterUsuarioGerenciadoresClientes(_user.GetUserId());

            var (idGerenciadores, idClientes) = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(usuario.Id);

            var usuarioBuscado = await _usuarioRepository.ObterUsuarioGerenciadoresClientes(id);

            return _usuarioValidator.ValidaPermissaoVisualizacao(usuarioBuscado, idGerenciadores, idClientes) ? usuarioBuscado : null;
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
    }
}

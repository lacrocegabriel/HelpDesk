using HelpDesk.Application.Interface;
using HelpDesk.Domain.Entities;
using HelpDesk.Domain.Interfaces.Services;

namespace HelpDesk.Application.AppService
{
    public class UsuarioAppService : AppServiceBase<Usuario>, IUsuarioAppService
    {
        private readonly IUsuarioService _usuarioService;
        
        public UsuarioAppService(IUsuarioService usuarioService) : base(usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task Adicionar(Usuario usuario)
        {
            await _usuarioService.Adicionar(usuario);
        }

        public async Task Atualizar(Usuario usuario)
        {
            await _usuarioService.Atualizar(usuario);
        }

        public async Task Remover(Guid id)
        {
            await _usuarioService.Remover(id);
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            await _usuarioService.AtualizarEndereco(endereco);
        }

        public async Task<IEnumerable<Usuario>> ObterTodos(int skip, int take)
        {
            return await _usuarioService.ObterTodos(skip, take);
        }

        public async Task<Usuario?> ObterPorId(Guid id)
        {
            return await _usuarioService.ObterPorId(id);
        }
    }
}

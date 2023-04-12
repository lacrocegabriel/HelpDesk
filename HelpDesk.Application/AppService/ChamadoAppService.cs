using HelpDesk.Application.Interface;
using HelpDesk.Domain.Entities;
using HelpDesk.Domain.Interfaces.Services;

namespace HelpDesk.Application.AppService
{
    public class ChamadoAppService : AppServiceBase<Chamado>, IChamadoAppService
    {
        private readonly IChamadoService _chamadoService;
        private readonly IGerenciadorService _gerenciadorService;
        private readonly IClienteService _clienteService;
        private readonly IUsuarioService _usuarioService;
        public ChamadoAppService(IChamadoService chamadoService, 
                                 IGerenciadorService gerenciadorService, 
                                 IClienteService clienteService, 
                                 IUsuarioService usuarioService) : base(chamadoService)
        {
            _chamadoService = chamadoService;
            _gerenciadorService = gerenciadorService;
            _clienteService = clienteService;
            _usuarioService = usuarioService;
        }
        public async Task Adicionar(Chamado chamado)
        {
            await _chamadoService.Adicionar(chamado);
        }

        public async Task Atualizar(Chamado chamado)
        {
            await _chamadoService.Atualizar(chamado);
        }

        public async Task<IEnumerable<Chamado>> ObterTodos(int skip, int take)
        {
            return await _chamadoService.ObterTodos(skip, take);
        }

        public async Task<Chamado?> ObterPorId(Guid id)
        {
            return await _chamadoService.ObterPorId(id);
        }
    }
}

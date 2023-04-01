using HelpDesk.Application.Interface;
using HelpDesk.Domain.Entities;
using HelpDesk.Domain.Interfaces.Services;

namespace HelpDesk.Application.AppService
{
    public class ChamadoAppService : AppServiceBase<Chamado>, IChamadoAppService
    {
        private readonly IChamadoService _chamadoService;
        public ChamadoAppService(IChamadoService chamadoService) : base(chamadoService)
        {
            _chamadoService = chamadoService;
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

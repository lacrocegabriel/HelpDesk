using HelpDesk.Application.Interface;
using HelpDesk.Domain.Entities;
using HelpDesk.Domain.Interfaces.Services;

namespace HelpDesk.Application.AppService
{
    public class TramiteAppService : AppServiceBase<Tramite>, ITramiteAppService
    {
        private readonly ITramiteService _tramiteService;

        public TramiteAppService(ITramiteService tramiteService) : base(tramiteService)
        {
            _tramiteService = tramiteService;
        }

        public async Task Adicionar(Tramite tramite)
        {
            await _tramiteService.Adicionar(tramite);
        }

        public async Task Atualizar(Tramite tramite)
        {
            await _tramiteService.Atualizar(tramite);
        }

        public async Task<IEnumerable<Tramite>> ObterTodos(int skip, int take)
        {
            return await _tramiteService.ObterTodos(skip, take);
        }

        public async Task<Tramite?> ObterPorId(Guid id)
        {
            return await _tramiteService.ObterPorId(id);
        }
    }
}

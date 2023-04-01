using HelpDesk.Application.Interface;
using HelpDesk.Domain.Entities;
using HelpDesk.Domain.Interfaces.Services;

namespace HelpDesk.Application.AppService
{
    public class SetorAppService : AppServiceBase<Setor>, ISetorAppService
    {
        private readonly ISetorService _setorService;
        public SetorAppService(ISetorService setorService) : base(setorService)
        {
            _setorService = setorService;
        }
        public async Task Adicionar(Setor setor)
        {
            await _setorService.Adicionar(setor);
        }

        public async Task Atualizar(Setor setor)
        {
            await _setorService.Atualizar(setor);
        }

        public async Task Remover(Guid id)
        {
            await _setorService.Remover(id);
        }

        public async Task<IEnumerable<Setor>> ObterTodos(int skip, int take)
        {
            return await _setorService.ObterTodos(skip, take);
        }

        public async Task<Setor?> ObterPorId(Guid id)
        {
            return await _setorService.ObterPorId(id);
        }
    }
}

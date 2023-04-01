using HelpDesk.Application.Interface;
using HelpDesk.Domain.Entities;
using HelpDesk.Domain.Interfaces.Services;

namespace HelpDesk.Application.AppService
{
    public class GerenciadorAppService : AppServiceBase<Gerenciador>, IGerenciadorAppService
    {
        private readonly IGerenciadorService _gerenciadorService;

        public GerenciadorAppService(IGerenciadorService gerenciadorService) : base(gerenciadorService)
        {
            _gerenciadorService = gerenciadorService;
        }

        public async Task Adicionar(Gerenciador gerenciador)
        {
            await _gerenciadorService.Adicionar(gerenciador);
        }

        public async Task Atualizar(Gerenciador gerenciador)
        {
            await _gerenciadorService.Atualizar(gerenciador);
        }

        public async Task Remover(Guid id)
        {
            await _gerenciadorService.Remover(id);
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            await _gerenciadorService.AtualizarEndereco(endereco);
        }

        public async Task<IEnumerable<Gerenciador>> ObterTodos(int skip, int take)
        {
            return await _gerenciadorService.ObterTodos(skip, take);
        }

        public async Task<Gerenciador?> ObterPorId(Guid id)
        {
            return await _gerenciadorService.ObterPorId(id);
        }
    }
}

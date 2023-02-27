using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Interfaces.Services;
using HelpDesk.Business.Models;

namespace HelpDesk.Business.Services
{
    public class GerenciadorService : IGerenciadorService
    {
        private readonly IGerenciadorRepository _gerenciadorRepository;

        public GerenciadorService(IGerenciadorRepository gerenciadorRepository)
        {
            _gerenciadorRepository = gerenciadorRepository;
        }

        public async Task Adicionar(Gerenciador gerenciador)
        {
            await _gerenciadorRepository.Adicionar(gerenciador);
        }

        public async Task Atualizar(Gerenciador gerenciador)
        {
            await _gerenciadorRepository.Atualizar(gerenciador);
        }

        public Task AtualizarEndereco(Endereco endereco)
        {
            throw new NotImplementedException();
        }

        public async Task Remover(Guid id)
        {
            await _gerenciadorRepository.Remover(id);   
        }

        public void Dispose()
        {
            _gerenciadorRepository?.Dispose();
        }
    }
}

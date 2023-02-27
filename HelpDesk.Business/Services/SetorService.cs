using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Interfaces.Services;
using HelpDesk.Business.Models;

namespace HelpDesk.Business.Services
{
    public class SetorService : ISetorService
    {
        private readonly ISetorRepository _setorRepository;

        public SetorService(ISetorRepository setorRepository)
        {
            _setorRepository = setorRepository;
        }

        public async Task Adicionar(Setor setor)
        {
            await _setorRepository.Adicionar(setor);
        }

        public async Task Atualizar(Setor setor)
        {
            await _setorRepository.Atualizar(setor);
        }

        public async Task Remover(Guid id)
        {
            await _setorRepository.Remover(id);
        }

        public void Dispose()
        {
            _setorRepository?.Dispose();
        }
    }
}

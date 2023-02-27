using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Interfaces.Services;
using HelpDesk.Business.Models;

namespace HelpDesk.Business.Services
{
    public class ChamadoService : IChamadoService
    {
        private readonly IChamadoRepository _chamadoRepository;

        public ChamadoService(IChamadoRepository chamadoRepository)
        {
            _chamadoRepository = chamadoRepository;
        }

        public async Task Adicionar(Chamado chamado)
        {
            await _chamadoRepository.Adicionar(chamado);
        }

        public async Task Atualizar(Chamado chamado)
        {
            await _chamadoRepository.Atualizar(chamado);
        }

        public async Task Remover(Guid id)
        {
            await _chamadoRepository.Remover(id);
        }
        public void Dispose()
        {
            _chamadoRepository?.Dispose();  
        }
    }
}

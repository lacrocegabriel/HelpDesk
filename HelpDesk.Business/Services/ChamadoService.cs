using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Interfaces.Services;
using HelpDesk.Business.Interfaces.Validators;
using HelpDesk.Business.Models;
using HelpDesk.Business.Validator.Validators;

namespace HelpDesk.Business.Services
{
    public class ChamadoService : IChamadoService
    {
        private readonly IChamadoRepository _chamadoRepository;
        private readonly IChamadoValidator _chamadoValidator;

        public ChamadoService(IChamadoRepository chamadoRepository,
                              IChamadoValidator chamadoValidator)
        {
            _chamadoRepository = chamadoRepository;
            _chamadoValidator = chamadoValidator;
        }

        public async Task Adicionar(Chamado chamado)
        {
            if (await _chamadoValidator.ValidaExistenciaChamado(chamado.Id) 
                || !await _chamadoValidator.ValidaChamado(new ChamadoValidation(), chamado)) return;

            await _chamadoRepository.Adicionar(chamado);
        }

        public async Task Atualizar(Chamado chamado)
        {
            if (!await _chamadoValidator.ValidaChamado(new ChamadoValidation(), chamado)) return;

            await _chamadoRepository.Atualizar(chamado);
        }
        public void Dispose()
        {
            _chamadoRepository?.Dispose();  
        }
    }
}

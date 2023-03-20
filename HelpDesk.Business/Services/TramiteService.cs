using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Interfaces.Services;
using HelpDesk.Business.Interfaces.Validators;
using HelpDesk.Business.Models;
using HelpDesk.Business.Validator.Validators;

namespace HelpDesk.Business.Services
{
    public class TramiteService : ITramiteService
    {
        private readonly ITramiteRepository _tramiteRepository;
        private readonly ITramiteValidator  _tramiteValidator;
        private readonly IChamadoService _chamadoService;

        public TramiteService(ITramiteRepository tramiteRepository,
                              ITramiteValidator  tramiteValidator,
                              IChamadoService chamadoService)
        {
            _tramiteRepository = tramiteRepository;
            _tramiteValidator = tramiteValidator;
            _chamadoService = chamadoService;
        }

        public async Task Adicionar(Tramite tramite)
        {
           
            if (await _tramiteValidator.ValidaExistenciaTramite(tramite.Id)
                || !await _tramiteValidator.ValidaTramite(new TramiteValidation(), tramite)) return;

            await _tramiteRepository.AdicionarTramite(tramite);
        }

        public async Task Atualizar(Tramite tramite)
        {
            if (!await _tramiteValidator.ValidaTramite(new TramiteValidation(), tramite)) return;

            await _tramiteRepository.AtualizarTramite(tramite);
        }
        public void Dispose()
        {
            _tramiteRepository?.Dispose();
        }
    }
}

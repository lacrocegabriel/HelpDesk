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

        public TramiteService(ITramiteRepository tramiteRepository,
                              ITramiteValidator  tramiteValidator)
        {
            _tramiteRepository = tramiteRepository;
            _tramiteValidator = tramiteValidator;
        }

        public async Task Adicionar(Tramite tramite)
        {
            if (await _tramiteValidator.ValidaExistenciaTramite(tramite.Id)
                || !await _tramiteValidator.ValidaTramite(new TramiteValidation(), tramite)) return;

            await _tramiteRepository.Adicionar(tramite);
        }

        public async Task Atualizar(Tramite tramite)
        {
            if (!await _tramiteValidator.ValidaTramite(new TramiteValidation(), tramite)) return;

            await _tramiteRepository.Atualizar(tramite);
        }
        public void Dispose()
        {
            _tramiteRepository?.Dispose();
        }
    }
}

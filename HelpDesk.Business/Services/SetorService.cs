using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Interfaces.Services;
using HelpDesk.Business.Interfaces.Validators;
using HelpDesk.Business.Models;
using HelpDesk.Business.Validator.Validators;

namespace HelpDesk.Business.Services
{
    public class SetorService : ISetorService
    {
        private readonly ISetorRepository _setorRepository;
        private readonly ISetorValidator _setorValidator;

        public SetorService(ISetorRepository setorRepository,
                            ISetorValidator setorValidator)
        {
            _setorRepository = setorRepository;
            _setorValidator = setorValidator;
        }

        public async Task Adicionar(Setor setor)
        {
            if (await _setorValidator.ValidaExistenciaSetor(setor.Id)
                ||!await _setorValidator.ValidaSetor(new SetorValidation(), setor)) return;

            await _setorRepository.Adicionar(setor);
        }

        public async Task Atualizar(Setor setor)
        {
            if (!await _setorValidator.ValidaSetor(new SetorValidation(), setor)) return;

            await _setorRepository.Atualizar(setor);
        }

        public async Task Remover(Guid id)
        {
            if(!await _setorValidator.ValidaExclusaoSetor(id)) return;

            await _setorRepository.Remover(id);
        }

        public void Dispose()
        {
            _setorRepository?.Dispose();
        }
    }
}

using FluentValidation;
using HelpDesk.Business.Models;

namespace HelpDesk.Business.Interfaces.Validators
{
    public interface ITramiteValidator
    {
        Task<bool> ValidaTramite(AbstractValidator<Tramite> validator, Tramite tramite);
        Task<bool> ValidaExistenciaTramite(Guid id);
    }
}

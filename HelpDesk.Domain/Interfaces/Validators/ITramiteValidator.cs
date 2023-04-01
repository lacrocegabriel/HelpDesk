using FluentValidation;
using HelpDesk.Domain.Models;

namespace HelpDesk.Domain.Interfaces.Validators
{
    public interface ITramiteValidator
    {
        Task<bool> ValidaTramite(AbstractValidator<Tramite> validator, Tramite tramite);
        Task<bool> ValidaExistenciaTramite(Guid id);
    }
}

using FluentValidation;
using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Interfaces.Validators;
using HelpDesk.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Business.Validator.Validators
{
    public class TramiteValidator : BaseValidator, ITramiteValidator
    {
        private readonly ITramiteRepository _tramiteRepository;

        public TramiteValidator(ITramiteRepository tramiteRepository,
                                INotificador notificador) : base(notificador) 
        {
            _tramiteRepository = tramiteRepository;
        }
        public async Task<bool> ValidaExistenciaTramite(Guid id)
        {
            var tramiteExistente = await _tramiteRepository.ObterPorId(id);

            if (tramiteExistente == null)
            {
                return false;
            }
            Notificar("O Id informado se encontra em uso pele trâmite  " + "Id: " + tramiteExistente.Id + " Descrição: " + tramiteExistente.Descricao.Substring(1,20));

            return true;
        }

        public async Task<bool> ValidaTramite(AbstractValidator<Tramite> validator, Tramite tramite)
        {
            if (!ExecutarValidacao(validator, tramite)) return false;

            return true;
        }
    }

    public class TramiteValidation : AbstractValidator<Tramite>
    {
        public TramiteValidation()
        {
            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("A descrição precisa ser fornecida")
                .MinimumLength(30).WithMessage("A descrição precisa ter no mínimo 30 caracteres");

            RuleFor(c => c.IdUsuarioGerador)
                .NotEmpty().WithMessage("O usuário precisa ser fornecido");

            RuleFor(c => c.IdChamado)
               .NotEmpty().WithMessage("O chamado precisa ser fornecido");
        }
    }
}

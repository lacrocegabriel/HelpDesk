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
    public class ChamadoValidator : BaseValidator, IChamadoValidator 
    {
        private readonly IChamadoRepository _chamadoRepository;

        public ChamadoValidator(INotificador notificador, IChamadoRepository chamadoRepository) : base(notificador)
        {
            _chamadoRepository = chamadoRepository;
        }

        public async Task<bool> ValidaChamado(AbstractValidator<Chamado> validator, Chamado chamado) 
        {
            if(!ExecutarValidacao(validator, chamado)) return false;

            return true;
        }

        public async Task<bool> ValidaExistenciaChamado(Guid id)
        {
            var chamadoExistente = await _chamadoRepository.ObterPorId(id);

            if (chamadoExistente == null)
            {
                return false;
            }
            Notificar("O Id informado se encontra em uso pela chamdo  " + "Id: " + chamadoExistente.Id + " Título: " + chamadoExistente.Titulo); ;

            return true;

        }

    }

    public class ChamadoValidation : AbstractValidator<Chamado>
    {
        public ChamadoValidation()
        {
            RuleFor(c => c.Titulo)
                .NotEmpty().WithMessage("O título precisa ser fornecido")
                .Length(10, 100).WithMessage("O título precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("A descrição precisa ser fornecida")
                .MinimumLength(30).WithMessage("A descrição precisa ter no mínimo 30 caracteres");

            RuleFor(c => c.IdSituacaoChamado)
                .NotEmpty().WithMessage("Deve ser informado a situação do chamado");

            RuleFor(c => c.IdCliente)
                .NotEmpty().WithMessage("O cliente precisa ser fornecido");

            RuleFor(c => c.IdGerenciador)
                .NotEmpty().WithMessage("O gerenciador precisa ser fornecido");

            RuleFor(c => c.IdUsuarioGerador)
                .NotEmpty().WithMessage("O usuário gerador precisa ser fornecido");

            RuleFor(c => c.IdUsuarioResponsavel)
                .NotEmpty().WithMessage("O usuário responsável precisa ser fornecido");

        }
    }
}

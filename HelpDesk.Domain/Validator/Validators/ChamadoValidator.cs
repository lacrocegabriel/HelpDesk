using FluentValidation;
using HelpDesk.Domain.Interfaces.Repositories;
using HelpDesk.Domain.Interfaces.Validators;
using HelpDesk.Domain.Models;

namespace HelpDesk.Domain.Validator.Validators
{
    public class ChamadoValidator : BaseValidator, IChamadoValidator 
    {
        private readonly IChamadoRepository _chamadoRepository;

        public ChamadoValidator(INotificador notificador, 
                                IChamadoRepository chamadoRepository) : base(notificador)
        {
            _chamadoRepository = chamadoRepository;
        }

        public bool ValidaPermissaoInsercaoEdicao(Chamado chamado, List<Guid> idGerenciadoresUsuario, List<Guid> idClientesUsuario, List<Guid> idGerenciadoresUsuarioResponsavel, List<Guid> idClientesUsuarioResponsavel)
        {
            if(!idGerenciadoresUsuario.Contains(chamado.IdGerenciador))
            {
                Notificar("O usuário não possui permissão para gerenciar um chamado para o gerenciador informado");
                return false;
            }

            if (!idClientesUsuario.Contains(chamado.IdCliente))
            {
                Notificar("O usuário não possui permissão para gerenciar um chamado para o cliente informado");
                return false;
            }

            if (!idGerenciadoresUsuarioResponsavel.Contains(chamado.IdGerenciador))
            {
                Notificar("O usuário responsável selecionado não possui permissão para gerenciar um chamado para o gerenciador informado");
                return false;
            }

            if (!idClientesUsuarioResponsavel.Contains(chamado.IdCliente))
            {
                Notificar("O usuário responsável selecionado não possui permissão para gerenciar um chamado para o cliente informado");
                return false;
            }

            return true;
        }

        public bool ValidaPermissaoVisualizacao(Chamado chamado, List<Guid> idGerenciadoresUsuario, List<Guid> idClientesUsuario)
        {
            if (!idGerenciadoresUsuario.Contains(chamado.IdGerenciador)
               || !idClientesUsuario.Contains(chamado.IdCliente))
            {
                Notificar("O usuário não possui permissão para visualizar o chamado selecionado! Verifique as informações e tente novamente");
                return false;
            }

            return true;
        }

        public bool ValidaChamado(AbstractValidator<Chamado> validator, Chamado chamado) 
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

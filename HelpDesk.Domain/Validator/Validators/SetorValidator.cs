using FluentValidation;
using HelpDesk.Domain.Interfaces.Repositories;
using HelpDesk.Domain.Interfaces.Validators;
using HelpDesk.Domain.Models;

namespace HelpDesk.Domain.Validator.Validators
{
    public class SetorValidator : BaseValidator, ISetorValidator
    {
        private readonly ISetorRepository _setorRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public SetorValidator(ISetorRepository setorRepository,
                              INotificador notificador,
                              IUsuarioRepository usuarioRepository) : base(notificador)
        {
            _setorRepository = setorRepository;
            _usuarioRepository = usuarioRepository;

        }

        public bool ValidaPermissaoVisualizacao(Setor setor, List<Guid> idGerenciadoresUsuario)
        {
            if (!idGerenciadoresUsuario.Contains(setor.IdGerenciador))
            {
                Notificar("O usuário não possui permissão para visualizar o setor selecionado");
                return false;
            }

            return true;
        }
        public bool ValidaPermissaoInsercaoEdicao(Setor setor, List<Guid> idGerenciadoresUsuario)
        {
            if (!idGerenciadoresUsuario.Contains(setor.IdGerenciador))
            {
                Notificar("O usuário não possui permissão para gerenciar um setor para o gerenciador informado");
                return false;
            }

            return true;
        }

        public async Task<bool> ValidaSetor(AbstractValidator<Setor> validator, Setor setor)
        {
            if (!ExecutarValidacao(validator, setor)) return false;

            var setorExistente = await _setorRepository.BuscarUnico(s => s.Descricao == setor.Descricao && s.Id != setor.Id);

            if(setorExistente != null) 
            {
                Notificar("A descrição informada já está em uso pelo setor: " + "Id: " + setorExistente.Id + " Descrição: " + setorExistente.Descricao);

                return false;
            }

            return true;
        }

        public async Task<bool> ValidaExistenciaSetor(Guid id)
        {
            var setorExistente = await _setorRepository.ObterPorId(id);

            if (setorExistente == null)
            {
                return false;
            }
            Notificar("O Id informado já se encontra em uso pela setor  " + "Id: " + setorExistente.Id + " Nome: " + setorExistente.Descricao); ;

            return true;

        }
        public async Task<bool> ValidaExclusaoSetor(Guid id)
        {
            var usuario = await _usuarioRepository.ObterUsuariosPorSetor(id);

            if (usuario.Any())
            {
                string mensagem = "Não é possível excluir o setor, pois esta vinculado nos seguintes usuários: ";

                foreach (var u in usuario)
                {
                    mensagem = mensagem + "Id: " + u.Id + " Nome: " + u.Nome;
                }

                Notificar(mensagem);
                return false;
            }

            return true;
        }

        public void Dispose()
        {
            _setorRepository?.Dispose();
        }
    }

    public class SetorValidation : AbstractValidator<Setor>
    {
        public SetorValidation() 
        {
            RuleFor(s => s.Descricao)
                .NotEmpty().WithMessage("O campo Descrição deve ser fornecido")
                .Length(10,300).WithMessage("O campo Descrição precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(s => s.IdGerenciador)
                .NotEmpty().WithMessage("Deve ser informado o gerenciador do setor");
        }
    }
}

using FluentValidation;
using HelpDesk.Domain.Entities;

namespace HelpDesk.Domain.Interfaces.Validators
{
    public interface IChamadoValidator
    {
        Task<bool> ValidaExistenciaChamado(Guid id);
        bool ValidaChamado(AbstractValidator<Chamado> validator, Chamado chamado);
        bool ValidaPermissaoInsercaoEdicao(Chamado chamado, List<Guid> idGerenciadoresUsuario, List<Guid> idClientesUsuario, List<Guid> idGerenciadoresUsuarioResponsavel, List<Guid> idClientesUsuarioResponsavel);
        bool ValidaPermissaoVisualizacao(Chamado chamado, List<Guid> idGerenciadoresUsuario, List<Guid> idClientesUsuario);
    }
}

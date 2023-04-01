using FluentValidation;
using HelpDesk.Domain.Entities;

namespace HelpDesk.Domain.Interfaces.Validators
{
    public interface ISetorValidator : IDisposable
    {
        Task<bool> ValidaSetor(AbstractValidator<Setor> validator, Setor setor);
        Task<bool> ValidaExclusaoSetor(Guid id);
        Task<bool> ValidaExistenciaSetor(Guid id);
        bool ValidaPermissaoVisualizacao(Setor setor, List<Guid> idGerenciadoresUsuario);
        bool ValidaPermissaoInsercaoEdicao(Setor setor, List<Guid> idGerenciadoresUsuario);
    }
}

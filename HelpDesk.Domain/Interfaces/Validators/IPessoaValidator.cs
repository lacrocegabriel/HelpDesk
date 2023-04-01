using FluentValidation;
using HelpDesk.Domain.Entities;

namespace HelpDesk.Domain.Interfaces.Validators
{
    public interface IPessoaValidator<TEntity> : IDisposable where TEntity : Pessoa 
    {
        Task<bool> ValidaPessoa(AbstractValidator<TEntity> validator, TEntity entity);
        Task<bool> ValidaInsercaoEdicaoPessoa(TEntity entity);
        Task<bool> ValidaExistenciaPessoa(Guid id);
        Task<bool> ValidaEnderecoPessoa(AbstractValidator<Endereco> validator, Endereco endereco);
    }
}

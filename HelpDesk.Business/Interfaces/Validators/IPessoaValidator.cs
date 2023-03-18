using FluentValidation;
using HelpDesk.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Business.Interfaces.Validators
{
    public interface IPessoaValidator<TEntity> : IDisposable where TEntity : Pessoa 
    {
        Task<bool> ValidaPessoa(AbstractValidator<TEntity> validator, TEntity entity);
        Task<bool> ValidaInsercaoEdicaoPessoa(TEntity entity);
        Task<bool> ValidaExistenciaPessoa(Guid id);
        Task<bool> ValidaEnderecoPessoa(AbstractValidator<Endereco> validator, Endereco endereco);
    }
}

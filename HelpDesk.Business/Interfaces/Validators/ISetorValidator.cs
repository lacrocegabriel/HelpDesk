using FluentValidation;
using HelpDesk.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Business.Interfaces.Validators
{
    public interface ISetorValidator : IDisposable
    {
        Task<bool> ValidaSetor(AbstractValidator<Setor> validator, Setor setor);
        Task<bool> ValidaExclusaoSetor(Guid id);
    }
}

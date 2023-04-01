using FluentValidation;
using HelpDesk.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Business.Interfaces.Validators
{
    public interface IGerenciadorValidator : IPessoaValidator<Gerenciador>, IDisposable
    {
        bool ValidaPermissaoVisualizacao(Gerenciador gerenciador, List<Guid> idGerenciadoresUsuario);
        Task<bool> ValidaExclusaoGerenciador(Guid idGerenciador);
    }
}

using FluentValidation;
using HelpDesk.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Business.Interfaces.Validators
{
    public interface IChamadoValidator
    {
        Task<bool> ValidaExistenciaChamado(Guid id);
        bool ValidaChamado(AbstractValidator<Chamado> validator, Chamado chamado);
        bool ValidaPermissaoInsercaoEdicao(Chamado chamado, List<Guid> idGerenciadoresUsuario, List<Guid> idClientesUsuario, List<Guid> idGerenciadoresUsuarioResponsavel, List<Guid> idClientesUsuarioResponsavel);
        bool ValidaPermissaoVisualizacao(Chamado chamado, List<Guid> idGerenciadoresUsuario, List<Guid> idClientesUsuario);
    }
}

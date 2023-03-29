using HelpDesk.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Business.Interfaces.Validators
{
    public interface IClienteValidator : IPessoaValidator<Cliente>, IDisposable
    {
        Task<bool> ValidaExclusaoCliente(Guid idCliente);
        bool ValidaPermissaoVisualizacao(Cliente cliente, List<Guid> idGerenciadoresUsuario);
        bool ValidaPermissaoInsercaoEdicao(Cliente cliente, List<Guid> idGerenciadoresUsuario);
    }
}

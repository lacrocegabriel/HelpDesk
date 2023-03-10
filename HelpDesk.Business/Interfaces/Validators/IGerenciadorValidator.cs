using FluentValidation;
using HelpDesk.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Business.Interfaces.Validators
{
    public interface IGerenciadorValidator
    {
        bool ValidaGerenciador(AbstractValidator<Gerenciador> validator, Gerenciador gerenciador);
        bool ValidaEnderecoGerenciador(AbstractValidator<Endereco> validator, Endereco endereco);
        bool ValidaExclusaoGerenciador(Guid idGerenciador);
    }
}

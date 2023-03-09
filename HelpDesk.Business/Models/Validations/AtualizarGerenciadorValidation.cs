using FluentValidation;
using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Models.Validations.DocumentoValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Business.Models.Validations
{
    public class AtualizarGerenciadorValidation : AbstractValidator<Gerenciador>
    {
        public AtualizarGerenciadorValidation() 
        {
            RuleFor(g => g.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(g => g.Email)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(10, 50)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(g => g.DataNascimentoConstituicao)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(g => g.IdTipoPessoa)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            When(g => g.IdTipoPessoa == 2, () =>
            {
                RuleFor(g => g.Documento.Length).Equal(CpfValidacao.TamanhoCpf)
                    .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
                RuleFor(g => CpfValidacao.Validar(g.Documento)).Equal(true)
                    .WithMessage("O documento fornecido é inválido.");
            });

            When(g => g.IdTipoPessoa == 1, () =>
            {
                RuleFor(g => g.Documento.Length).Equal(CnpjValidacao.TamanhoCnpj)
                    .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
                RuleFor(g => CnpjValidacao.Validar(g.Documento)).Equal(true)
                    .WithMessage("O documento fornecido é inválido.");
            });

        }

                
    }
}

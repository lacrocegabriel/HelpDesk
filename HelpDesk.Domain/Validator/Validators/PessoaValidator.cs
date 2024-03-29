﻿using FluentValidation;
using HelpDesk.Domain.Entities;
using HelpDesk.Domain.Interfaces.Repositories;
using HelpDesk.Domain.Interfaces.Validators;

namespace HelpDesk.Domain.Validator.Validators
{
    public class PessoaValidator<TEntity> : BaseValidator, IPessoaValidator<TEntity> where TEntity : Pessoa, new()
    {
        private readonly IPessoaRepository _pessoaRepository;

        public PessoaValidator(IPessoaRepository pessoaRepository, 
                               INotificador notificador) : base(notificador)
        {
            _pessoaRepository = pessoaRepository;
        }



        public async Task<bool> ValidaPessoa(AbstractValidator<TEntity> validator, TEntity entity)
        {
            if (!ExecutarValidacao(validator, entity)
                || !await ValidaInsercaoEdicaoPessoa(entity)) return false;

            if (entity.Endereco != null)
            {
                return await ValidaEnderecoPessoa(new EnderecoValidaton(), entity.Endereco);
            }

            return true;
        }

        public async Task<bool> ValidaExistenciaPessoa(Guid id)
        {
            var pessoa = await _pessoaRepository.ObterPorId(id);

            if (pessoa == null)
            {
                return false;
            }
            Notificar("O Id informado já se encontra em uso pela pessoa  " + "Id: " + pessoa.Id + " Nome: " + pessoa.Nome); ;

            return true;
            
        }

        public async Task<bool> ValidaEnderecoPessoa(AbstractValidator<Endereco> validator, Endereco endereco)
        {
            if (!ExecutarValidacao(validator, endereco)) return false;

            return true;
        }

        public async Task<bool> ValidaInsercaoEdicaoPessoa(TEntity entity)
        {
            var pessoaMesmoDocumento = await _pessoaRepository.BuscarUnico(g => g.Documento == entity.Documento && g.Id != entity.Id);

            if (pessoaMesmoDocumento != null)
            {
                Notificar("O documento informado para a pessoa já se encontra em uso pela pessoa  " + "Id: " + pessoaMesmoDocumento.Id + " Nome: " + pessoaMesmoDocumento.Nome); ;
                return false;
            }

            var pessoamesmoemail = await _pessoaRepository.BuscarUnico(g => g.Email == entity.Email && g.Id != entity.Id);

            if (pessoamesmoemail != null)
            {
                Notificar("O e-mail informado para a pessoa já se encontra em uso pela pessoa: " + "id: " + pessoamesmoemail.Id + " nome: " + pessoamesmoemail.Nome);

                return false;
            }

            try
            {
                if (Enum.IsDefined(typeof(Entities.Enums.TipoPessoa), entity.IdTipoPessoa))
                {
                    return true;
                }
                Notificar("O tipo de pessoa informada não está entre as permitidas! Verifique as informações e tente novamente.");

                return false;
            }
            catch (OverflowException ex)
            {
                Notificar("O tipo de pessoa informada não está entre as permitidas! Verifique as informações e tente novamente.");
                return false;
            }

        }
        public void Dispose()
        {
           _pessoaRepository?.Dispose();
        }
    }
     
}

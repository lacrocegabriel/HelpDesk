using FluentValidation;
using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Interfaces.Validators;
using HelpDesk.Business.Models;

namespace HelpDesk.Business.Validator.Validators
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

            return true;

        }
        public void Dispose()
        {
           _pessoaRepository?.Dispose();
        }
    }
     
}

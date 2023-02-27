using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Interfaces.Services;
using HelpDesk.Business.Models;

namespace HelpDesk.Business.Services
{
    public class PessoaService : IPessoaService
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IEnderecoRepository _enderecoRepository;

        public PessoaService(IPessoaRepository pessoaRepository, IEnderecoRepository enderecoRepository)
        {
            _pessoaRepository = pessoaRepository;
            _enderecoRepository = enderecoRepository;
        }

        public async Task Adicionar(Pessoa pessoa)
        {
            await _enderecoRepository.Adicionar(pessoa.Endereco);

            await _pessoaRepository.Adicionar(pessoa);
        }

        public async Task Atualizar(Pessoa pessoa)
        {
            await _enderecoRepository.Atualizar(pessoa.Endereco);

            await  _pessoaRepository.Atualizar(pessoa);
        }

        public Task AtualizarEndereco(Endereco endereco)
        {
            throw new NotImplementedException();
        }

        public async Task Remover(Guid id)
        {
            await _pessoaRepository.Remover(id);   
        }

        public void Dispose()
        {
            _pessoaRepository?.Dispose();
        }
    }
}

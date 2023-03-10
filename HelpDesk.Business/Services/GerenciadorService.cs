using HelpDesk.Business.Interfaces;
using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Interfaces.Services;
using HelpDesk.Business.Interfaces.Validators;
using HelpDesk.Business.Models;
using HelpDesk.Business.Models.Validations;
using HelpDesk.Business.Validator;

namespace HelpDesk.Business.Services
{
    public class GerenciadorService : IGerenciadorService
    {
        private readonly IGerenciadorRepository _gerenciadorRepository;
        private readonly IGerenciadorValidator _gerenciadorValidator;
        private readonly IEnderecoRepository _enderecoRepository;

        public GerenciadorService(IGerenciadorRepository gerenciadorRepository,
                                  IEnderecoRepository enderecoRepository,
                                  IGerenciadorValidator gerenciadorValidator)
        {
            _gerenciadorRepository = gerenciadorRepository;
            _enderecoRepository = enderecoRepository;
            _gerenciadorValidator = gerenciadorValidator;
        }

        public async Task Adicionar(Gerenciador gerenciador)
        {
            if (!_gerenciadorValidator.ValidaGerenciador(new AdicionarGerenciadorValidation(), gerenciador)) return;

            await _gerenciadorRepository.Adicionar(gerenciador);
        }

        public async Task Atualizar(Gerenciador gerenciador)
        {
            if (!_gerenciadorValidator.ValidaGerenciador(new AtualizarGerenciadorValidation(), gerenciador)) return;

            await _gerenciadorRepository.Atualizar(gerenciador);
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            if (!_gerenciadorValidator.ValidaEnderecoGerenciador(new EnderecoValidator(), endereco)) return;

            await _enderecoRepository.Atualizar(endereco);
        }

        public async Task Remover(Guid idGerenciador)
        {
            if (!_gerenciadorValidator.ValidaExclusaoGerenciador(idGerenciador)) return;

            await _gerenciadorRepository.Remover(idGerenciador);   
        }

        public void Dispose()
        {
            _gerenciadorRepository?.Dispose();
        }

       
    }
}

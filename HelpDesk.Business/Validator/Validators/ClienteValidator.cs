using FluentValidation;
using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Interfaces.Validators;
using HelpDesk.Business.Models;
using HelpDesk.Business.Validator.Validators.DocumentoValidators;

namespace HelpDesk.Business.Validator.Validators
{
    public class ClienteValidator : PessoaValidator<Cliente>, IClienteValidator
    {
        private readonly IChamadoRepository _chamadoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        
        public ClienteValidator(IChamadoRepository chamadoRepository,
                                INotificador notificador,
                                IPessoaRepository pessoaRepository,
                                IUsuarioRepository usuarioRepository) : base (pessoaRepository,notificador) 
        {
            _chamadoRepository = chamadoRepository;
            _usuarioRepository = usuarioRepository;
        }
        public bool ValidaPermissaoVisualizacao(Cliente cliente, List<Guid> idGerenciadoresUsuario)
        {
            if (!idGerenciadoresUsuario.Contains(cliente.IdGerenciador))
            {
                Notificar("O usuário não possui permissão para visualizar o cliente selecionado");
                return false;
            }

            return true;
        }
        public bool ValidaPermissaoInsercaoEdicao(Cliente cliente, List<Guid> idGerenciadoresUsuario)
        {
            if (!idGerenciadoresUsuario.Contains(cliente.IdGerenciador))
            {
                Notificar("O usuário não possui permissão para gerenciar um cliente para o gerenciador informado");
                return false;
            }

            return true;
        }
        public async Task<bool> ValidaExclusaoCliente(Guid idCliente)
        {
            var chamadosExistentes = await _chamadoRepository.Buscar(c => c.IdCliente == idCliente);

            if (chamadosExistentes.Any())
            {
                string mensagem = "Não é possível excluir o gerenciador, pois esta vinculado nos seguintes chamados: ";

                foreach (var c in chamadosExistentes)
                {
                    mensagem = mensagem + "Id: " + c.Id + " Título: " + c.Titulo;
                }

                Notificar(mensagem);

                return false;
            }

            var usuariosExistentes = await _usuarioRepository.ObterUsuariosPorCliente(idCliente);

            if (usuariosExistentes.Any())
            {
                string mensagem = "Não é possível excluir o cliente, pois esta vinculado nos seguintes usuários: ";

                foreach (var c in usuariosExistentes)
                {
                    mensagem = mensagem + "Id: " + c.Id + " Título: " + c.Nome;
                }

                Notificar(mensagem);

                return false;
            }

            return true;

        }
    }

    public class ClienteValidation : AbstractValidator<Cliente>
    {
        public ClienteValidation()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(10, 50)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.DataNascimentoConstituicao)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(c => c.IdTipoPessoa)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(c => c.Endereco)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            When(c => c.IdTipoPessoa == 2, () =>
            {
                RuleFor(c => c.Documento.Length).Equal(CpfValidacao.TamanhoCpf)
                    .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
                RuleFor(c => CpfValidacao.Validar(c.Documento)).Equal(true)
                    .WithMessage("O documento fornecido é inválido.");
            });

            When(c => c.IdTipoPessoa == 1, () =>
            {
                RuleFor(c => c.Documento.Length).Equal(CnpjValidacao.TamanhoCnpj)
                    .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
                RuleFor(c => CnpjValidacao.Validar(c.Documento)).Equal(true)
                    .WithMessage("O documento fornecido é inválido.");
            });

            RuleFor(c => c.IdGerenciador)
                .NotEmpty().WithMessage("O campo gerenciador precisa ser fornecido");

        }
    }    
}

using FluentValidation;
using HelpDesk.Domain.Entities;
using HelpDesk.Domain.Interfaces.Repositories;
using HelpDesk.Domain.Interfaces.Validators;
using HelpDesk.Domain.Validator.Validators.DocumentoValidators;

namespace HelpDesk.Domain.Validator.Validators
{
    public class GerenciadorValidator : PessoaValidator<Gerenciador>, IGerenciadorValidator
    {
        private readonly IChamadoRepository _chamadoRepository;
        private readonly ISetorRepository _setorRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public GerenciadorValidator(IPessoaRepository pessoaRepository,
                                    INotificador notificador,
                                    IChamadoRepository chamadoRepository,
                                    ISetorRepository setorRepository,
                                    IClienteRepository clienteRepository,
                                    IUsuarioRepository usuarioRepository) : base(pessoaRepository, notificador)
        {
            _chamadoRepository = chamadoRepository;
            _setorRepository = setorRepository;
            _clienteRepository = clienteRepository;
            _usuarioRepository = usuarioRepository;
        }
        public bool ValidaPermissaoVisualizacao(Gerenciador gerenciador, List<Guid> idGerenciadoresUsuario)
        {
            if (!idGerenciadoresUsuario.Contains(gerenciador.Id))
            {
                Notificar("O usuário não possui permissão para visualizar o gerenciador selecionado");
                return false;
            }

            return true;
        }
        public async Task<bool> ValidaExclusaoGerenciador(Guid idGerenciador)
        {
            var clientesExistentes = await _clienteRepository.Buscar(c => c.IdGerenciador == idGerenciador);

            if (clientesExistentes.Any())
            {
                string mensagem = "Não é possível excluir o gerenciador, pois esta vinculado nos seguintes clientes: ";

                foreach (var c in clientesExistentes)
                {
                    mensagem = mensagem + "Id: " + c.Id + " Nome: " + c.Nome;
                }

                Notificar(mensagem);
                return false;
            }

            var setorGerenciador = await _setorRepository.Buscar(s => s.IdGerenciador == idGerenciador);

            if (setorGerenciador.Any())
            {
                string mensagem = "Não é possível excluir o gerenciador, pois esta vinculado nos seguintes setores: ";

                foreach (var s in setorGerenciador)
                {
                    mensagem = mensagem + "Id: " + s.Id + " Descricao: " + s.Descricao;
                }

                Notificar(mensagem);
                return false;
            }

            var chamadosExistentes = await _chamadoRepository.Buscar(c => c.IdGerenciador == idGerenciador);

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

            var usuariosExistentes = await _usuarioRepository.ObterUsuariosPorGerenciador(idGerenciador);

            if (usuariosExistentes.Any())
            {
                string mensagem = "Não é possível excluir o gerenciador, pois esta vinculado nos seguintes usuários: ";

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

    public class GerenciadorValidation : AbstractValidator<Gerenciador> 
    {
        public GerenciadorValidation()
        {
            RuleFor(g => g.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100)
                .WithMessage("O campo {PropertyName} precisa c");

            RuleFor(g => g.Email)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(10, 50)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(g => g.DataNascimentoConstituicao)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(g => g.IdTipoPessoa)
                .NotEmpty().WithMessage("O tipo da pessoa precisa ser fornecido");

            RuleFor(g => g.Endereco)
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

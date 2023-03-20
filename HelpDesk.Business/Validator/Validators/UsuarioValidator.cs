using FluentValidation;
using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Interfaces.Validators;
using HelpDesk.Business.Models;
using HelpDesk.Business.Validator.Validators.DocumentoValidators;

namespace HelpDesk.Business.Validator.Validators
{
    public class UsuarioValidator : PessoaValidator<Usuario>, IUsuarioValidator
    {
        private readonly IChamadoRepository _chamadoRepository;
        private readonly IGerenciadorRepository _gerenciadorRepository;
        private readonly IClienteRepository _clienteRepository;
        public UsuarioValidator(IPessoaRepository pessoaRepository,
                                INotificador notificador,
                                IChamadoRepository chamadoRepository,
                                IGerenciadorRepository gerenciadorRepository,
                                IClienteRepository clienteRepository) : base(pessoaRepository, notificador)
        {
            _chamadoRepository = chamadoRepository;
            _gerenciadorRepository = gerenciadorRepository;
            _clienteRepository = clienteRepository;
        }

        public async Task<bool> ValidaGerenciadoresClientesUsuario(IEnumerable<Gerenciador> Gerenciadores, IEnumerable<Cliente> Clientes)
        {
            if(!Gerenciadores.Any())
            {
                Notificar("É necessário informar ao menos um gerenciador para o usuário");
                return false;
            }

            if (!Clientes.Any())
            {
                Notificar("É necessário informar ao menos um cliente para o usuário");
                return false;
            }

            foreach(var g in Gerenciadores)
            {
                var gerenciador = await _gerenciadorRepository.ObterPorId(g.Id);
                if(gerenciador == null)
                {
                    Notificar("O gerencidador de Id: " + g.Id + " não se encontra cadastrado!");
                    return false;
                }
            }

            foreach (var c in Clientes)
            {
                var cliente = await _clienteRepository.ObterPorId(c.Id);
                if (cliente == null)
                {
                    Notificar("O cliente de Id: " + c.Id + " não se encontra cadastrado!");
                    return false;
                }
            }

            return true;

        }

        public async Task<bool> ValidaExclusaoUsuario(Guid idUsuario)
        {
            var chamados = await _chamadoRepository.ObterChamadosPorUsuarioGerador(idUsuario);

            chamados.Concat(await _chamadoRepository.ObterChamadosPorUsuarioResponsavel(idUsuario));

           if (chamados.Any())
            {
                string mensagem = "Não é possível excluir o gerenciador, pois esta vinculado nos seguintes chamados: ";

                foreach (var c in chamados)
                {
                    mensagem = mensagem + "Id: " + c.Id + " Número: " + c.Numero + ", ";
                }

                Notificar(mensagem);

                return false;
            };


            return true;
        }
    }

    public class UsuarioValidation : AbstractValidator<Usuario>
    {
        public UsuarioValidation()
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

            RuleFor(g => g.IdSetor)
                 .NotEmpty().WithMessage("O setor do usuário precisa ser fornecido");

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

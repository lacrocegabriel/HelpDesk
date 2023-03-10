using FluentValidation;
using HelpDesk.Business.Interfaces;
using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Interfaces.Validators;
using HelpDesk.Business.Models.Validations.DocumentoValidation;
using HelpDesk.Business.Validator;

namespace HelpDesk.Business.Models.Validations
{
    public class GerenciadorValidator : BaseValidator, IGerenciadorValidator
    {
        private readonly IGerenciadorRepository _gerenciadorRepository;
        private readonly IChamadoRepository _chamadoRepository;
        private readonly ISetorRepository _setorRepository;
        private readonly IClienteRepository _clienteRepository;

        public GerenciadorValidator(IGerenciadorRepository gerenciadorRepository,
                                  INotificador notificador,
                                 IChamadoRepository chamadoRepository,
                                  ISetorRepository setorRepository,
                                  IClienteRepository clienteRepository) : base(notificador)
        {
            _gerenciadorRepository = gerenciadorRepository;
            _chamadoRepository = chamadoRepository;
            _setorRepository = setorRepository;
            _clienteRepository = clienteRepository;
        }

        public bool ValidaGerenciador(AbstractValidator<Gerenciador> validator, Gerenciador gerenciador)
        {
            if(!ExecutarValidacao(validator, gerenciador)
                || !ValidaInsercaoEdicaoGerenciador(gerenciador)) return false;


            if (gerenciador.Endereco != null)
            {
                return ValidaEnderecoGerenciador(new EnderecoValidator(),gerenciador.Endereco);
            }

            return true;
        }

        public bool ValidaExclusaoGerenciador(Guid idGerenciador)
        {
            var clientesExistentes = _clienteRepository.Buscar(c => c.IdGerenciador == idGerenciador).Result.ToList();

            if (clientesExistentes.Count > 0)
            {
                string mensagem = "Não é possível excluir o gerenciador, pois esta vinculado nos seguintes clientes: ";

                foreach (var c in clientesExistentes)
                {
                    mensagem = mensagem + "Id: " + c.Id + " Nome: " + c.Nome;
                }

                Notificar(mensagem);
                return false;
            }

            var setorGerenciador = _setorRepository.Buscar(c => c.IdGerenciador == idGerenciador).Result.ToList();

            if (setorGerenciador.Count > 0)
            {
                string mensagem = "Não é possível excluir o gerenciador, pois esta vinculado nos seguintes setores: ";

                foreach (var c in setorGerenciador)
                {
                    mensagem = mensagem + "Id: " + c.Id + " Descricao: " + c.Descricao;
                }

                Notificar(mensagem);
                return false;
            }

            var chamadosExistentes = _chamadoRepository.Buscar(c => c.IdGerenciador == idGerenciador).Result.ToList();

            if (chamadosExistentes.Count > 0)
            {
                string mensagem = "Não é possível excluir o gerenciador, pois esta vinculado nos seguintes chamados: ";

                foreach (var c in chamadosExistentes)
                {
                    mensagem = mensagem + "Id: " + c.Id + " Título: " + c.Titulo;
                }

                Notificar(mensagem);

                return false;
            }

            return true;

        }

        public bool ValidaEnderecoGerenciador(AbstractValidator<Endereco> validator, Endereco endereco)
        {
            if (!ExecutarValidacao(validator, endereco)) return false;

            return true;
        }
       
        private bool ValidaInsercaoEdicaoGerenciador(Gerenciador gerenciador)
        {
            var gerenciadorMesmoDocumento = _gerenciadorRepository.Buscar(g => g.Documento == gerenciador.Documento && g.Id != gerenciador.Id).Result.FirstOrDefault();

            if (gerenciadorMesmoDocumento != null)
            {
                Notificar("O documento ja esta informado no seguinte gerenciador: " + "Id: " + gerenciadorMesmoDocumento.Id + " Nome: " + gerenciadorMesmoDocumento.Nome);
                return false;
            }

            var gerenciadorMesmoEmail = _gerenciadorRepository.Buscar(g => g.Email == gerenciador.Email && g.Id != gerenciador.Id).Result.FirstOrDefault();

            if (gerenciadorMesmoEmail != null)
            {
                Notificar("O email ja esta informado no seguinte gerenciador: " + "Id: " + gerenciadorMesmoEmail.Id + " Nome: " + gerenciadorMesmoEmail.Nome);

                return false;
            }

            return true;

        }
        
    }

    public class AdicionarGerenciadorValidation : AbstractValidator<Gerenciador>
    {
        public AdicionarGerenciadorValidation()
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

            RuleFor(g => g.IdEndereco)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

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

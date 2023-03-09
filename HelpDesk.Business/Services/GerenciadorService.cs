using HelpDesk.Business.Interfaces;
using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Interfaces.Services;
using HelpDesk.Business.Models;
using HelpDesk.Business.Models.Validations;

namespace HelpDesk.Business.Services
{
    public class GerenciadorService : BaseService,IGerenciadorService
    {
        private readonly IGerenciadorRepository _gerenciadorRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IChamadoRepository _chamadoRepository;
        private readonly ISetorRepository _setorRepository;
        private readonly IClienteRepository _clienteRepository;

        public GerenciadorService(IGerenciadorRepository gerenciadorRepository,
                                  INotificador notificador,
                                  IEnderecoRepository enderecoRepository,
                                  IChamadoRepository chamadoRepository,
                                  ISetorRepository setorRepository,
                                  IClienteRepository clienteRepository) : base(notificador)
        {
            _gerenciadorRepository = gerenciadorRepository;
            _enderecoRepository = enderecoRepository;
            _chamadoRepository = chamadoRepository;
            _setorRepository = setorRepository;
            _clienteRepository = clienteRepository;
        }

        public async Task Adicionar(Gerenciador gerenciador)
        {
            if (!ExecutarValidacao(new AdicionarGerenciadorValidation(), gerenciador)
                || !ValidaInsercaoEdicaoGerenciador(gerenciador)
                || !ExecutarValidacao(new EnderecoValidation(), gerenciador.Endereco)) return;

            await _gerenciadorRepository.Adicionar(gerenciador);
        }

        public async Task Atualizar(Gerenciador gerenciador)
        {
            if (!ExecutarValidacao(new AtualizarGerenciadorValidation(), gerenciador)
                || !ValidaInsercaoEdicaoGerenciador(gerenciador)) return;

            await _gerenciadorRepository.Atualizar(gerenciador);
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            if (!ExecutarValidacao(new EnderecoValidation(), endereco)) return;

            await _enderecoRepository.Atualizar(endereco);
        }

        public async Task Remover(Guid idGerenciador)
        {
            if (!ValidaExclusaoGerenciador(idGerenciador)) return;

            await _gerenciadorRepository.Remover(idGerenciador);   
        }

        public void Dispose()
        {
            _gerenciadorRepository?.Dispose();
        }

        private bool ValidaInsercaoEdicaoGerenciador (Gerenciador gerenciador)
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

        private bool ValidaExclusaoGerenciador(Guid idGerenciador)
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
    }
}

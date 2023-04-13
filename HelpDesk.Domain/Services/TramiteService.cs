using HelpDesk.Domain.Entities;
using HelpDesk.Domain.Interfaces.Others;
using HelpDesk.Domain.Interfaces.Repositories;
using HelpDesk.Domain.Interfaces.Services;
using HelpDesk.Domain.Interfaces.Validators;
using HelpDesk.Domain.Validator.Validators;

namespace HelpDesk.Domain.Services
{
    public class TramiteService : ServiceBase<Tramite>, ITramiteService
    {
        private readonly ITramiteRepository _tramiteRepository;
        private readonly IChamadoRepository _chamadoRepository;
        private readonly ITramiteValidator  _tramiteValidator;
        private readonly IChamadoValidator _chamadoValidator;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUser _user;

        public TramiteService(ITramiteRepository tramiteRepository,
                              ITramiteValidator  tramiteValidator,
                              IChamadoValidator chamadoValidator,
                              IUser user,
                              IUsuarioRepository usuarioRepository, 
                              IChamadoRepository chamadoRepository) : base(tramiteRepository)
        {
            _tramiteRepository = tramiteRepository;
            _tramiteValidator = tramiteValidator;
            _chamadoValidator = chamadoValidator;
            _user = user;
            _usuarioRepository = usuarioRepository;
            _chamadoRepository = chamadoRepository;
        }
        public async Task<IEnumerable<Tramite>> ObterTodos(int skip, int take)
        {
            var usuario = await _usuarioRepository.ObterUsuarioGerenciadoresClientes(_user.GetUserId());

            var (idGerenciadores, idClientes) = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(usuario.Id);

            var tramites = await _tramiteRepository.ObterTramitesPorPermissao(idGerenciadores, idClientes, skip, take);

            return tramites;

        }

        public async Task<Tramite?> ObterPorId(Guid id)
        {
            var usuario = await _usuarioRepository.ObterUsuarioGerenciadoresClientes(_user.GetUserId());

            var (idGerenciadores, idClientes) = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(usuario.Id);

            var tramite = await _tramiteRepository.ObterTramiteChamado(id);

            return _chamadoValidator.ValidaPermissaoVisualizacao(tramite.Chamado, idGerenciadores, idClientes) ? tramite : null;
        }

        public async Task Adicionar(Tramite tramite)
        {
            if (await _tramiteValidator.ValidaExistenciaTramite(tramite.Id)
                || !await _tramiteValidator.ValidaTramite(new TramiteValidation(), tramite)) return;

            tramite.Chamado = await _chamadoRepository.ObterPorId(tramite.IdChamado);

            tramite.Chamado.IdSituacaoChamado = tramite.IdSituacaoChamado;

            var usuario = await _usuarioRepository.ObterUsuarioGerenciadoresClientes(_user.GetUserId());

            var (idGerenciadoresUsuario, idClientesUsuario) = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(usuario.Id);

            var (idGerenciadoresUsuarioResponsavel, idClientesUsuarioResponsavel) = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(tramite.Chamado.IdUsuarioResponsavel);

            if (!_chamadoValidator.ValidaPermissaoInsercaoEdicao(tramite.Chamado, idGerenciadoresUsuario, idClientesUsuario,
                   idGerenciadoresUsuarioResponsavel, idClientesUsuarioResponsavel)
                || !await _tramiteValidator.ValidaSituacaoChamado(tramite)) return;

            await _tramiteRepository.AdicionarTramite(tramite);
        }

        public async Task Atualizar(Tramite tramite)
        {
            if (!await _tramiteValidator.ValidaTramite(new TramiteValidation(), tramite)) return;

            tramite.Chamado = await _chamadoRepository.ObterPorId(tramite.IdChamado);

            tramite.Chamado.IdSituacaoChamado = tramite.IdSituacaoChamado;

            var usuario = await _usuarioRepository.ObterUsuarioGerenciadoresClientes(_user.GetUserId());

            var (idGerenciadoresUsuario, idClientesUsuario) = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(usuario.Id);

            var (idGerenciadoresUsuarioResponsavel, idClientesUsuarioResponsavel) = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(tramite.Chamado.IdUsuarioResponsavel);

            if (!_chamadoValidator.ValidaPermissaoInsercaoEdicao(tramite.Chamado, idGerenciadoresUsuario, idClientesUsuario,
                   idGerenciadoresUsuarioResponsavel, idClientesUsuarioResponsavel)
                || !await _tramiteValidator.ValidaSituacaoChamado(tramite)) return;

            await _tramiteRepository.AtualizarTramite(tramite);
        }
    }
}

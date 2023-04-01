using HelpDesk.Domain.Interfaces.Others;
using HelpDesk.Domain.Interfaces.Repositories;
using HelpDesk.Domain.Interfaces.Services;
using HelpDesk.Domain.Interfaces.Validators;
using HelpDesk.Domain.Models;
using HelpDesk.Domain.Validator.Validators;

namespace HelpDesk.Domain.Services
{
    public class TramiteService : ITramiteService
    {
        private readonly ITramiteRepository _tramiteRepository;
        private readonly ITramiteValidator  _tramiteValidator;
        private readonly IChamadoValidator _chamadoValidator;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUser _user;

        public TramiteService(ITramiteRepository tramiteRepository,
                              ITramiteValidator  tramiteValidator,
                              IChamadoValidator chamadoValidator,
                              IUser user,
                              IUsuarioRepository usuarioRepository)
        {
            _tramiteRepository = tramiteRepository;
            _tramiteValidator = tramiteValidator;
            _chamadoValidator = chamadoValidator;
            _user = user;
            _usuarioRepository = usuarioRepository;
        }
        public async Task<IEnumerable<Tramite>> ObterTodos(int skip, int take)
        {
            var usuario = await _usuarioRepository.ObterUsuarioPorAutenticacao(_user.GetUserId());

            var (IdGerenciadores, IdClientes) = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(usuario.Id);

            return await _tramiteRepository.ObterTramitesPorPermissao(IdGerenciadores, IdClientes, skip, take);

        }

        public async Task<Tramite?> ObterPorId(Guid id)
        {
            var usuario = await _usuarioRepository.ObterUsuarioPorAutenticacao(_user.GetUserId());

            var (IdGerenciadores, IdClientes) = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(usuario.Id);

            var tramite = await _tramiteRepository.ObterTramiteChamado(id);

            if (_chamadoValidator.ValidaPermissaoVisualizacao(tramite.Chamado, IdGerenciadores, IdClientes))
            {
                return tramite;
            }

            return null;
        }

        public async Task Adicionar(Tramite tramite)
        {
            var usuario = await _usuarioRepository.ObterUsuarioPorAutenticacao(_user.GetUserId());

            var (IdGerenciadoresUsuario, IdClientesUsuario) = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(usuario.Id);

            var (IdGerenciadoresUsuarioResponsavel, IdClientesUsuarioResponsavel) = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(tramite.Chamado.IdUsuarioResponsavel);

            if (await _tramiteValidator.ValidaExistenciaTramite(tramite.Id)
                || !await _tramiteValidator.ValidaTramite(new TramiteValidation(), tramite)
                || !_chamadoValidator.ValidaPermissaoInsercaoEdicao(tramite.Chamado, IdGerenciadoresUsuario, IdClientesUsuario,
                   IdGerenciadoresUsuarioResponsavel, IdClientesUsuarioResponsavel)) return;

            await _tramiteRepository.AdicionarTramite(tramite);
        }

        public async Task Atualizar(Tramite tramite)
        {
            var usuario = await _usuarioRepository.ObterUsuarioPorAutenticacao(_user.GetUserId());

            var (IdGerenciadoresUsuario, IdClientesUsuario) = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(usuario.Id);

            var (IdGerenciadoresUsuarioResponsavel, IdClientesUsuarioResponsavel) = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(tramite.Chamado.IdUsuarioResponsavel);

            if (!await _tramiteValidator.ValidaTramite(new TramiteValidation(), tramite)
                || !_chamadoValidator.ValidaPermissaoInsercaoEdicao(tramite.Chamado, IdGerenciadoresUsuario, IdClientesUsuario,
                   IdGerenciadoresUsuarioResponsavel, IdClientesUsuarioResponsavel)) return;

            await _tramiteRepository.AtualizarTramite(tramite);
        }
        public void Dispose()
        {
            _tramiteRepository?.Dispose();
        }
    }
}

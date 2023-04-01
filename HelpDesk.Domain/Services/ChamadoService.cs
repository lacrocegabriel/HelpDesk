using HelpDesk.Domain.Entities;
using HelpDesk.Domain.Interfaces.Others;
using HelpDesk.Domain.Interfaces.Repositories;
using HelpDesk.Domain.Interfaces.Services;
using HelpDesk.Domain.Interfaces.Validators;
using HelpDesk.Domain.Validator.Validators;

namespace HelpDesk.Domain.Services
{
    public class ChamadoService : ServiceBase<Chamado> , IChamadoService
    {
        private readonly IChamadoRepository _chamadoRepository;
        private readonly IChamadoValidator  _chamadoValidator;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUser _user;

        public ChamadoService(IChamadoRepository chamadoRepository,
                              IChamadoValidator chamadoValidator,
                              IUser user,
                              IUsuarioRepository usuarioRepository) : base(chamadoRepository)
        {
            _chamadoRepository = chamadoRepository;
            _chamadoValidator = chamadoValidator;
            _user = user;
            _usuarioRepository= usuarioRepository;
        }

        public async Task<IEnumerable<Chamado>> ObterTodos(int skip, int take)
        {
            var usuario = await _usuarioRepository.ObterUsuarioGerenciadoresClientes(_user.GetUserId());

            var (idGerenciadores, idClientes) = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(usuario.Id);

            return await _chamadoRepository.ObterChamadosPorPermissao(idGerenciadores, idClientes, skip, take);               
            
        }

        public async Task<Chamado?> ObterPorId(Guid id)
        {
            var usuario = await _usuarioRepository.ObterUsuarioGerenciadoresClientes(_user.GetUserId());

            var (idGerenciadores, idClientes) = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(usuario.Id);

            var chamado = await _chamadoRepository.ObterPorId(id);

            return _chamadoValidator.ValidaPermissaoVisualizacao(chamado, idGerenciadores, idClientes) ? chamado : null;
        }

        public async Task Adicionar(Chamado chamado)
        {
            var usuario = await _usuarioRepository.ObterUsuarioGerenciadoresClientes(_user.GetUserId());

            var (idGerenciadoresUsuario, idClientesUsuario) = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(usuario.Id);

            var (idGerenciadoresUsuarioResponsavel, idClientesUsuarioResponsavel) = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(chamado.IdUsuarioResponsavel);

            if (await _chamadoValidator.ValidaExistenciaChamado(chamado.Id) 
                || !_chamadoValidator.ValidaChamado(new ChamadoValidation(), chamado)
                || !_chamadoValidator.ValidaPermissaoInsercaoEdicao(chamado, idGerenciadoresUsuario, idClientesUsuario, 
                   idGerenciadoresUsuarioResponsavel, idClientesUsuarioResponsavel)) return;

            await _chamadoRepository.Adicionar(chamado);
        }

        public async Task Atualizar(Chamado chamado)
        {
            var usuario = await _usuarioRepository.ObterUsuarioGerenciadoresClientes(_user.GetUserId());

            var (idGerenciadoresUsuario, idClientesUsuario) = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(usuario.Id);

            var (idGerenciadoresUsuarioResponsavel, idClientesUsuarioResponsavel) = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(chamado.IdUsuarioResponsavel);

            if (!_chamadoValidator.ValidaChamado(new ChamadoValidation(), chamado)
                || !_chamadoValidator.ValidaPermissaoInsercaoEdicao(chamado, idGerenciadoresUsuario, idClientesUsuario,
                   idGerenciadoresUsuarioResponsavel, idClientesUsuarioResponsavel)) return;

            await _chamadoRepository.Atualizar(chamado);
        }
        
    }
}

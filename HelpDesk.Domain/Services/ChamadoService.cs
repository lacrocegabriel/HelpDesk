using HelpDesk.Domain.Entities;
using HelpDesk.Domain.Interfaces.Others;
using HelpDesk.Domain.Interfaces.Repositories;
using HelpDesk.Domain.Interfaces.Services;
using HelpDesk.Domain.Interfaces.Validators;
using HelpDesk.Domain.Validator.Validators;

namespace HelpDesk.Domain.Services
{
    public class ChamadoService : IChamadoService
    {
        private readonly IChamadoRepository _chamadoRepository;
        private readonly IChamadoValidator _chamadoValidator;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUser _user;

        public ChamadoService(IChamadoRepository chamadoRepository,
                              IChamadoValidator chamadoValidator,
                              IUser user,
                              IUsuarioRepository usuarioRepository)
        {
            _chamadoRepository = chamadoRepository;
            _chamadoValidator = chamadoValidator;
            _user = user;
            _usuarioRepository= usuarioRepository;
        }

        public async Task<IEnumerable<Chamado>> ObterTodos(int skip, int take)
        {
            var usuario = await _usuarioRepository.ObterUsuarioPorAutenticacao(_user.GetUserId());

            var (IdGerenciadores, IdClientes) = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(usuario.Id);

            return await _chamadoRepository.ObterChamadosPorPermissao(IdGerenciadores, IdClientes, skip, take);               
            
        }

        public async Task<Chamado?> ObterPorId(Guid id)
        {
            var usuario = await _usuarioRepository.ObterUsuarioPorAutenticacao(_user.GetUserId());

            var (IdGerenciadores, IdClientes) = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(usuario.Id);

            var chamado = await _chamadoRepository.ObterPorId(id);

            if(_chamadoValidator.ValidaPermissaoVisualizacao(chamado, IdGerenciadores, IdClientes))
            {
                return chamado;
            }

            return null; 
        }

        public async Task Adicionar(Chamado chamado)
        {
            var usuario = await _usuarioRepository.ObterUsuarioPorAutenticacao(_user.GetUserId());

            var (IdGerenciadoresUsuario, IdClientesUsuario) = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(usuario.Id);

            var (IdGerenciadoresUsuarioResponsavel, IdClientesUsuarioResponsavel) = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(chamado.IdUsuarioResponsavel);

            if (await _chamadoValidator.ValidaExistenciaChamado(chamado.Id) 
                || !_chamadoValidator.ValidaChamado(new ChamadoValidation(), chamado)
                || !_chamadoValidator.ValidaPermissaoInsercaoEdicao(chamado, IdGerenciadoresUsuario, IdClientesUsuario, 
                   IdGerenciadoresUsuarioResponsavel, IdClientesUsuarioResponsavel)) return;

            await _chamadoRepository.Adicionar(chamado);
        }

        public async Task Atualizar(Chamado chamado)
        {
            var usuario = await _usuarioRepository.ObterUsuarioPorAutenticacao(_user.GetUserId());

            var (IdGerenciadoresUsuario, IdClientesUsuario) = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(usuario.Id);

            var (IdGerenciadoresUsuarioResponsavel, IdClientesUsuarioResponsavel) = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(chamado.IdUsuarioResponsavel);

            if (!_chamadoValidator.ValidaChamado(new ChamadoValidation(), chamado)
                || !_chamadoValidator.ValidaPermissaoInsercaoEdicao(chamado, IdGerenciadoresUsuario, IdClientesUsuario,
                   IdGerenciadoresUsuarioResponsavel, IdClientesUsuarioResponsavel)) return;

            await _chamadoRepository.Atualizar(chamado);
        }
        public void Dispose()
        {
            _chamadoRepository?.Dispose();  
        }        
    }
}

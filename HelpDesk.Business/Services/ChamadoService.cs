using HelpDesk.Business.Interfaces.Others;
using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Interfaces.Services;
using HelpDesk.Business.Interfaces.Validators;
using HelpDesk.Business.Models;
using HelpDesk.Business.Validator.Validators;
using System.Runtime.CompilerServices;

namespace HelpDesk.Business.Services
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

            return await _chamadoRepository.ObterChamadosPorPermissao(usuario);               
            
        }

        public async Task Adicionar(Chamado chamado)
        {
            var usuario = await _usuarioRepository.ObterUsuarioPorAutenticacao(_user.GetUserId());

            var gerenciadoresClientesUsuario = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(usuario.Id);

            var gerenciadoresClientesUsuarioResponsavel = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(chamado.IdUsuarioResponsavel);

            if (await _chamadoValidator.ValidaExistenciaChamado(chamado.Id) 
                || !await _chamadoValidator.ValidaChamado(new ChamadoValidation(), chamado)
                || _chamadoValidator.ValidaPermissao(chamado, gerenciadoresClientesUsuario.IdGerenciadores, gerenciadoresClientesUsuario.IdClientes, 
                   gerenciadoresClientesUsuarioResponsavel.IdGerenciadores, gerenciadoresClientesUsuarioResponsavel.IdClientes)) return;

            await _chamadoRepository.Adicionar(chamado);
        }

        public async Task Atualizar(Chamado chamado)
        {
            var usuario = await _usuarioRepository.ObterUsuarioPorAutenticacao(_user.GetUserId());

            var gerenciadoresClientesUsuario = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(usuario.Id);

            var gerenciadoresClientesUsuarioResponsavel = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(chamado.IdUsuarioResponsavel);

            if (!await _chamadoValidator.ValidaChamado(new ChamadoValidation(), chamado)
                || _chamadoValidator.ValidaPermissao(chamado, gerenciadoresClientesUsuario.IdGerenciadores, gerenciadoresClientesUsuario.IdClientes,
                   gerenciadoresClientesUsuarioResponsavel.IdGerenciadores, gerenciadoresClientesUsuarioResponsavel.IdClientes)) return;

            await _chamadoRepository.Atualizar(chamado);
        }
        public void Dispose()
        {
            _chamadoRepository?.Dispose();  
        }        
    }
}

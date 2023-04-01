using HelpDesk.Domain.Interfaces.Others;
using HelpDesk.Domain.Interfaces.Repositories;
using HelpDesk.Domain.Interfaces.Services;
using HelpDesk.Domain.Interfaces.Validators;
using HelpDesk.Domain.Models;
using HelpDesk.Domain.Validator.Validators;

namespace HelpDesk.Domain.Services
{
    public class SetorService : ISetorService
    {
        private readonly ISetorRepository _setorRepository;
        private readonly ISetorValidator _setorValidator;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUser _user;

        public SetorService(ISetorRepository setorRepository,
                            ISetorValidator setorValidator,
                              IUsuarioRepository usuarioRepository,
                              IUser user)
        {
            _setorRepository = setorRepository;
            _setorValidator = setorValidator;
            _usuarioRepository = usuarioRepository;
            _user = user;
        }
        public async Task<IEnumerable<Setor>> ObterTodos(int skip, int take)
        {
            var usuario = await _usuarioRepository.ObterUsuarioPorAutenticacao(_user.GetUserId());

            var idGerenciadores = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(usuario.Id);

            return await _setorRepository.ObterSetoresPorPermissao(idGerenciadores.IdGerenciadores, skip, take);

        }

        public async Task<Setor?> ObterPorId(Guid id)
        {
            var usuario = await _usuarioRepository.ObterUsuarioPorAutenticacao(_user.GetUserId());

            var idGerenciadoresUsuario = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(usuario.Id);

            var setor = await _setorRepository.ObterPorId(id);

            if (_setorValidator.ValidaPermissaoVisualizacao(setor, idGerenciadoresUsuario.IdGerenciadores))
            {
                return setor;
            }

            return null;
        }
        public async Task Adicionar(Setor setor)
        {
            var usuario = await _usuarioRepository.ObterUsuarioPorAutenticacao(_user.GetUserId());

            var idGerenciadoresUsuario = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(usuario.Id);

            if (await _setorValidator.ValidaExistenciaSetor(setor.Id)
                ||!await _setorValidator.ValidaSetor(new SetorValidation(), setor)
                || !_setorValidator.ValidaPermissaoInsercaoEdicao(setor, idGerenciadoresUsuario.IdGerenciadores)) return;

            await _setorRepository.Adicionar(setor);
        }

        public async Task Atualizar(Setor setor)
        {
            var usuario = await _usuarioRepository.ObterUsuarioPorAutenticacao(_user.GetUserId());

            var idGerenciadoresUsuario = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(usuario.Id);

            if (!await _setorValidator.ValidaSetor(new SetorValidation(), setor)
                || !_setorValidator.ValidaPermissaoInsercaoEdicao(setor, idGerenciadoresUsuario.IdGerenciadores)) return;

            await _setorRepository.Atualizar(setor);
        }

        public async Task Remover(Guid id)
        {
            var usuario = await _usuarioRepository.ObterUsuarioPorAutenticacao(_user.GetUserId());

            var idGerenciadoresUsuario = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(usuario.Id);

            var setor = await _setorRepository.ObterPorId(id);

            if (!await _setorValidator.ValidaExclusaoSetor(id)
                || !_setorValidator.ValidaPermissaoInsercaoEdicao(setor, idGerenciadoresUsuario.IdGerenciadores)) return;

            await _setorRepository.Remover(id);
        }

        public void Dispose()
        {
            _setorRepository?.Dispose();
        }
    }
}

using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Interfaces.Services;
using HelpDesk.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Business.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPessoaService _pessoaService;

        public UsuarioService(IUsuarioRepository usuariorepository, IPessoaService pessoaService)
        {
            _usuarioRepository = usuariorepository;
            _pessoaService = pessoaService;
        }

        public async Task Adicionar(Usuario usuario)
        {
            await _usuarioRepository.Adicionar(usuario);

            await _pessoaService.Adicionar(usuario);
        }

        public async Task Atualizar(Usuario usuario)
        {
            await _usuarioRepository.Atualizar(usuario);

            await _pessoaService.Atualizar(usuario);
        }

        public Task AtualizarEndereco(Endereco endereco)
        {
            throw new NotImplementedException();
        }

        public async Task Remover(Guid id)
        {
            await _usuarioRepository.Remover(id);

            await _pessoaService.Remover(id);
        }

        public void Dispose()
        {
            _usuarioRepository?.Dispose();
        }
    }
}

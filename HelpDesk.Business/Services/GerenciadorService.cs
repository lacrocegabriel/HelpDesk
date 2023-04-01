﻿using HelpDesk.Business.Interfaces.Others;
using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Interfaces.Services;
using HelpDesk.Business.Interfaces.Validators;
using HelpDesk.Business.Models;
using HelpDesk.Business.Validator.Validators;

namespace HelpDesk.Business.Services
{
    public class GerenciadorService : IGerenciadorService
    {
        private readonly IGerenciadorRepository _gerenciadorRepository;
        private readonly IGerenciadorValidator _gerenciadorValidator;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUser _user;

        public GerenciadorService(IGerenciadorRepository gerenciadorRepository,
                                  IEnderecoRepository enderecoRepository,
                                  IGerenciadorValidator gerenciadorValidator,
                                  IUsuarioRepository usuarioRepository,
                                  IUser user)
        {
            _gerenciadorRepository = gerenciadorRepository;
            _enderecoRepository = enderecoRepository;
            _gerenciadorValidator = gerenciadorValidator;
            _usuarioRepository = usuarioRepository;
            _user = user;
        }
        public async Task<IEnumerable<Gerenciador>> ObterTodos(int skip, int take)
        {
            var usuario = await _usuarioRepository.ObterUsuarioPorAutenticacao(_user.GetUserId());

            var idGerenciadores = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(usuario.Id);

            return await _gerenciadorRepository.ObterGerenciadoresPorPermissao(idGerenciadores.IdGerenciadores, skip, take);

        }
        public async Task<Gerenciador?> ObterPorId(Guid id)
        {
            var usuario = await _usuarioRepository.ObterUsuarioPorAutenticacao(_user.GetUserId());

            var idGerenciadoresUsuario = await _usuarioRepository.ObterGerenciadoresClientesPermitidos(usuario.Id);

            var gerenciador = await _gerenciadorRepository.ObterPorId(id);

            if (_gerenciadorValidator.ValidaPermissaoVisualizacao(gerenciador, idGerenciadoresUsuario.IdGerenciadores))
            {
                return gerenciador;
            }

            return null;
        }

        public async Task Adicionar(Gerenciador gerenciador)
        {
            if (await _gerenciadorValidator.ValidaExistenciaPessoa(gerenciador.Id)
                || !await _gerenciadorValidator.ValidaPessoa(new GerenciadorValidation(), gerenciador)) return;

            await _gerenciadorRepository.Adicionar(gerenciador);
        }

        public async Task Atualizar(Gerenciador gerenciador)
        {
            if (!await _gerenciadorValidator.ValidaPessoa(new GerenciadorValidation(), gerenciador)) return;

            await _gerenciadorRepository.Atualizar(gerenciador);
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            if (!await _gerenciadorValidator.ValidaEnderecoPessoa(new EnderecoValidaton(), endereco)) return;

            await _enderecoRepository.Atualizar(endereco);
        }

        public async Task Remover(Guid idGerenciador)
        {
            if (!await _gerenciadorValidator.ValidaExclusaoGerenciador(idGerenciador)) return;

            await _gerenciadorRepository.Remover(idGerenciador);   
        }

        public void Dispose()
        {
            _gerenciadorRepository?.Dispose();
        }

       
    }
}

using AutoMapper;
using HelpDesk.Api.DTOs;
using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Interfaces.Services;
using HelpDesk.Business.Interfaces.Validators;
using HelpDesk.Business.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HelpDesk.Api.Controllers
{
    [Route("api/usuarios")]
    public class UsuariosController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public UsuariosController(IUsuarioRepository usuarioRepository,
                                  IUsuarioService usuarioService,
                                  IMapper mapper,
                                  INotificador notificador,
                                  SignInManager<IdentityUser> signInManager,
                                  UserManager<IdentityUser> userManager) : base(notificador)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioService = usuarioService;
            _mapper = mapper;
            _signInManager = signInManager;
        }

        [HttpGet("{skip:int}/{take:int}")]
        public async Task<IEnumerable<UsuarioDto>> ObterTodos([FromRoute] int skip = 0, int take = 25)
        {
            return _mapper.Map<IEnumerable<UsuarioDto>>(await _usuarioRepository.ObterTodos(skip, take));

        }

        [HttpGet("{id:guid}")]
        public async Task<UsuarioDto> ObterPorId(Guid id)
        {
            return _mapper.Map<UsuarioDto>(await _usuarioRepository.ObterPorId(id));

        }

        [HttpPost("registrar")]
        public async Task<ActionResult<UsuarioDto>> Registrar(UsuarioDto usuarioDto)
        {
            await _usuarioService.Adicionar(_mapper.Map<Usuario>(usuarioDto));
            
            return CustomResponse();

        }

        [HttpPost("entrar")]
        public async Task<ActionResult> Login(UsuarioDtoLogin usuarioDtoLogin)
        {
            var result = await _signInManager.PasswordSignInAsync(usuarioDtoLogin.Login, usuarioDtoLogin.Senha, isPersistent: false, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                return CustomResponse(usuarioDtoLogin);
            }
            if (result.IsLockedOut)
            {
                NotificateError("Usuário temporariamente bloqueado por excesso de tentativas inválidas");
                return CustomResponse(usuarioDtoLogin);
            }

            NotificateError("Usuario ou Senha incorretos");
            return CustomResponse(usuarioDtoLogin);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<UsuarioDto>> Atualizar(Guid id, UsuarioDto usuarioDto)
        {
            if (id != usuarioDto.Id)
            {
                NotificateError("O Id fornecido não corresponde ao Id enviado no usuário. Por favor, verifique se o Id está correto e tente novamente.");
                return CustomResponse();
            }

            await _usuarioService.Atualizar(_mapper.Map<Usuario>(usuarioDto));

            return CustomResponse();

        }

        [HttpPut("atualizar-endereco/{id:guid}")]
        public async Task<IActionResult> AtualizarEndereco(Guid id, UsuarioDto usuarioDto)
        {
            if (id != usuarioDto.IdEndereco || id != usuarioDto.Endereco.Id)
            {
                NotificateError("O Id fornecido não corresponde ao Id enviado no endereço. Por favor, verifique se o Id está correto e tente novamente.");
                return CustomResponse();
            };

            await _usuarioService.AtualizarEndereco(_mapper.Map<Endereco>(usuarioDto.Endereco));

            return CustomResponse();

        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Remover(Guid id)
        {
            if (await _usuarioRepository.ObterPorId(id) == null) return NotFound();

            await _usuarioService.Remover(id);

            return CustomResponse();
        }
    }
}

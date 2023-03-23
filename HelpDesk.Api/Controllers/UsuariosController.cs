using AutoMapper;
using HelpDesk.Api.DTOs;
using HelpDesk.Api.Extensions;
using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Interfaces.Services;
using HelpDesk.Business.Interfaces.Validators;
using HelpDesk.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace HelpDesk.Api.Controllers
{
    [Route("api/usuarios")]
    public class UsuariosController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AppSettings _appSettings;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public UsuariosController(IUsuarioRepository usuarioRepository,
                                  IUsuarioService usuarioService,
                                  IMapper mapper,
                                  INotificador notificador,
                                  SignInManager<IdentityUser> signInManager,
                                  IOptions<AppSettings> appsettings) : base(notificador)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioService = usuarioService;
            _mapper = mapper;
            _signInManager = signInManager;
            _appSettings = appsettings.Value;
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

            if (OperacaoValida())
            {
                return CustomResponse(GerarJwt());
            }

            return CustomResponse();

        }

        [AllowAnonymous]
        [HttpPost("entrar")]
        public async Task<ActionResult> Login(UsuarioDtoLogin usuarioDtoLogin)
        {
            var result = await _signInManager.PasswordSignInAsync(usuarioDtoLogin.Login, usuarioDtoLogin.Senha, isPersistent: false, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                return CustomResponse(GerarJwt());
            }
            if (result.IsLockedOut)
            {
                NotificateError("Usuário temporariamente bloqueado por excesso de tentativas inválidas");
                return CustomResponse(usuarioDtoLogin);
            }

            NotificateError("Usuario ou Senha incorretos");
            return CustomResponse(usuarioDtoLogin);
        }

        [HttpPut("atualizar/{id:guid}")]
        public async Task<ActionResult<UsuarioDto>> Atualizar(Guid id, UsuarioDto usuarioDto)
        {
            if (id != usuarioDto.Id)
            {
                NotificateError("O Id fornecido não corresponde ao Id enviado no usuário. Por favor, verifique se o Id está correto e tente novamente.");
                return CustomResponse();
            }
            if(_usuarioRepository.ObterPorId(usuarioDto.Id).Result == null)
            {
                NotificateError("O usuário não se encontra cadastrado! Verifique as informações e tente novamente");
                return CustomResponse();
            };

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

        private string GerarJwt()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);
            return encodedToken;
        }
    }
}

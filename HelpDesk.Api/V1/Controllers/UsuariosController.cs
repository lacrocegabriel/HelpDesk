using AutoMapper;
using HelpDesk.Api.Controllers;
using HelpDesk.Api.DTOs;
using HelpDesk.Api.Extensions;
using HelpDesk.Business.Interfaces.Others;
using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Interfaces.Services;
using HelpDesk.Business.Interfaces.Validators;
using HelpDesk.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static HelpDesk.Api.Extensions.CustomAuthorization;

namespace HelpDesk.Api.V1.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("helpdesk/v{version:apiVersion}/usuarios")]
    public class UsuariosController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public UsuariosController(IUsuarioRepository usuarioRepository,
                                  IUsuarioService usuarioService,
                                  IMapper mapper,
                                  INotificador notificador,
                                  SignInManager<IdentityUser> signInManager,
                                  UserManager<IdentityUser> userManager,
                                  IOptions<AppSettings> appsettings,
                                  IUser user) : base(notificador, user)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioService = usuarioService;
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appsettings.Value;
        }

        [ClaimsAuthorize("Chamados", "R")]
        [HttpGet("{skip:int}/{take:int}")]
        public async Task<IEnumerable<UsuarioDto>> ObterTodos([FromRoute] int skip = 0, int take = 25)
        {
            return _mapper.Map<IEnumerable<UsuarioDto>>(await _usuarioRepository.ObterTodos(skip, take));

        }

        [ClaimsAuthorize("Chamados", "R")]
        [HttpGet("{id:guid}")]
        public async Task<UsuarioDto> ObterPorId(Guid id)
        {
            return _mapper.Map<UsuarioDto>(await _usuarioRepository.ObterPorId(id));

        }

        [ClaimsAuthorize("Administrador", "A")]
        [HttpPost("registrar")]
        public async Task<ActionResult<UsuarioDto>> Registrar(UsuarioDto usuarioDto)
        {
            var user = new IdentityUser
            {
                UserName = usuarioDto.Login,
                Email = usuarioDto.Email,
                EmailConfirmed = true
            };

            usuarioDto.IdUsuarioAutenticacao = Guid.Parse(user.Id);

            _usuarioRepository.BeginTransaction();

            await _usuarioService.Adicionar(_mapper.Map<Usuario>(usuarioDto));

            if (!OperacaoValida())
            {
                _usuarioRepository.Rollback();
                return CustomResponse();
            }

            var result = await _userManager.CreateAsync(user, usuarioDto.Senha);

            if (!result.Succeeded)
            {
                _usuarioRepository.Rollback();

                foreach (var error in result.Errors)
                {
                    NotificateError(error.Description);
                }
                return CustomResponse();
            }

            _usuarioRepository.Commit();
            return CustomResponse(await GerarJwt(usuarioDto.Login));
        }

        [AllowAnonymous]
        [HttpPost("entrar")]
        public async Task<ActionResult> Login(UsuarioDtoLogin usuarioDtoLogin)
        {
            var result = await _signInManager.PasswordSignInAsync(usuarioDtoLogin.Login, usuarioDtoLogin.Senha, isPersistent: false, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                return CustomResponse(await GerarJwt(usuarioDtoLogin.Login));
            }
            if (result.IsLockedOut)
            {
                NotificateError("Usuário temporariamente bloqueado por excesso de tentativas inválidas");
                return CustomResponse(usuarioDtoLogin);
            }

            NotificateError("Usuario ou Senha incorretos");
            return CustomResponse(usuarioDtoLogin);
        }

        [ClaimsAuthorize("Chamados", "U")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<UsuarioDto>> Atualizar(Guid id, UsuarioDto usuarioDto)
        {
            if (id != usuarioDto.Id)
            {
                NotificateError("O Id fornecido não corresponde ao Id enviado no usuário. Por favor, verifique se o Id está correto e tente novamente.");
                return CustomResponse();
            }
            if (_usuarioRepository.ObterPorId(usuarioDto.Id).Result == null)
            {
                NotificateError("O usuário não se encontra cadastrado! Verifique as informações e tente novamente");
                return CustomResponse();
            };

            _usuarioRepository.BeginTransaction();

            await _usuarioService.Atualizar(_mapper.Map<Usuario>(usuarioDto));

            if (!OperacaoValida())
            {
                _usuarioRepository.Rollback();
                return CustomResponse();
            }

            var user = _userManager.FindByNameAsync(usuarioDto.Login).Result;

            if (usuarioDto.Email != user.Email)
            {
                user.Email = usuarioDto.Email;
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                _usuarioRepository.Rollback();

                foreach (var error in result.Errors)
                {
                    NotificateError(error.Description);
                }
                return CustomResponse();
            }

            _usuarioRepository.Commit();
            return CustomResponse(await GerarJwt(usuarioDto.Login));
        }

        [ClaimsAuthorize("Chamados", "U")]
        [HttpPut("endereco/{id:guid}")]
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

        private async Task<LoginResponseDto> GerarJwt(string login)
        {

            var user = await _userManager.FindByNameAsync(login);
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var role in userRoles)
            {
                claims.Add(new Claim("role", role));
            }

            var identityClaims = new ClaimsIdentity();

            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            var response = new LoginResponseDto
            {
                AcessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds,
                User = new UsuarioTokenDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = identityClaims.Claims.Select(c => new ClaimDto { Type = c.Type, Value = c.Value })
                }
            };

            return response;
        }

        private static long ToUnixEpochDate(DateTime date) => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}

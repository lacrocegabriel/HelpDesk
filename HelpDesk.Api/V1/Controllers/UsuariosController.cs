using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using HelpDesk.Application.Interface;
using HelpDesk.Domain.Entities;
using HelpDesk.Domain.Interfaces.Others;
using HelpDesk.Domain.Interfaces.Validators;
using HelpDesk.Services.Api.Controllers;
using HelpDesk.Services.Api.DTOs;
using HelpDesk.Services.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using static HelpDesk.Services.Api.Extensions.CustomAuthorization;

namespace HelpDesk.Services.Api.V1.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("helpdesk/v{version:apiVersion}/usuarios")]
    public class UsuariosController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;
        private readonly IUsuarioAppService _usuarioAppService;
        private readonly IMapper _mapper;
        
        public UsuariosController(IUsuarioAppService usuarioAppService,
                                  IMapper mapper,
                                  INotificador notificador,
                                  SignInManager<IdentityUser> signInManager,
                                  UserManager<IdentityUser> userManager,
                                  IOptions<AppSettings> appsettings,
                                  IUser user) : base(notificador, user)
        {
            _usuarioAppService = usuarioAppService;
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appsettings.Value;        
        }

        [ClaimsAuthorize("Usuarios", "R")]
        [HttpGet("{skip:int}/{take:int}")]
        public async Task<IEnumerable<UsuarioDto>> ObterTodos(int skip = 0, int take = 25)
        {
            return _mapper.Map<IEnumerable<UsuarioDto>>(await _usuarioAppService.ObterTodos(skip, take));

        }

        [ClaimsAuthorize("Usuarios", "R")]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UsuarioDto>> ObterPorId(Guid id)
        {
            var usuario = _mapper.Map<UsuarioDto>(await _usuarioAppService.ObterPorId(id));

            if (usuario == null)
            {
                NotificateError("O usuário não se encontra cadastrado ou o usuário logado não possui permissão para visualiza-lo! Verifique as informações e tente novamente");
                return CustomResponse();
            }
            return usuario;

        }

        [ClaimsAuthorize("Administrador", "A")]
        [HttpPost("registrar")]
        public async Task<ActionResult<UsuarioDto>> Registrar(UsuarioDto usuarioDto)
        {
            if (usuarioDto == null)
            {
                NotificateError("Não foi enviado um usuário válido. Verifique as informações e tente novamente!");

                return CustomResponse();
            }

            if (!ValidaClaimsPermitidas(usuarioDto.Permissoes))
            {
                return CustomResponse();
            }

            var user = new IdentityUser
            {
                Id = usuarioDto.Id.ToString(),
                UserName = usuarioDto.Documento,
                Email = usuarioDto.Email,
                EmailConfirmed = true
            };

            _usuarioAppService.BeginTransaction();

            await _usuarioAppService.Adicionar(_mapper.Map<Usuario>(usuarioDto));

            if (!OperacaoValida())
            {
                _usuarioAppService.Rollback();
                return CustomResponse();
            }

            var result = await _userManager.CreateAsync(user, usuarioDto.Senha);

            if (!result.Succeeded)
            {
                _usuarioAppService.Rollback();

                foreach (var error in result.Errors)
                {
                    NotificateError(error.Description);
                }
                return CustomResponse();
            }

            if (usuarioDto.Permissoes.Any())
            {
                await PermissionaUsuario(user, usuarioDto.Permissoes);
            }
            
            _usuarioAppService.Commit();
            return CustomResponse(await GerarJwt(usuarioDto.Documento));
        }

        [AllowAnonymous]
        [HttpPost("entrar")]
        public async Task<ActionResult> Login(UsuarioDtoLogin usuarioDtoLogin)
        {
            if (usuarioDtoLogin == null)
            {
                NotificateError("Não foi enviado um usuário válido. Verifique as informações e tente novamente!");

                return CustomResponse();
            }

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

        [ClaimsAuthorize("Usuarios", "U")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<UsuarioDto>> Atualizar(Guid id, UsuarioDto usuarioDto)
        {
            if(usuarioDto == null)
            {
                NotificateError("Não foi enviado um usuário válido. Verifique as informações e tente novamente!");

                return CustomResponse();
            }
            if (id != usuarioDto.Id)
            {
                NotificateError("O Id fornecido não corresponde ao Id enviado no usuário. Por favor, verifique se o Id está correto e tente novamente.");
                return CustomResponse();
            }
            if (_usuarioAppService.ObterPorId(usuarioDto.Id).Result == null)
            {
                NotificateError("O usuário não se encontra cadastrado ou o usuário não possui permissão para editá-lo! Verifique as informações e tente novamente");
                return CustomResponse();
            }
            if (!ValidaClaimsPermitidas(usuarioDto.Permissoes))
            {
                return CustomResponse();
            }

            _usuarioAppService.BeginTransaction();

            await _usuarioAppService.Atualizar(_mapper.Map<Usuario>(usuarioDto));

            if (!OperacaoValida())
            {
                _usuarioAppService.Rollback();
                return CustomResponse();
            }

            var user = _userManager.FindByNameAsync(usuarioDto.Documento).Result;

            if (usuarioDto.Email != user.Email)
            {
                user.Email = usuarioDto.Email;
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                _usuarioAppService.Rollback();

                foreach (var error in result.Errors)
                {
                    NotificateError(error.Description);
                }
                return CustomResponse();
            }


            await PermissionaUsuario(user, usuarioDto.Permissoes);

            _usuarioAppService.Commit();
            return CustomResponse(await GerarJwt(usuarioDto.Documento));
            
        }

        [ClaimsAuthorize("Usuarios", "U")]
        [HttpPut("endereco/{id:guid}")]
        public async Task<IActionResult> AtualizarEndereco(Guid id, UsuarioDto usuarioDto)
        {
            if (usuarioDto == null)
            {
                NotificateError("Não foi enviado um usuário válido. Verifique as informações e tente novamente!");

                return CustomResponse();
            }

            if (id != usuarioDto.IdEndereco || id != usuarioDto.Endereco.Id)
            {
                NotificateError("O Id fornecido não corresponde ao Id enviado no endereço. Por favor, verifique se o Id está correto e tente novamente.");
                return CustomResponse();
            };

            await _usuarioAppService.AtualizarEndereco(_mapper.Map<Endereco>(usuarioDto.Endereco));

            return CustomResponse();

        }
        
        private async Task PermissionaUsuario(IdentityUser user, IEnumerable<ClaimDto> claimDtos)
        {
            var claimsAtuais = _mapper.Map<List<ClaimDto>>(await _userManager.GetClaimsAsync(user));

            var claimsRemover = new List<ClaimDto>();

            var claimsAdicionar = claimDtos.Where(claim => (claimsAtuais.Where(x => x.Value == claim.Value && x.Type == claim.Type).FirstOrDefault() == null)).ToList();

            foreach (var claim in claimsAtuais)
           {
               var teste = claimDtos.Where(x => x.Value == claim.Value && x.Type == claim.Type).FirstOrDefault();

               if (claimDtos.Where(x => x.Value == claim.Value && x.Type == claim.Type).FirstOrDefault() == null)
               {
                   claimsRemover.Add(claim);
               }
           }
           
           if(claimsRemover.Any())
           {
               await _userManager.RemoveClaimsAsync(user, _mapper.Map<IEnumerable<Claim>>(claimsRemover));
           }

           if (claimsAdicionar.Any())
           {
               await _userManager.AddClaimsAsync(user, _mapper.Map<IEnumerable<Claim>>(claimsAdicionar));
           }

           return;          

        }
        private bool ValidaClaimsPermitidas(IEnumerable<ClaimDto> claims)
        {
            var claimsValuesPermitidos = new List<string>() { "C", "R", "U", "D", "A" };

            var claimsTypePermitidos = new List<string>() { "Chamados", "Tramites", "Usuarios", "Setores", "Clientes", "Gerenciadores"};

            string mensagem = @"As seguintes permissões não podem ser utilizadas, pois não fazem parte do controle de autenticação da aplicação! Verifique as informações e tente novamente! Permissões:";

            var claimsNaoPermitidas = new List<string>();

            int i = 1;
            
            foreach (var claim in claims)
            {
                if (!claimsTypePermitidos.Contains(claim.Type) || !claimsValuesPermitidos.Contains(claim.Value))
                {
                    claimsNaoPermitidas.Add(string.Format(" {0} - Tipo: {1} - Valor: {2}", i, claim.Type, claim.Value));
                    i++;                   
                }
            }

            if (claimsNaoPermitidas.Any())
            {
                foreach (var msg in claimsNaoPermitidas)
                {
                    mensagem += msg;
                }
                NotificateError(mensagem);

                return false;
            }

            return true;
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

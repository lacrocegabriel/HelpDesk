using AutoMapper;
using HelpDesk.Api.DTOs;
using HelpDesk.Business.Interfaces.Services;
using HelpDesk.Business.Interfaces.Validators;
using HelpDesk.Business.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers
{
    [Route("api")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public AuthController(INotificador notificador,
                              SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IUsuarioService usuarioService) : base(notificador)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _usuarioService = usuarioService;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult> Registrar(UsuarioDto usuario)
        {
            await _usuarioService.Adicionar(_mapper.Map<Usuario>(usuario));

            var user = new IdentityUser
            {
                UserName = usuario.Email,
                Email = usuario.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, usuario.Senha);

            if(result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return CustomResponse(usuario);
            }
            foreach(var error in result.Errors)
            {
                NotificateError(error.Description);
            }

            return CustomResponse(usuario);
        }

        [HttpPost("entrar")]
        public async Task<ActionResult> Login(UsuarioDtoLogin usuarioDtoLogin)
        {
            var result = await _signInManager.PasswordSignInAsync(usuarioDtoLogin.Login, usuarioDtoLogin.Senha, isPersistent: false,lockoutOnFailure: true);

            if(result.Succeeded)
            {
                return CustomResponse(usuarioDtoLogin);
            }
            if(result.IsLockedOut)
            {
                NotificateError("Usuário temporariamente bloqueado por excesso de tentativas inválidas");
                return CustomResponse(usuarioDtoLogin);
            }

            NotificateError("Usuario ou Senha incorretos");
            return CustomResponse(usuarioDtoLogin);
        }






    }
}

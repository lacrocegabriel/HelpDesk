using AutoMapper;
using HelpDesk.Api.Controllers;
using HelpDesk.Api.DTOs;
using HelpDesk.Business.Interfaces.Others;
using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Interfaces.Services;
using HelpDesk.Business.Interfaces.Validators;
using HelpDesk.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static HelpDesk.Api.Extensions.CustomAuthorization;

namespace HelpDesk.Api.V1.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("helpdesk/v{version:apiVersion}/clientes")]
    public class ClientesController : MainController
    {
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;

        public ClientesController(IClienteRepository clienteRepository,
                                 IClienteService clienteService,
                                 IMapper mapper,
                                 INotificador notificador,
                                  IUser user) : base(notificador, user)
        {
            _clienteService = clienteService;
            _mapper = mapper;

        }

        [ClaimsAuthorize("Clientes", "R")]
        [HttpGet("{skip:int}/{take:int}")]
        public async Task<IEnumerable<ClienteDto>> ObterTodos(int skip = 0, int take = 25)
        {
            return _mapper.Map<IEnumerable<ClienteDto>>(await _clienteService.ObterTodos(skip, take));

        }

        [ClaimsAuthorize("Clientes", "R")]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ClienteDto>> ObterPorId(Guid id)
        {
            var cliente = _mapper.Map<ClienteDto>(await _clienteService.ObterPorId(id));

            if (cliente == null)
            {
                return CustomResponse();
            }
            return cliente;

        }

        [ClaimsAuthorize("Clientes", "C")]
        [HttpPost]
        public async Task<ActionResult<ClienteDto>> Adicionar(ClienteDto clienteDto)
        {
            await _clienteService.Adicionar(_mapper.Map<Cliente>(clienteDto));

            return CustomResponse();

        }

        [ClaimsAuthorize("Clientes", "U")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ClienteDto>> Atualizar(Guid id, ClienteDto clienteDto)
        {
            if (id != clienteDto.Id)
            {
                NotificateError("O Id fornecido não corresponde ao Id enviado no cliente. Por favor, verifique se o Id está correto e tente novamente.");
                return CustomResponse();
            };
            if (await _clienteService.ObterPorId(clienteDto.Id) == null)
            {
                NotificateError("O cliente não se encontra cadastrado ou o usuário não possui permissão para editá-lo! Verifique as informações e tente novamente");
                return CustomResponse();
            };

            await _clienteService.Atualizar(_mapper.Map<Cliente>(clienteDto));

            return CustomResponse();

        }

        [ClaimsAuthorize("Clientes", "U")]
        [HttpPut("endereco/{id:guid}")]
        public async Task<IActionResult> AtualizarEndereco(Guid id, ClienteDto clienteDto)
        {
            if (id != clienteDto.IdEndereco || id != clienteDto.Endereco.Id)
            {
                NotificateError("O Id fornecido não corresponde ao Id enviado no endereço. Por favor, verifique se o Id está correto e tente novamente.");
                return CustomResponse();
            };

            await _clienteService.AtualizarEndereco(_mapper.Map<Endereco>(clienteDto.Endereco));

            return CustomResponse();

        }

        [ClaimsAuthorize("Clientes", "D")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Remover(Guid id)
        {
            if (await _clienteService.ObterPorId(id) == null) return NotFound();

            await _clienteService.Remover(id);

            return CustomResponse();
        }
    }
}

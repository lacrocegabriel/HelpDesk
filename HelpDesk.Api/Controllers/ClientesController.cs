using AutoMapper;
using HelpDesk.Api.DTOs;
using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Interfaces.Services;
using HelpDesk.Business.Interfaces.Validators;
using HelpDesk.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers
{
    [Route("api/clientes")]
    public class ClientesController : MainController
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;

        public ClientesController(IClienteRepository clienteRepository,
                                 IClienteService clienteService,
                                 IMapper mapper,
                                 INotificador notificador) : base(notificador)
        {
            _clienteRepository = clienteRepository;
            _clienteService = clienteService;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IEnumerable<ClienteDto>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<ClienteDto>>(await _clienteRepository.ObterTodos());

        }

        [HttpGet("{id:guid}")]
        public async Task<ClienteDto> ObterPorId(Guid id)
        {
            return _mapper.Map<ClienteDto>(await _clienteRepository.ObterPorId(id));

        }

        [HttpPost]
        public async Task<ActionResult<ClienteDto>> Adicionar(ClienteDto clienteDto)
        {
            await _clienteService.Adicionar(_mapper.Map<Cliente>(clienteDto));

            return CustomResponse();

        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ClienteDto>> Atualizar(Guid id, ClienteDto clienteDto)
        {
            if (id != clienteDto.Id) return BadRequest();

            await _clienteService.Atualizar(_mapper.Map<Cliente>(clienteDto));

            return CustomResponse();

        }

        [HttpPut("atualizar-endereco/{id:guid}")]
        public async Task<IActionResult> AtualizarEndereco(Guid id, ClienteDto clienteDto)
        {
            if (id != clienteDto.IdEndereco || id != clienteDto.Endereco.Id) return BadRequest();

            await _clienteService.AtualizarEndereco(_mapper.Map<Endereco>(clienteDto.Endereco));

            return CustomResponse();

        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Remover(Guid id)
        {
            if (await _clienteRepository.ObterPorId(id) == null) return NotFound();

            await _clienteService.Remover(id);

            return CustomResponse();
        }
    }
}

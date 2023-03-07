using AutoMapper;
using HelpDesk.Api.DTOs;
using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Interfaces.Services;
using HelpDesk.Business.Models;
using HelpDesk.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers
{
    [Route("api/gerenciadores")]
    public class GerenciadoresController : ControllerBase
    {
        private readonly IGerenciadorRepository _gerenciadorRepository;
        private readonly IGerenciadorService _gerenciadorService;
        private readonly IMapper _mapper;

        public GerenciadoresController(IGerenciadorRepository gerenciadorRepository,
                                       IGerenciadorService gerenciadorService,
                                       IMapper mapper)
        {
            _gerenciadorRepository = gerenciadorRepository;
            _gerenciadorService = gerenciadorService;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IEnumerable<GerenciadorDto>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<GerenciadorDto>>(await _gerenciadorRepository.ObterTodos());

        }

        [HttpGet("{id:guid}")]
        public async Task<GerenciadorDto> ObterPorId(Guid id)
        {
            return _mapper.Map<GerenciadorDto>(await _gerenciadorRepository.ObterPorId(id));

        }

        [HttpPost]
        public async Task<ActionResult<GerenciadorDto>> Adicionar([FromBody] GerenciadorDto gerenciadorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _gerenciadorService.Adicionar(_mapper.Map<Gerenciador>(gerenciadorDto));

            return Ok(gerenciadorDto);

        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<GerenciadorDto>> Atualizar(Guid id, [FromBody] GerenciadorDto gerenciadorDto)
        {
            if(id != gerenciadorDto.Id) return BadRequest();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _gerenciadorService.Atualizar(_mapper.Map<Gerenciador>(gerenciadorDto));

            return Ok(gerenciadorDto);

        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Remover(Guid id)
        {
            if (await _gerenciadorRepository.ObterPorId(id) == null) return NotFound();

            await _gerenciadorService.Remover(id);

            return Ok();
        }
    }
}



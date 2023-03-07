using AutoMapper;
using HelpDesk.Api.DTOs;
using HelpDesk.Business.Interfaces.Repositories;
using HelpDesk.Business.Interfaces.Services;
using HelpDesk.Business.Models;
using HelpDesk.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers
{
    [Route("api/chamados")]
    public class ChamadosController : ControllerBase
    {
        private readonly IChamadoRepository _chamadoRepository;
        private readonly IChamadoService _chamadoService;
        private readonly IMapper _mapper;

        public ChamadosController(IChamadoRepository chamadoRepository,
                                       IChamadoService chamadoService,
                                       IMapper mapper)
        {
            _chamadoRepository = chamadoRepository;
            _chamadoService = chamadoService;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IEnumerable<ChamadoDto>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<ChamadoDto>>(await _chamadoRepository.ObterTodos());

        }

        [HttpPost]
        public async Task<ActionResult<ChamadoDto>> Adicionar([FromBody] ChamadoDto chamdoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _chamadoService.Adicionar(_mapper.Map<Chamado>(chamdoDto));

            return Ok(chamdoDto);

        }
    }
}



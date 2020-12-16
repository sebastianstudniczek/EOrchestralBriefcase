using AutoMapper;
using EOrchestralBriefcase.Application.Dtos;
using EOrchestralBriefcase.Application.Interfaces;
using EOrchestralBriefcase.Application.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EOrchestralBriefcase.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [ApiVersion("1")]
    [Produces("application/json")]
    public class OrchestralBriefcasesController : ControllerBase
    {
        private readonly IOrchestralBriefcasesService _orchBriefcaseService;
        private readonly IMapper _mapper;

        public OrchestralBriefcasesController(IOrchestralBriefcasesService service, IMapper mapper)
        {
            _orchBriefcaseService = service;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<OrchestralBriefcasesVm>> GetAll()
        {
            var orchPiecesDto = await _orchBriefcaseService.GetAllAsync();
            List<OrchestralBriefcaseVm> orchBriefcaseVms =
                _mapper.Map<List<OrchestralBriefcaseDto>, List<OrchestralBriefcaseVm>>(orchPiecesDto);

            var orchBriefcasesVm = new OrchestralBriefcasesVm
            {
                OrchestralBriefcases = orchBriefcaseVms,
                Count = orchBriefcaseVms.Count
            };
            Response.Headers["x-total-count"] = orchBriefcasesVm.Count.ToString();

            return Ok(orchBriefcasesVm);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrchestralBriefcaseVm>> GetById(int id)
        {
            var orchPieceDto = await _orchBriefcaseService.GetByIdAsync(id);

            if (orchPieceDto == null)
            {
                return NotFound();
            }
            OrchestralBriefcaseVm orchBriefcaseVm =
                _mapper.Map<OrchestralBriefcaseDto, OrchestralBriefcaseVm>(orchPieceDto);

            return Ok(orchPieceDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] OrchestralBriefcaseVm vm)
        {
            int id;
            var orchBriefcaseDto = _mapper.Map<OrchestralBriefcaseVm,OrchestralBriefcaseDto>(vm);

            try
            {
                id = await _orchBriefcaseService.InsertAsync(orchBriefcaseDto);
                orchBriefcaseDto.Id = id;

                return new CreatedResult($"/orchestralbriefcases/{orchBriefcaseDto.Name}", orchBriefcaseDto);
            }
            catch (Exception ex)
            {
                return ValidationProblem(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] OrchestralBriefcaseVm orchBriefcaseVm)
        {
            if (id != orchBriefcaseVm.Id)
            {
                return BadRequest();
            }
            try
            {
                var orchBriefcaseDto = await _orchBriefcaseService.GetByIdAsync(orchBriefcaseVm.Id);

                if (orchBriefcaseDto == null)
                {
                    return NotFound();
                }

                orchBriefcaseDto = _mapper.Map<OrchestralBriefcaseDto>(orchBriefcaseVm);
                await _orchBriefcaseService.UpdateAsync(orchBriefcaseDto);

                return Ok(orchBriefcaseDto);
            }
            catch (Exception ex)
            {
                return ValidationProblem(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteById(int id)
        {
            await _orchBriefcaseService.DeleteByIdAsync(id);

            return NoContent();
        }
    }
}

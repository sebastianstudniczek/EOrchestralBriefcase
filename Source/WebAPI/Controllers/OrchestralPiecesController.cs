using AutoMapper;
using EOrchestralBriefcase.Application.Dtos;
using EOrchestralBriefcase.Application.Interfaces;
using EOrchestralBriefcase.Application.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EOrchestralBriefcase.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [ApiVersion("1")]
    [Produces("application/json")]
    public class OrchestralPiecesController : ControllerBase
    {
        private readonly IOrchestralPiecesService _orchPieceService;
        private readonly IMapper _mapper;

        public OrchestralPiecesController(IOrchestralPiecesService orchPieceService, IMapper mapper)
        {
            _orchPieceService = orchPieceService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrchestralPieceVm>> GetById(int id, [FromQuery] int orchBriefcaseId)
        {
            var orchPieceDto = await _orchPieceService.GetByIdAsync(id);

            if (orchPieceDto == null)
            {
                return NotFound();
            }
            var orchPieceVm = _mapper.Map<OrchestralPieceVm>(orchPieceDto);

            return Ok(orchPieceVm);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody]OrchestralPieceVm vm)
        {
            int id;
            var orchPieceDto = _mapper.Map<OrchestralPieceDto>(vm);

            try
            {
                id = await _orchPieceService.InsertAsync(orchPieceDto);
                orchPieceDto.Id = id;

                return new CreatedResult($"/orchestralpieces/{orchPieceDto.Title}",orchPieceDto);
            }
            catch (Exception ex)
            {
                return ValidationProblem(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] OrchestralPieceVm orchPieceVm)
        {
            if (id != orchPieceVm.Id)
            {
                return BadRequest();
            }

            try
            {
                var orchPieceDto = await _orchPieceService.GetByIdAsync(id);

                if (orchPieceDto == null)
                {
                    return NotFound();
                }

                orchPieceDto = _mapper.Map<OrchestralPieceDto>(orchPieceVm);
                orchPieceDto.Id = id;
                await _orchPieceService.UpdateAsync(orchPieceDto);

                return Ok(orchPieceDto);
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
            await _orchPieceService.DeleteAsync(id);

            return NoContent();
        }
    }
}

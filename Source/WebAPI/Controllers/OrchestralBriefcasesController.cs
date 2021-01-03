using System.Collections.Generic;
using System.Threading.Tasks;
using EOrchestralBriefcase.Application.Dtos.OrchestralBriefcases;
using EOrchestralBriefcase.Application.Dtos.OrchestralPieces;
using EOrchestralBriefcase.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EOrchestralBriefcase.WebAPI.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class OrchestralBriefcasesController : ControllerBase
    {
        private readonly IOrchestralBriefcasesService _orchestralBriefcaseService;
        private readonly IOrchestralPiecesService _orchestralPieceService;

        public OrchestralBriefcasesController(
            IOrchestralBriefcasesService orchestralBriefcaseService,
            IOrchestralPiecesService orchestralPieceService)
        {
            _orchestralBriefcaseService = orchestralBriefcaseService;
            _orchestralPieceService = orchestralPieceService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<OrchestralBriefcaseReadDto>>> GetAll()
        {
            var orchestralBriefcasesReadDtos = await _orchestralBriefcaseService.GetAllAsync();
            Response.Headers["x-total-count"] = orchestralBriefcasesReadDtos.Count.ToString();

            return Ok(orchestralBriefcasesReadDtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrchestralBriefcaseReadDto>> GetById(int id)
        {
            var dto = await _orchestralBriefcaseService.GetByIdAsync(id);

            return Ok(dto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> Create(OrchestralBriefcaseCreateDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            int id = await _orchestralBriefcaseService.CreateAsync(createDto);

            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, OrchestralBriefcaseUpdateDto updateDto)
        {
            if (id != updateDto.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            await _orchestralBriefcaseService.UpdateAsync(updateDto);

            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteById(int id)
        {
            await _orchestralBriefcaseService.DeleteByIdAsync(id);

            return NoContent();
        }

        [HttpGet("{id}/orchestralpieces")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<OrchestralPieceReadDto>>> GetOrchestralPiecesForBriefcase(int id)
        {
            var orchestralPieces = await _orchestralPieceService
                .GetAllForOrchestralBriefcaseAsync(id);

            return Ok(orchestralPieces);
        }
    }
}

using Applications.DTOs.EntranceExam;
using Applications.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers.ResultExtension;

namespace Presentation.Controllers;

[ApiController]
[Route("api/entranceExams")]
public class EntranceExamsController(IEntranceExamService service) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<EntranceExamResponse>>> GetList()
    {
        var response = await service.GetListAsync();
        return !response.IsSuccess ? response.HandleResult() : Ok(response.Value);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<EntranceExamResponse>> GetById(int id)
    {
        var response = await service.GetByIdAsync(id);
        return !response.IsSuccess ? response.HandleResult() : Ok(response.Value);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<EntranceExamResponse>> Create(EntranceExamRequest? request)
    {
        var response = await service.AddAsync(request);
        if (!response.IsSuccess)
            return response.HandleResult();

        return CreatedAtAction(nameof(GetById), new { id = response.Value.Id }, response.Value);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(int id)
    {
        var response = await service.DeleteAsync(id);
        return !response.IsSuccess ? response.HandleResult() : NoContent();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Update(int id, EntranceExamRequest? request)
    {
        var response = await service.UpdateAsync(id, request);
        return !response.IsSuccess ? response.HandleResult() : NoContent();
    }

    [HttpPatch("{id}/score")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateScore(int id, UpdateScoreRequest? request)
    {
        var response = await service.UpdateStudentScoreAsync(id, request);
        return !response.IsSuccess ? response.HandleResult() : NoContent();
    }
    
    [HttpPatch("{id}/criteria")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateScoreCriteria(int id, UpdateScoreCriteriaRequest? request)
    {
        var response = await service.UpdateScoreCriteriaAsync(id, request);
        return !response.IsSuccess ? response.HandleResult() : NoContent();
    }
    
    [HttpPatch("{id}/cancel")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CancelExam(int id, string? notes)
    {
        var response = await service.CancelExamAsync(id, notes);
        return !response.IsSuccess ? response.HandleResult() : NoContent();
    }
}
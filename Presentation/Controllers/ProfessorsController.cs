using Applications.DTOs.Professor;
using Applications.DTOs.Student;
using Applications.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers.ResultExtension;

namespace Presentation.Controllers;

[ApiController]
[Route("api/professors")]
public class ProfessorsController(IProfessorService service) : ControllerBase
{
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<ProfessorResponse>>> GetList()
    {
        var response = await service.GetListAsync();
        return !response.IsSuccess ? response.HandleResult() : Ok(response.Value);
    }



    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ProfessorResponse>> GetById(int id)
    {
        var response = await service.GetByIdAsync(id);
        return !response.IsSuccess ? response.HandleResult() : Ok(response.Value);
    }



    [HttpGet("by-number/{number}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ProfessorResponse>> GetByStudentNumber(string number)
    {
        var response = await service.GetByEmployeeNumberAsync(number);
        return !response.IsSuccess ? response.HandleResult() : Ok(response.Value);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ProfessorResponse>> Create(ProfessorRequest? request)
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


    [HttpDelete("by-number/{number}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(string number)
    {
        var response = await service.DeleteAsync(number);
        return !response.IsSuccess ? response.HandleResult() : NoContent();
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Update(int id, ProfessorRequest? request)
    {
        var response = await service.UpdateAsync(id, request);
        return !response.IsSuccess ? response.HandleResult() : NoContent();
    }
}
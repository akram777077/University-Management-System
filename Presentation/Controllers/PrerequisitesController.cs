using Applications.DTOs.Prerequisite;
using Applications.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers.ResultExtension;

namespace Presentation.Controllers;

[ApiController]
[Route("api/courses/prerequisites")]
public class PrerequisitesController(IPrerequisiteService service) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<PrerequisiteResponse>>> GetByCourseId(int courseId)
    {
        var result = await service.GetByCourseIdAsync(courseId);
        return !result.IsSuccess ? result.HandleResult() : Ok(result.Value);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PrerequisiteResponse>> Add(int courseId, PrerequisiteRequest request)
    {
        var response = await service.AddAsync(request);
        
        if (!response.IsSuccess)
            response.HandleResult();
        
        return CreatedAtAction(nameof(GetByCourseId), new { courseId }, response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Update(int id, PrerequisiteRequest request)
    {
        var result = await service.UpdateAsync(id, request);
        return !result.IsSuccess ? result.HandleResult() : NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await service.DeleteAsync(id);
        return !result.IsSuccess ? result.HandleResult() : NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteAllForCourse(int courseId)
    {
        var result = await service.DeleteForCourseAsync(courseId);
        return !result.IsSuccess ? result.HandleResult() : NoContent();
    }
}
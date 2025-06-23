using Applications.DTOs.ServiceApplication;
using Applications.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers.ResultExtension;

namespace Presentation.Controllers;

[ApiController]
[Route("api/serviceApplications")]
public class ServiceApplicationsController(IServiceApplicationService service) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<ServiceApplicationResponse>>> GetList()
    {
        var response = await service.GetListAsync();
        return !response.IsSuccess ? response.HandleResult() : Ok(response.Value);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ServiceApplicationResponse>> GetById(int id)
    {
        var response = await service.GetByIdAsync(id);
        return !response.IsSuccess ? response.HandleResult() : Ok(response.Value);
    }

    [HttpGet("by-person/{personId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ServiceApplicationResponse>> GetByPerson(int personId)
    {
        var response = await service.GetByPersonIdAsync(personId);
        return !response.IsSuccess ? response.HandleResult() : Ok(response.Value);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ServiceApplicationResponse>> Create(ServiceApplicationCreateRequest request)
    {
        var response = await service.AddAsync(request);
        
        if (!response.IsSuccess)
            return response.HandleResult();

        return CreatedAtAction(nameof(GetById), new { id = response.Value.Id }, response.Value);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Update(int id, ServiceApplicationUpdateRequest request)
    {
        var response = await service.UpdateAsync(id, request);
        return !response.IsSuccess ? response.HandleResult() : NoContent();
    }

    [HttpPatch("{id}/status")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateStatus(int id, ServiceApplicationUpdateStatusRequest? request)
    {
        var response = await service.UpdateStatusAsync(id, request);
        return !response.IsSuccess ? response.HandleResult() : NoContent();
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
}
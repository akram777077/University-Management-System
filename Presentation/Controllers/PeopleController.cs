using Applications.DTOs.People;
using Applications.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers.ResultExtension;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/people")]
    public class PeopleController : ControllerBase
    {
        private readonly IPersonService _service;

        public PeopleController(IPersonService service)
        {
            _service = service;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PersonResponse>>> GetList()
        {
            var response = await _service.GetListAsync();

            if (response.IsSuccess)
                return Ok(response.Value);

            return response.HandleResult();
        }



        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PersonResponse>> GetById(int id)
        {
            var response = await _service.GetByIdAsync(id);

            if (response.IsSuccess)
                return Ok(response.Value);

            return response.HandleResult();
        }


        [HttpGet("by-name/{lastName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PersonResponse>> GetByName(string lastName)
        {
            var response = await _service.GetByNameAsync(lastName);

            if (response.IsSuccess)
                return Ok(response.Value);

            return response.HandleResult();
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PersonResponse>> Create(PersonRequest? request)
        {
            var response = await _service.AddAsync(request);

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
            var response = await _service.DeleteAsync(id);

            if (response.IsSuccess)
                return NoContent();

            return response.HandleResult();
        }


        [HttpDelete("by-name/{lastName}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(string lastName)
        {
            var response = await _service.DeleteAsync(lastName);

            if (response.IsSuccess)
                return NoContent();

            return response.HandleResult();
        }



        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(int id, PersonRequest? request)
        {
            var result = await _service.UpdateAsync(id, request);

            if (result.IsSuccess)
                return NoContent();

            return result.HandleResult();
        }
    }
}

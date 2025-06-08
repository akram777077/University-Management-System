using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers.ResultExtension;
using Applications.Interfaces.Services;
using Applications.DTOs.Student;

namespace Presentation.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentsController(IStudentService service)
        {
            _service = service;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<StudentRequest>>> GetList()
        {
            var result = await _service.GetListAsync();

            if(!result.IsSuccess)
                return result.HandleResult();

            return Ok(result.Value);
        }



        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentResponse>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (!result.IsSuccess)
                return result.HandleResult();

            return Ok(result.Value);
        }



        [HttpGet("by-number/{number}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentResponse>> GetByStudentNumber(string number)
        {
            var result = await _service.GetByStudentNumberAsync(number);

            if (!result.IsSuccess)
                return result.HandleResult();

            return Ok(result.Value);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentResponse>> Create(StudentRequest? request)
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

            if (!response.IsSuccess)
                return response.HandleResult();

            return NoContent();
        }


        [HttpDelete("by-number/{number}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(string number)
        {
            var response = await _service.DeleteAsync(number);

            if (!response.IsSuccess)
                return response.HandleResult();

            return NoContent();
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(int id, StudentRequest? request)
        {
            var response = await _service.UpdateAsync(id, request);

            if (!response.IsSuccess)
                return response.HandleResult();

            return NoContent();
        }

        [HttpPatch("{id}/status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Patch(int id, UpdateStudentStatusRequest? status)
        {
            var response = await _service.UpdateStudentStatusAsync(id, status);

            if (!response.IsSuccess)
                return response.HandleResult();

            return NoContent();
        }
    }   
}

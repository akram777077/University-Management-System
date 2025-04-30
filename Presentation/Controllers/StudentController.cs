using Applications.DTOs;
using Applications.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentController(IStudentService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetAll()
        {
            var result = await _service.GetAllAsync();

            if(result.IsSuccess)
                return Ok(result.Value);
            
            return NotFound(result.Error);
        }



        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDto>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result.IsSuccess)
                return Ok(result.Value);

            return NotFound(result.Error);
        }


        [HttpGet("byname/{lastName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDto>> GetByName(string lastName)
        {
            var result = await _service.GetByNameAsync(lastName);

            if (result.IsSuccess)
                return Ok(result.Value);

            return NotFound(result.Error);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDto>> Add(StudentDto student)
        {
            var result = await _service.AddAsync(student);

            if (result.IsSuccess)
                return CreatedAtRoute(nameof(GetById), new { id = result.Value }, student);

            return BadRequest(result.Error);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDto>> DeleteById(int id)
        {
            var result = await _service.DeleteAsync(id);

            if (result.IsSuccess)
                return Ok("Student Deleted Successfully");

            return NotFound(result.Error);
        }


        [HttpDelete("byname/{lastName}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDto>> DeleteByLastName(string lastName)
        {
            var result = await _service.DeleteAsync(lastName);

            if (result.IsSuccess)
                return NoContent();

            return NotFound(result.Error);
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDto>> Update(int id, StudentDto student)
        {
            if (id != student.Id)
                return BadRequest("ID in the URL must match ID in the request body");

            var result = await _service.UpdateAsync(student);

            if (result.IsSuccess)
                return NoContent();

            return NotFound(result.Error);
        }
    }   
}

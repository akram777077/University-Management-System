using Applications.DTOs.Users;
using Applications.Helpers;
using Applications.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers.ResultExtension;

namespace Presentation.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetList()
        {
            var result = await _service.GetListAsync();

            if (!result.IsSuccess)
                return result.HandleResult();

            return Ok(result.Value);
        }



        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserResponse>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (!result.IsSuccess)
                return result.HandleResult();

            return Ok(result.Value);
        }



        [HttpGet("by-username/{username}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserResponse>> GetByUsername(string username)
        {
            var result = await _service.GetByUsernameAsync(username);

            if (!result.IsSuccess)
                return result.HandleResult();

            return Ok(result.Value);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserResponse>> Create(CreateUserRequest? request)
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


        [HttpDelete("by-username/{username}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(string username)
        {
            var response = await _service.DeleteAsync(username);

            if (!response.IsSuccess)
                return response.HandleResult();

            return NoContent();
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(int id, UpdateUserRequest? request)
        {
            var response = await _service.UpdateAsync(id, request);

            if (!response.IsSuccess)
                return response.HandleResult();

            return NoContent();
        }


        [HttpPatch("{id}/role")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateUserRole(int id, UpdateUserRoleRequest? request)
        {
            var response = await _service.UpdateUserRoleAsync(id, request);

            if (!response.IsSuccess)
                return response.HandleResult();

            return NoContent();
        }

        [HttpPatch("{id}/password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ChangePassword(int id, ChangePasswordRequest? request)
        {
            var response = await _service.ChangePasswordAsync(id, request);

            if (!response.IsSuccess)
                return response.HandleResult();

            return NoContent();
        }
    }
}

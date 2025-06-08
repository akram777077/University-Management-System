using Applications.DTOs;
using Applications.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers.ResultExtension;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/countries")]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _service;

        public CountriesController(ICountryService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CountryResponse>>> GetListAsync()
        {
            var result = await _service.GetListAsync();

            if (result.IsSuccess)
                return Ok(result.Value);

            return result.HandleResult();
        }



        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CountryResponse>> GetByIdAsync(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result.IsSuccess)
                return Ok(result.Value);

            return result.HandleResult();
        }


        [HttpGet("by-name/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CountryResponse>> GetByNameAsync(string name)
        {
            var result = await _service.GetByNameAsync(name);

            if (result.IsSuccess)
                return Ok(result.Value);

            return result.HandleResult();
        }


        [HttpGet("by-code/{code}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CountryResponse>> GetByCodeAsync(string code)
        {
            var result = await _service.GetByCodeAsync(code);

            if (result.IsSuccess)
                return Ok(result.Value);

            return result.HandleResult();
        }
    }
}

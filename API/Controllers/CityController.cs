using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controller for managing City-related operations.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CityController"/> class.
        /// </summary>
        /// <param name="cityService">The service for handling city operations.</param>
        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        /// <summary>
        /// Creates a new City.
        /// </summary>
        /// <param name="dto">The data transfer object containing City details.</param>
        /// <returns>The created City's ID.</returns>
        [HttpPost("Create")]
        [ProducesResponseType(typeof(int), 201)] // Created
        [ProducesResponseType(typeof(string), 400)] // Bad Request
        public async Task<IActionResult> CreateCity([FromBody] CityCreateDto dto)
        {
            try
            {
                var id = await _cityService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetCityById), new { id }, id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves a City by its ID.
        /// </summary>
        /// <param name="id">The ID of the city to retrieve.</param>
        /// <returns>city details.</returns>
        [HttpGet("GetById")]
        [ProducesResponseType(typeof(CityDto), 200)] // OK
        [ProducesResponseType(404)] // Not Found
        [ProducesResponseType(typeof(string), 400)] // Bad Request
        public async Task<IActionResult> GetCityById(int id)
        {
            try
            {
                var result = await _cityService.GetByIdAsync(id);
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary
        /// Retrieves a paginated list of all cities
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve (default is 1).</param>
        /// <param name="pageSize">The number of items per page (default is 10).</param>
        /// <returns>A paginated list of cities</returns>
        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(IEnumerable<CityDto>), 200)] // OK
        [ProducesResponseType(typeof(string), 400)] // Bad Request
        public async Task<IActionResult> GetAllCities(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var result = await _cityService.GetAllAsync(pageNumber, pageSize, null, null, false);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing City.
        /// </summary>
        /// <param name="id">The ID of the City to update.</param>
        /// <param name="dto">The data transfer object containing updated City details.</param>
        /// <returns>No content if the update is successful.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)] // No Content
        [ProducesResponseType(typeof(string), 400)] // Bad Request
        public async Task<IActionResult> UpdateCity(int id, [FromBody] CityDto dto)
        {
            try
            {
                if (id != dto.Id)
                    return BadRequest("ID mismatch.");

                await _cityService.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a City by its ID.
        /// </summary>
        /// <param name="id">The ID of the City to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)] // No Content
        [ProducesResponseType(typeof(string), 400)] // Bad Request
        public async Task<IActionResult> DeleteCity(int id)
        {
            try
            {
                var result = await _cityService.GetByIdAsync(id);
                if (result == null)
                    return NotFound("Entity wasn't found");

                await _cityService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

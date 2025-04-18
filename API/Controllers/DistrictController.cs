using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controller for managing District-related operations.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        private readonly IDistrictService _DistrictService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DistrictController"/> class.
        /// </summary>
        /// <param name="DistrictService">The service for handling District operations.</param>
        public DistrictController(IDistrictService DistrictService)
        {
            _DistrictService = DistrictService;
        }

        /// <summary>
        /// Creates a new District.
        /// </summary>
        /// <param name="dto">The data transfer object containing District details.</param>
        /// <returns>The created District's ID.</returns>
        [HttpPost("Create")]
        [ProducesResponseType(typeof(int), 201)] // Created
        [ProducesResponseType(typeof(string), 400)] // Bad Request
        public async Task<IActionResult> CreateDistrict([FromBody] DistrictCreateDto dto)
        {
            try
            {
                var id = await _DistrictService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetDistrictById), new { id = id }, id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves a District by its ID.
        /// </summary>
        /// <param name="id">The ID of the District to retrieve.</param>
        /// <returns>District details.</returns>
        [HttpGet("GetById")]
        [ProducesResponseType(typeof(DistrictDto), 200)] // OK
        [ProducesResponseType(404)] // Not Found
        [ProducesResponseType(typeof(string), 400)] // Bad Request
        public async Task<IActionResult> GetDistrictById(int id)
        {
            try
            {
                var result = await _DistrictService.GetByIdAsync(id);
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
        [ProducesResponseType(typeof(IEnumerable<DistrictDto>), 200)] // OK
        [ProducesResponseType(typeof(string), 400)] // Bad Request
        public async Task<IActionResult> GetAllCities(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var result = await _DistrictService.GetAllAsync(pageNumber, pageSize, null, null, false);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing District.
        /// </summary>
        /// <param name="id">The ID of the District to update.</param>
        /// <param name="dto">The data transfer object containing updated District details.</param>
        /// <returns>No content if the update is successful.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)] // No Content
        [ProducesResponseType(typeof(string), 400)] // Bad Request
        public async Task<IActionResult> UpdateDistrict(int id, [FromBody] DistrictDto dto)
        {
            try
            {
                if (id != dto.Id)
                    return BadRequest("ID mismatch.");

                await _DistrictService.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a District by its ID.
        /// </summary>
        /// <param name="id">The ID of the District to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)] // No Content
        [ProducesResponseType(typeof(string), 400)] // Bad Request
        public async Task<IActionResult> DeleteDistrict(int id)
        {
            try
            {
                var result = await _DistrictService.GetByIdAsync(id);
                if (result == null)
                    return NotFound("Entity wasn't found");

                await _DistrictService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

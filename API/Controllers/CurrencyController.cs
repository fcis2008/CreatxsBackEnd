using Application.DTOs;
using Application.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controller for managing currency-related operations.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyController"/> class.
        /// </summary>
        /// <param name="currencyService">The service for handling currency operations.</param>
        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        /// <summary>
        /// Creates a new currency.
        /// </summary>
        /// <param name="dto">The data transfer object containing currency details.</param>
        /// <returns>The created currency's ID.</returns>
        [HttpPost("Create")]
        [ProducesResponseType(typeof(int), 201)] // Created
        [ProducesResponseType(typeof(string), 400)] // Bad Request
        public async Task<IActionResult> CreateCurrency([FromBody] CurrencyCreateDto dto)
        {
            try
            {
                var id = await _currencyService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetCurrencyById), new { id = id }, id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves a currency by its ID.
        /// </summary>
        /// <param name="id">The ID of the currency to retrieve.</param>
        /// <returns>The currency details.</returns>
        [HttpGet("GetById")]
        [ProducesResponseType(typeof(CurrencyDto), 200)] // OK
        [ProducesResponseType(404)] // Not Found
        [ProducesResponseType(typeof(string), 400)] // Bad Request
        public async Task<IActionResult> GetCurrencyById(int id)
        {
            try
            {
                var result = await _currencyService.GetByIdAsync(id);
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves a paginated list of all currencies.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve (default is 1).</param>
        /// <param name="pageSize">The number of items per page (default is 10).</param>
        /// <returns>A paginated list of currencies.</returns>
        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(IEnumerable<CurrencyDto>), 200)] // OK
        [ProducesResponseType(typeof(string), 400)] // Bad Request
        public async Task<IActionResult> GetAllCurrencies(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var result = await _currencyService.GetAllAsync(pageNumber, pageSize, null, null, false);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing currency.
        /// </summary>
        /// <param name="id">The ID of the currency to update.</param>
        /// <param name="dto">The data transfer object containing updated currency details.</param>
        /// <returns>No content if the update is successful.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)] // No Content
        [ProducesResponseType(typeof(string), 400)] // Bad Request
        public async Task<IActionResult> UpdateCurrency(int id, [FromBody] CurrencyDto dto)
        {
            try
            {
                if (id != dto.Id)
                    return BadRequest("ID mismatch.");

                await _currencyService.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a currency by its ID.
        /// </summary>
        /// <param name="id">The ID of the currency to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)] // No Content
        [ProducesResponseType(typeof(string), 400)] // Bad Request
        public async Task<IActionResult> DeleteCurrency(int id)
        {
            try
            {
                await _currencyService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

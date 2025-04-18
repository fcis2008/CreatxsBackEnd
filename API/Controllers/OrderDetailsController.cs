using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controller for managing orderDetails-related operations.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailsService _orderDetailsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderDetailsController"/> class.
        /// </summary>
        /// <param name="orderDetailsService">The service for handling orderDetails operations.</param>
        public OrderDetailsController(IOrderDetailsService orderDetailsService)
        {
            _orderDetailsService = orderDetailsService;
        }

        /// <summary>
        /// Creates a new orderDetails.
        /// </summary>
        /// <param name="dto">The data transfer object containing orderDetails details.</param>
        /// <returns>The created orderDetails's ID.</returns>
        [HttpPost("Create")]
        [ProducesResponseType(typeof(int), 201)] // Created
        [ProducesResponseType(typeof(string), 400)] // Bad Request
        public async Task<IActionResult> CreateOrderDetails([FromBody] List<OrderDetailsCreateDto> dto)
        {
            try
            {
                var id = await _orderDetailsService.CreateListAsync(dto);
                return CreatedAtAction(nameof(GetOrderDetailsById), new { id }, id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves a orderDetails by its ID.
        /// </summary>
        /// <param name="id">The ID of the orderDetails to retrieve.</param>
        /// <returns>The orderDetails details.</returns>
        [HttpGet("GetById")]
        [ProducesResponseType(typeof(OrderDetailsDto), 200)] // OK
        [ProducesResponseType(404)] // Not Found
        [ProducesResponseType(typeof(string), 400)] // Bad Request
        public async Task<IActionResult> GetOrderDetailsById(int id)
        {
            try
            {
                var result = await _orderDetailsService.GetByIdAsync(id);
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
        /// Retrieves a paginated list of all order details.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve (default is 1).</param>
        /// <param name="pageSize">The number of items per page (default is 10).</param>
        /// <returns>A paginated list of order details.</returns>
        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(IEnumerable<OrderDetailsDto>), 200)] // OK
        [ProducesResponseType(typeof(string), 400)] // Bad Request
        public async Task<IActionResult> GetAllOrderDetails(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var result = await _orderDetailsService.GetAllAsync(pageNumber, pageSize, null, null, false);
                if (result is IEnumerable<OrderDetailsDto> orderDetails)
                {
                    return Ok(orderDetails);
                }
                return BadRequest("Unexpected result type.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing orderDetails.
        /// </summary>
        /// <param name="id">The ID of the orderDetails to update.</param>
        /// <param name="dto">The data transfer object containing updated orderDetails details.</param>
        /// <returns>No content if the update is successful.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)] // No Content
        [ProducesResponseType(typeof(string), 400)] // Bad Request
        public async Task<IActionResult> UpdateOrderDetails(int id, [FromBody] OrderDetailsDto dto)
        {
            try
            {
                if (id != dto.Id)
                    return BadRequest("ID mismatch.");

                await _orderDetailsService.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a orderDetails by its ID.
        /// </summary>
        /// <param name="id">The ID of the orderDetails to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)] // No Content
        [ProducesResponseType(typeof(string), 400)] // Bad Request
        public async Task<IActionResult> DeleteOrderDetails(int id)
        {
            try
            {
                var result = await _orderDetailsService.GetByIdAsync(id);
                if (result == null)
                    return NotFound("Entity wasn't found");

                await _orderDetailsService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

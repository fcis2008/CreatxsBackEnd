using Application.DTOs;
using Application.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controller for managing Product-related operations.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _branchService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="branchService">The service for handling branch operations.</param>
        public ProductController(IProductService branchService)
        {
            _branchService = branchService;
        }

        /// <summary>
        /// Creates a new Product.
        /// </summary>
        /// <param name="dto">The data transfer object containing Product details.</param>
        /// <returns>The created Product's ID.</returns>
        [HttpPost("Create")]
        [ProducesResponseType(typeof(int), 201)] // Created
        [ProducesResponseType(typeof(string), 400)] // Bad Request
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto dto)
        {
            try
            {
                var id = await _branchService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetProductById), new { id }, id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves a Product by its ID.
        /// </summary>
        /// <param name="id">The ID of the branch to retrieve.</param>
        /// <returns>branch details.</returns>
        [HttpGet("GetById")]
        [ProducesResponseType(typeof(ProductDto), 200)] // OK
        [ProducesResponseType(404)] // Not Found
        [ProducesResponseType(typeof(string), 400)] // Bad Request
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var result = await _branchService.GetByIdAsync(id);
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
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), 200)] // OK
        [ProducesResponseType(typeof(string), 400)] // Bad Request
        public async Task<IActionResult> GetAllProducts(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var result = await _branchService.GetAllAsync(pageNumber, pageSize, null, null, false);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing Product.
        /// </summary>
        /// <param name="id">The ID of the Product to update.</param>
        /// <param name="dto">The data transfer object containing updated Product details.</param>
        /// <returns>No content if the update is successful.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)] // No Content
        [ProducesResponseType(typeof(string), 400)] // Bad Request
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto dto)
        {
            try
            {
                if (id != dto.Id)
                    return BadRequest("ID mismatch.");

                await _branchService.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a Product by its ID.
        /// </summary>
        /// <param name="id">The ID of the Product to delete.</param>
        /// <returns>No content if the deletion is successful, or a not found message if the entity doesn't exist.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)] // No Content
        [ProducesResponseType(typeof(string), 400)] // Bad Request
        [ProducesResponseType(typeof(string), 404)] // Not Found
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var result = await _branchService.GetByIdAsync(id);
                if (result == null)
                    return NotFound("Entity wasn't found");

                await _branchService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

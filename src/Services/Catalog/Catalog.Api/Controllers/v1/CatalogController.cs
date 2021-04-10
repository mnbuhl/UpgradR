using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Core.Contracts.v1.Products;
using Catalog.Core.Entities;
using Catalog.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Catalog.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository productRepository, ILogger<CatalogController> logger,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetAll()
        {
            IEnumerable<Product> products = await _productRepository.GetAllProducts();
            var productResponses = _mapper.Map<IEnumerable<ProductResponseDto>>(products);

            return Ok(productResponses);
        }

        [HttpGet("{id:length(24)}", Name = "GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductResponseDto>> GetById(string id)
        {
            var product = await _productRepository.GetProduct(id);

            if (product == null)
            {
                _logger.LogInformation($"Product with id: {id} not found.");
                return NotFound();
            }

            return Ok(_mapper.Map<ProductResponseDto>(product));
        }

        [HttpGet("Category/{category}", Name = "GetByCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetByCategory(string category)
        {
            IEnumerable<Product> products = await _productRepository.GetProductsByCategory(category);

            if (products == null)
            {
                _logger.LogInformation($"Products with category: {category} not found.");
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<ProductResponseDto>>(products));
        }

        [HttpGet("Search/{name}", Name = "GetByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetByName(string name)
        {
            IEnumerable<Product> products = await _productRepository.GetProductsByName(name);

            if (products == null)
            {
                _logger.LogInformation($"Search result for: {name} returned no products.");
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<ProductResponseDto>>(products));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductResponseDto>> Create([FromBody] ProductCommandDto productRequest)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Attempted to create product with bad ModelState");
                return BadRequest(ModelState);
            }

            var product = _mapper.Map<Product>(productRequest);
            await _productRepository.CreateProduct(product);

            var productResponse = _mapper.Map<ProductResponseDto>(product);

            return CreatedAtRoute("GetById", new { id = product.Id }, productResponse);
        }

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update([FromBody] ProductCommandDto productRequest)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Attempted to update product with bad ModelState");
                return BadRequest(ModelState);
            }

            var product = _mapper.Map<Product>(productRequest);

            bool updated = await _productRepository.UpdateProduct(product);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(string id)
        {
            bool deleted = await _productRepository.DeleteProduct(id);

            return deleted ? NoContent() : NotFound();
        }
    }
}
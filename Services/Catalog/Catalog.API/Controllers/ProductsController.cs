using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Asp.Versioning;
using Catalog.Application.Common.Models;
using Catalog.Application.Features.Products.CreateProduct;
using Catalog.Application.Features.Products.DeleteProduct;
using Catalog.Application.Features.Products.GetProductById;
using Catalog.Application.Features.Products.GetProducts;
using Catalog.Application.Features.Products.Shared;
using Catalog.Application.Features.Products.UpdateProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/products")]
public class ProductsController : ControllerBase
{
  private readonly ISender _sender;

  public ProductsController(ISender sender)
  {
    _sender = sender;
  }

  [HttpGet]
  [ProducesResponseType(typeof(Result<IList<ProductDto>>), (int)HttpStatusCode.OK)]
  public async Task<ActionResult<Result<IList<ProductDto>>>> GetProducts([FromQuery] string productName,
    [FromQuery] string brandName)
  {
    var query = new GetProductsQuery();
    var result = await _sender.Send(query);

    return result.IsSuccess ? Ok(result) : NotFound(result);
  }

  [HttpGet("{id}")]
  [ProducesResponseType(typeof(Result<ProductDto>), (int)HttpStatusCode.OK)]
  [ProducesResponseType((int)HttpStatusCode.NotFound)]
  public async Task<ActionResult<Result<ProductDto>>> GetProductById([FromRoute] string id)
  {
    var query = new GetProductByIdQuery(id);
    var result = await _sender.Send(query);

    return result.IsSuccess ? Ok(result) : NotFound(result);
  }


  [HttpPost]
  [ProducesResponseType(typeof(Result<ProductDto>), (int)HttpStatusCode.Created)]
  [ProducesResponseType((int)HttpStatusCode.BadRequest)]
  public async Task<ActionResult<Result<ProductDto>>> CreateProduct(
    [FromBody] CreateProductCommand createProductCommand)
  {
    var result = await _sender.Send(createProductCommand);

    return result.IsSuccess
      ? CreatedAtAction(nameof(GetProductById), new { id = result.Data!.Id }, result)
      : BadRequest(result);
  }

  [HttpPut("{id}")]
  [ProducesResponseType(typeof(Result<Unit>), (int)HttpStatusCode.OK)]
  [ProducesResponseType(typeof(Result<Unit>), (int)HttpStatusCode.BadRequest)]
  public async Task<ActionResult<bool>> UpdateProduct([FromRoute] string id,
    [FromBody] UpdateProductCommand updateProductCommand)
  {
    var result = await _sender.Send(updateProductCommand);
    return result.IsSuccess ? Ok(result) : BadRequest(result);
  }

  [HttpDelete("{id}")]
  [ProducesResponseType(typeof(Result<ProductDto>), (int)HttpStatusCode.NoContent)]
  [ProducesResponseType(typeof(Result<ProductDto>), (int)HttpStatusCode.BadRequest)]
  public async Task<IActionResult> DeleteProduct(string id)
  {
    var command = new DeleteProductCommand(id);
    var result = await _sender.Send(command);

    return result.IsSuccess ? NoContent() : BadRequest(result);
  }
}
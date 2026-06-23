using System.Collections.Generic;
using System.Threading.Tasks;
using Asp.Versioning;
using Catalog.Application.Common.Models;
using Catalog.Application.Features.Products.CreateProduct;
using Catalog.Application.Features.Products.DeleteProduct;
using Catalog.Application.Features.Products.GetProductById;
using Catalog.Application.Features.Products.GetProducts;
using Catalog.Application.Features.Products.Shared;
using Catalog.Application.Features.Products.UpdateProduct;
using Catalog.Core.helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/products")]
public class ProductsController(ISender sender) : ControllerBase
{
  [HttpGet]
  [ProducesResponseType(typeof(Result<IList<ProductDto>>), StatusCodes.Status200OK)]
  public async Task<ActionResult<Result<IList<ProductDto>>>> GetProducts([FromQuery] QueryParams queryParams)
  {
    var query = new GetProductsQuery(queryParams);
    var result = await sender.Send(query);

    return result.IsSuccess ? Ok(result) : NotFound(result);
  }

  [HttpGet("{id}")]
  [ProducesResponseType(typeof(Result<ProductDto>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<Result<ProductDto>>> GetProductById([FromRoute] string id)
  {
    var query = new GetProductByIdQuery(id);
    var result = await sender.Send(query);

    return result.IsSuccess ? Ok(result) : NotFound(result);
  }


  [HttpPost]
  [ProducesResponseType(typeof(Result<ProductDto>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(Result<ProductDto>), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<Result<ProductDto>>> CreateProduct(
    [FromBody] CreateProductCommand createProductCommand)
  {
    var result = await sender.Send(createProductCommand);

    return result.IsSuccess
      ? Ok(result)
      : BadRequest(result);
  }

  [HttpPut("{id}")]
  [ProducesResponseType(typeof(Result<Unit>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(Result<Unit>), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<bool>> UpdateProduct([FromRoute] string id,
    [FromBody] UpdateProductCommand updateProductCommand)
  {
    var result = await sender.Send(updateProductCommand);
    return result.IsSuccess ? Ok(result) : BadRequest(result);
  }

  [HttpDelete("{id}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(typeof(Result<Unit>), StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> DeleteProduct(string id)
  {
    var command = new DeleteProductCommand(id);
    var result = await sender.Send(command);

    return result.IsSuccess ? NoContent() : BadRequest(result);
  }
}
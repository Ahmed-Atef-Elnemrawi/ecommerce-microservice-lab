using Asp.Versioning;
using Basket.Application.Common.Dto;
using Basket.Application.Common.Models.ResultModel;
using Basket.Application.Features.ShoppingCart.Create;
using Basket.Application.Features.ShoppingCart.Delete;
using Basket.Application.Features.ShoppingCart.GetByUserName;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/basket")]
public class BasketController(ISender sender) : ControllerBase
{
  [HttpGet("{username}")]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(Result<CartDto>), StatusCodes.Status200OK)]
  public async Task<ActionResult<Result<CartDto>>> GetByUsername([FromRoute] string username)
  {
    var result = await sender.Send(new GetByUserNameQuery(username));
    return result.IsSuccess ? Ok(result) : NotFound(result);
  }

  [HttpPost("{username}")]
  [ProducesResponseType(typeof(Result<CartDto>), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(CartDto), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<Result<CartDto>>> CreateCart([FromRoute] string username,
    [FromBody] CreateCartCommand command)
  {
    command = command with { UserName = username };
    var result = await sender.Send(command);
    return result.IsSuccess ? Ok(result) : BadRequest(result);
  }

  [HttpDelete("{username}")]
  [ProducesResponseType(typeof(Result<Unit>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(Result<Unit>), StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> DeleteCart([FromRoute] string username)
  {
    var result = await sender.Send(new DeleteCartCommand(username));
    return result.IsSuccess ? Ok(result) : BadRequest(result);
  }
}
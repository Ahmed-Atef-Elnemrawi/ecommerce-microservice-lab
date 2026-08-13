using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Common.Models.ResultModel;
using Ordering.Application.Contracts.Ordering;
using Ordering.Application.Features.Ordering.Create;
using Ordering.Application.Features.Ordering.Delete;
using Ordering.Application.Features.Ordering.GetByUserName;
using Ordering.Application.Features.Ordering.Update;

namespace Ordering.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/orders")]
public sealed class OrderController(ISender sender) : ControllerBase
{
  [HttpGet("{userName}")]
  [ProducesResponseType(typeof(Result<IEnumerable<OrderDto>>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<Result<IEnumerable<OrderDto>>>> GetByUserName([FromRoute] string userName)
  {
    var query = new GetOrdersByUserNameQuery(userName);
    var result = await sender.Send(query);

    return result.IsSuccess ? Ok(result) : NotFound(result);
  }

  [HttpPost]
  [ProducesResponseType(typeof(Result<OrderDto>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<Result<OrderDto>>> CreateOrder([FromBody] CreateOrderCommand command)
  {
    var result = await sender.Send(command);
    return result.IsSuccess ? Ok(result) : BadRequest(result);
  }

  [HttpPut("{orderId:int}")]
  [ProducesResponseType(typeof(Result<OrderDto>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<Result<OrderDto>>> UpdateOrder([FromRoute] int orderId,
    [FromBody] UpdateOrderCommand command)
  {
    command = command with { Id = orderId };
    var result = await sender.Send(command);

    return result.IsSuccess ? Ok(result) : BadRequest(result);
  }

  [HttpDelete("{orderId:int}")]
  [ProducesResponseType(typeof(Result<Unit>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<Result<Unit>>> DeleteOrder([FromRoute] int orderId)
  {
    var command = new DeleteOrderCommand(orderId);
    var result = await sender.Send(command);

    return result.IsSuccess ? Ok(result) : BadRequest(result);
  }
}
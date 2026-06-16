using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Asp.Versioning;
using Catalog.Application.Common.Models;
using Catalog.Application.Features.Types.GetTypes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/products/types")]
public class TypesController : ControllerBase
{
  private readonly ISender _sender;

  public TypesController(ISender sender)
  {
    _sender = sender;
  }

  [HttpGet]
  [ProducesResponseType(typeof(Result<IList<TypeDto>>), (int)HttpStatusCode.OK)]
  public async Task<ActionResult<Result<IList<TypeDto>>>> GetTypes()
  {
    var query = new GetTypesQuery();
    var result = await _sender.Send(query);
    return Ok(result);
  }
}
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Asp.Versioning;
using Catalog.Application.Common.Models;
using Catalog.Application.Features.Brands.GetBrands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/products/brands")]
public class BrandsController: ControllerBase
{
  private readonly ISender _sender;

  public BrandsController(ISender sender)
  {
    _sender = sender;
  }

  [HttpGet]
  [ProducesResponseType(typeof(IList<Result<BrandDto>>), (int)HttpStatusCode.OK)]
  public async Task<ActionResult<Result<IList<BrandDto>>>> GetBrands()
  {
    var query = new GetBrandsQuery();
    var result = await _sender.Send(query);
    return Ok(result);
  }
}
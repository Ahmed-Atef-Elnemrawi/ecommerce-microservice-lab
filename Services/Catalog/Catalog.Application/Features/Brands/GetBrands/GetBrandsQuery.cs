using Catalog.Application.Common.Models;
using MediatR;

namespace Catalog.Application.Features.Brands.GetBrands;

public record GetBrandsQuery : IRequest<Result<IList<BrandDto>>>
{

}
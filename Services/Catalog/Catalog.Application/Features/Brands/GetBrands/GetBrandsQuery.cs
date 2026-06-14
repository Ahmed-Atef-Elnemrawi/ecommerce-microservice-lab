using MediatR;

namespace Catalog.Application.Features.Brands.GetBrands;

public record GetBrandsQuery : IRequest<IList<ProductBrandDto>>
{

}
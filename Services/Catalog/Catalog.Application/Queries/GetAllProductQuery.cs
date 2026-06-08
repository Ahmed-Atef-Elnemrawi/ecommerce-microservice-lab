using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries;

public record GetAllProductsQuery :  IRequest<IList<ProductDto>>;
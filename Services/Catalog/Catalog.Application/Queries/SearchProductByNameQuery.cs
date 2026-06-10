using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries;

public record SearchProductsByNameQuery(string Name) : IRequest<IList<ProductDto>>;
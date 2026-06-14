using MediatR;

namespace Catalog.Application.Features.Types.GetTypes;

public record GetTypesQuery: IRequest<IList<ProductTypeDto>>;
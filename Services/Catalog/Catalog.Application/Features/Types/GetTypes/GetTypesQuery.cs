using Catalog.Application.Common.Models;
using MediatR;

namespace Catalog.Application.Features.Types.GetTypes;

public record GetTypesQuery: IRequest<Result<IList<TypeDto>>>;
using MediatR;
using Ordering.Application.Common.Models.ResultModel;

namespace Ordering.Application.Features.Ordering.Delete;

public sealed record DeleteOrderCommand(int OrderId): IRequest<Result<Unit>>;
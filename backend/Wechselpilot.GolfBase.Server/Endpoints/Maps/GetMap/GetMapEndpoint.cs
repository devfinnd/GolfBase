using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wechselpilot.GolfBase.Data;
using Wechselpilot.GolfBase.Data.Entities;
using Wechselpilot.GolfBase.Server.Endpoints.Models;
using Wechselpilot.GolfBase.Server.Extensions;

namespace Wechselpilot.GolfBase.Server.Endpoints.Maps.GetMap;

public sealed class GetMapEndpoint : IEndpoint
{
    public sealed record GetMapParameters(
        [FromRoute] Guid MapId
    );

    public static string EndpointName => "GetMap";

    public static RouteHandlerBuilder ConfigureEndpoint(IEndpointRouteBuilder builder, string route) =>
        builder.MapGet(route, HandleGetMap)
            .Produces<GetMapResponse>()
            .ProducesNotFound()
            .ProducesInternalServerError();

    private static async Task<IResult> HandleGetMap(
        [AsParameters] GetMapParameters parameters,
        [FromServices] GolfDbContext dbContext,
        CancellationToken cancellationToken = default)
    {
        MapEntity? mapEntity = await dbContext.Maps
            .Include(m => m.Sessions.Where(s => s.Timestamp >= DateTime.UtcNow.AddDays(-30)))
            .ThenInclude(s => s.Results)
            .FirstOrDefaultAsync(m => m.MapId == parameters.MapId, cancellationToken);

        if (mapEntity is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(new GetMapResponse(
            Map: mapEntity.ToMap()
        ));
    }
}

using GolfBase.ApiContracts.Maps.GetMapSessions;
using GolfBase.ApiContracts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wechselpilot.GolfBase.Data;
using Wechselpilot.GolfBase.Data.Entities;
using Wechselpilot.GolfBase.Server.Extensions;

namespace Wechselpilot.GolfBase.Server.Endpoints.Maps.GetMapSessions;

public sealed class GetMapSessionsEndpoint : IEndpoint
{
    public static string EndpointName => "GetMapSessions";

    public static RouteHandlerBuilder ConfigureEndpoint(IEndpointRouteBuilder builder, string route) =>
        builder.MapGet(route, HandleGetMapSessions)
            .Produces<GetMapSessionsResponse>()
            .ProducesNotFound()
            .ProducesValidationProblem()
            .ProducesInternalServerError();

    private static async Task<IResult> HandleGetMapSessions(
        [AsParameters] GetMapSessionsParameters parameters,
        [FromServices] GolfDbContext dbContext,
        CancellationToken cancellationToken
    )
    {
        MapEntity? map = await dbContext.Maps
            .Include(x => x.Sessions)
            .ThenInclude(x => x.Results)
            .FirstOrDefaultAsync(x => x.MapId == parameters.MapId, cancellationToken);

        if (map is null)
        {
            return TypedResults.NotFound();
        }

        Session[] sessions = map.Sessions
            .Where(x => parameters.From is null || x.Timestamp >= parameters.From)
            .Where(x => parameters.To is null || x.Timestamp <= parameters.To)
            .Select(x => x.ToSession())
            .ToArray();

        return TypedResults.Ok(new GetMapSessionsResponse(sessions));
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wechselpilot.GolfBase.Data;
using Wechselpilot.GolfBase.Data.Entities;
using Wechselpilot.GolfBase.Server.Endpoints.Models;
using Wechselpilot.GolfBase.Server.Extensions;

namespace Wechselpilot.GolfBase.Server.Endpoints.Players.GetPlayerSessions;

public sealed class GetPlayerSessionsEndpoint : IEndpoint
{
    public sealed record GetPlayerSessionsParameters(
        [FromRoute] Guid PlayerId,
        [FromQuery] DateTime? From,
        [FromQuery] DateTime? To
    );

    public static string EndpointName => "GetPlayerSessions";

    public static RouteHandlerBuilder ConfigureEndpoint(IEndpointRouteBuilder builder, string route) =>
        builder.MapGet(route, HandleGetPlayerSessions)
            .Produces<GetPlayerSessionsResponse>()
            .ProducesNotFound()
            .ProducesValidationProblem()
            .ProducesInternalServerError();

    private static async Task<IResult> HandleGetPlayerSessions(
        [AsParameters] GetPlayerSessionsParameters parameters,
        [FromServices] GolfDbContext dbContext,
        CancellationToken cancellationToken
    )
    {
        PlayerEntity? player = await dbContext.Players
            .Include(x => x.Sessions)
            .ThenInclude(x => x.Results.Where(r => r.PlayerId == parameters.PlayerId))
            .FirstOrDefaultAsync(x => x.PlayerId == parameters.PlayerId, cancellationToken);

        if (player is null)
        {
            return TypedResults.NotFound();
        }

        Session[] sessions = player.Sessions
            .Where(x => parameters.From is null || x.Timestamp >= parameters.From)
            .Where(x => parameters.To is null || x.Timestamp <= parameters.To)
            .Select(x => x.ToSession())
            .ToArray();

        return TypedResults.Ok(new GetPlayerSessionsResponse(sessions));
    }
}

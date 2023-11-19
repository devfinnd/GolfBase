using GolfBase.ApiContracts.Models;
using GolfBase.ApiContracts.Seasons.GetSeasonSessions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wechselpilot.GolfBase.Data;
using Wechselpilot.GolfBase.Data.Entities;
using Wechselpilot.GolfBase.Server.Extensions;

namespace Wechselpilot.GolfBase.Server.Endpoints.Seasons.GetSeasonSessions;

public sealed class GetSeasonSessionsEndpoint : IEndpoint
{
    public static string EndpointName => "GetSeasonSessions";

    public static RouteHandlerBuilder ConfigureEndpoint(IEndpointRouteBuilder builder, string route) =>
        builder.MapGet(route, HandleGetSeasonSessions)
            .Produces<GetSeasonSessionsResponse>()
            .ProducesNotFound()
            .ProducesValidationProblem()
            .ProducesInternalServerError();

    private static async Task<IResult> HandleGetSeasonSessions(
        [AsParameters] GetSeasonSessionsParameters parameters,
        [FromServices] GolfDbContext dbContext,
        CancellationToken cancellationToken
    )
    {
        SeasonEntity? season = await dbContext.Seasons
            .Include(x => x.Maps)
            .ThenInclude(x => x.Sessions)
            .ThenInclude(x => x.Results)
            .FirstOrDefaultAsync(x => x.SeasonId == parameters.SeasonId, cancellationToken);

        if (season is null)
        {
            return TypedResults.NotFound();
        }

        Session[] sessions = season.Maps
            .SelectMany(x => x.Sessions)
            .Where(x => parameters.From is null || x.Timestamp >= parameters.From)
            .Where(x => parameters.To is null || x.Timestamp <= parameters.To)
            .Select(x => x.ToSession())
            .ToArray();

        return TypedResults.Ok(new GetSeasonSessionsResponse(sessions));
    }
}

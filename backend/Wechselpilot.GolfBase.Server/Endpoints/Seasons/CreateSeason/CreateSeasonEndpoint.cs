using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wechselpilot.GolfBase.Data;
using Wechselpilot.GolfBase.Data.Entities;
using Wechselpilot.GolfBase.Server.Endpoints.Seasons.GetSeason;
using Wechselpilot.GolfBase.Server.Extensions;

namespace Wechselpilot.GolfBase.Server.Endpoints.Seasons.CreateSeason;

public sealed class CreateSeasonEndpoint : IEndpoint
{
    public sealed record CreateSeasonParameters(
        [FromBody] CreateSeasonRequest Body
    );

    public static string EndpointName => "CreateSeason";

    public static RouteHandlerBuilder ConfigureEndpoint(IEndpointRouteBuilder builder, string route) =>
        builder.MapPost(route, HandleCreateSeason)
            .Produces<CreateSeasonResponse>((int)HttpStatusCode.Created)
            .ProducesValidationProblem()
            .ProducesInternalServerError();

    public static async Task<IResult> HandleCreateSeason(
        [AsParameters] CreateSeasonParameters parameters,
        [FromServices] GolfDbContext dbContext,
        CancellationToken cancellationToken = default)
    {
        MapEntity[] maps = await dbContext.Maps
            .Where(map => parameters.Body.MapIds.Contains(map.MapId))
            .ToArrayAsync(cancellationToken);

        if (maps.Length != parameters.Body.MapIds.Length)
        {
            return TypedResults.BadRequest(new ProblemDetails
            {
                Title = "Invalid map ids",
                Detail = "One or more map ids are invalid"
            });
        }

        SeasonEntity season = new SeasonEntity(
            parameters.Body.Name,
            parameters.Body.StartDate,
            parameters.Body.EndDate,
            maps
        );

        await dbContext.Seasons.AddAsync(season, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return TypedResults.CreatedAtRoute(
            new CreateSeasonResponse(season.SeasonId),
            GetSeasonEndpoint.EndpointName,
            new { season.SeasonId }
        );
    }
}

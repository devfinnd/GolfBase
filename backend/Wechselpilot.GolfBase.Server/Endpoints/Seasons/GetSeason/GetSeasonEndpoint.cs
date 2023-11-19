using GolfBase.ApiContracts.Seasons.GetSeason;
using Microsoft.AspNetCore.Mvc;
using Wechselpilot.GolfBase.Data;
using Wechselpilot.GolfBase.Data.Entities;
using Wechselpilot.GolfBase.Server.Extensions;

namespace Wechselpilot.GolfBase.Server.Endpoints.Seasons.GetSeason;

public sealed class GetSeasonEndpoint : IEndpoint
{
    public static string EndpointName => "GetSeason";

    public static RouteHandlerBuilder ConfigureEndpoint(IEndpointRouteBuilder builder, string route) =>
        builder.MapGet(route, HandleGetSeason)
            .Produces<GetSeasonResponse>()
            .ProducesNotFound()
            .ProducesValidationProblem()
            .ProducesInternalServerError();

    public static async Task<IResult> HandleGetSeason(
        [AsParameters] GetSeasonParameters request,
        [FromServices] GolfDbContext dbContext,
        CancellationToken cancellationToken = default)
    {
        SeasonEntity? season = await dbContext.Seasons.FindAsync(new object[] { request.SeasonId }, cancellationToken);

        if (season is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(new GetSeasonResponse(
            Season: season.ToSeason()
        ));
    }
}

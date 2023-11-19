using GolfBase.ApiContracts.Models;
using GolfBase.ApiContracts.Seasons;
using GolfBase.ApiContracts.Seasons.GetSeasons;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wechselpilot.GolfBase.Data;
using Wechselpilot.GolfBase.Data.Entities;
using Wechselpilot.GolfBase.Server.Extensions;

namespace Wechselpilot.GolfBase.Server.Endpoints.Seasons.GetSeasons;

public sealed class GetSeasonsEndpoint : IEndpoint
{
    public static string EndpointName => "GetSeasons";

    public static RouteHandlerBuilder ConfigureEndpoint(IEndpointRouteBuilder builder, string route) =>
        builder.MapGet(route, HandleGetSeasons)
            .Produces<GetSeasonsResponse>()
            .ProducesValidationProblem()
            .ProducesInternalServerError();

    public static async Task<IResult> HandleGetSeasons(
        [AsParameters] GetSeasonsParameters parameters,
        [FromServices] GolfDbContext dbContext,
        CancellationToken cancellationToken = default)
    {
        IQueryable<SeasonEntity> queryable = dbContext.Seasons;

        int totalItems = await queryable.CountAsync(cancellationToken);
        int totalPages = (int)Math.Ceiling((double)totalItems / parameters.PageSize);

        SeasonListItem[] seasons = await queryable
            .OrderBy(season => season.StartDate)
            .Skip(Math.Max(parameters.Page - 1, 0) * parameters.PageSize)
            .Take(parameters.PageSize)
            .Select(s => new SeasonListItem(
                s.SeasonId,
                s.Name,
                s.StartDate,
                s.EndDate
            )).ToArrayAsync(cancellationToken);

        return TypedResults.Ok(new GetSeasonsResponse(
            Seasons: seasons,
            TotalItems: totalItems,
            TotalPages: totalPages
        ));
    }
}

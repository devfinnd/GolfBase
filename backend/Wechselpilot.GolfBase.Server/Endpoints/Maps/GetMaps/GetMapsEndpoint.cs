using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wechselpilot.GolfBase.Data;
using Wechselpilot.GolfBase.Data.Entities;
using Wechselpilot.GolfBase.Server.Endpoints.Models;
using Wechselpilot.GolfBase.Server.Extensions;

namespace Wechselpilot.GolfBase.Server.Endpoints.Maps.GetMaps;

public sealed class GetMapsEndpoint : IEndpoint
{
    public sealed record GetMapsParameters(
        int Page,
        int PageSize,
        string? SearchTerm
    );

    public static string EndpointName => "GetMaps";

    public static RouteHandlerBuilder ConfigureEndpoint(IEndpointRouteBuilder builder, string route) =>
        builder.MapGet(route, HandleGetMaps)
            .Produces<GetMapsResponse>()
            .ProducesInternalServerError()
            .ProducesValidationProblem();

    private static async Task<IResult> HandleGetMaps(
        [AsParameters] GetMapsParameters parameters,
        [FromServices] GolfDbContext dbContext,
        CancellationToken cancellationToken = default)
    {
        IQueryable<MapEntity> queryable = dbContext.Maps;

        if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
        {
            queryable = queryable.Where(map => map.Name.Contains(parameters.SearchTerm));
        }

        int totalItems = await queryable.CountAsync(cancellationToken);
        int totalPages = (int)Math.Ceiling(totalItems / (double)parameters.PageSize);

        MapListItem[] maps = await queryable
            .OrderBy(map => map.Name)
            .Skip(Math.Max(parameters.Page - 1, 0) * parameters.PageSize)
            .Take(parameters.PageSize)
            .Select(m => new MapListItem(
                m.MapId,
                m.Name,
                m.Par,
                m.Holes
            )).ToArrayAsync(cancellationToken);

        return TypedResults.Ok(new GetMapsResponse(
            Maps: maps,
            TotalItems: totalItems,
            TotalPages: totalPages
        ));
    }
}

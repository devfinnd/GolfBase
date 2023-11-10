using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wechselpilot.GolfBase.Data;
using Wechselpilot.GolfBase.Data.Entities;
using Wechselpilot.GolfBase.Server.Endpoints.Models;
using Wechselpilot.GolfBase.Server.Extensions;

namespace Wechselpilot.GolfBase.Server.Endpoints.Players.GetPlayers;

public sealed class GetPlayersEndpoint : IEndpoint
{
    public sealed record GetPlayersParameters(
        [FromQuery] int Page,
        [FromQuery] int PageSize,
        [FromQuery] string? SearchTerm
    );

    public static string EndpointName => "GetPlayers";

    public static RouteHandlerBuilder ConfigureEndpoint(IEndpointRouteBuilder builder, string route) =>
        builder.MapGet(route, HandleGetPlayers)
            .Produces<GetPlayersResponse>()
            .ProducesValidationProblem()
            .ProducesInternalServerError();

    public static async Task<IResult> HandleGetPlayers(
        [AsParameters] GetPlayersParameters parameters,
        [FromServices] GolfDbContext dbContext,
        CancellationToken cancellationToken = default)
    {
        IQueryable<PlayerEntity> queryable = dbContext.Players;

        if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
        {
            queryable = queryable.Where(player => player.Name.Contains(parameters.SearchTerm));
        }

        int totalItems = await queryable.CountAsync(cancellationToken);
        int totalPages = (int)Math.Ceiling((double)totalItems / parameters.PageSize);

        var players = await queryable
            .OrderBy(player => player.Name)
            .Skip(Math.Max(parameters.Page - 1, 0) * parameters.PageSize)
            .Take(parameters.PageSize)
            .Select(p => new PlayerListItem(
                p.PlayerId,
                p.Name
            ))
            .ToArrayAsync(cancellationToken);

        return TypedResults.Ok(new GetPlayersResponse(
            players,
            totalItems,
            totalPages
        ));
    }
}

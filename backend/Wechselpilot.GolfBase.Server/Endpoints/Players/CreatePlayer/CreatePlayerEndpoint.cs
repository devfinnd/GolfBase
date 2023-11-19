using System.Net;
using GolfBase.ApiContracts.Players.CreatePlayer;
using Microsoft.AspNetCore.Mvc;
using Wechselpilot.GolfBase.Data;
using Wechselpilot.GolfBase.Data.Entities;
using Wechselpilot.GolfBase.Server.Endpoints.Players.GetPlayer;
using Wechselpilot.GolfBase.Server.Extensions;

namespace Wechselpilot.GolfBase.Server.Endpoints.Players.CreatePlayer;

public sealed class CreatePlayerEndpoint : IEndpoint
{
    public sealed record CreatePlayerParameters(
        [FromBody] CreatePlayerRequest Body
    );

    public static string EndpointName => "CreatePlayer";

    public static RouteHandlerBuilder ConfigureEndpoint(IEndpointRouteBuilder builder, string route) =>
        builder.MapPost(route, HandleCreatePlayer)
            .Produces<CreatePlayerResponse>((int)HttpStatusCode.Created)
            .ProducesValidationProblem()
            .ProducesInternalServerError();

    private static async Task<IResult> HandleCreatePlayer(
        [AsParameters] CreatePlayerParameters parameters,
        [FromServices] GolfDbContext dbContext,
        CancellationToken cancellationToken = default)
    {
        PlayerEntity player = new PlayerEntity(
            parameters.Body.Name
        );

        dbContext.Players.Add(player);

        await dbContext.SaveChangesAsync(cancellationToken);

        return TypedResults.CreatedAtRoute(
            new CreatePlayerResponse(player.PlayerId),
            GetPlayerEndpoint.EndpointName,
            new { player.PlayerId }
        );
    }
}
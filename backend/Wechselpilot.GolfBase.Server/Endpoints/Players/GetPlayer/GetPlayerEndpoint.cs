using GolfBase.ApiContracts.Players.GetPlayer;
using Microsoft.AspNetCore.Mvc;
using Wechselpilot.GolfBase.Data;
using Wechselpilot.GolfBase.Data.Entities;
using Wechselpilot.GolfBase.Server.Extensions;

namespace Wechselpilot.GolfBase.Server.Endpoints.Players.GetPlayer;

public sealed class GetPlayerEndpoint : IEndpoint
{
    public static string EndpointName => "GetPlayer";

    public static RouteHandlerBuilder ConfigureEndpoint(IEndpointRouteBuilder builder, string route) =>
        builder.MapGet(route, HandleGetPlayer)
            .Produces<GetPlayerResponse>()
            .ProducesNotFound()
            .ProducesValidationProblem()
            .ProducesInternalServerError();

    public static async Task<IResult> HandleGetPlayer(
        [AsParameters] GetPlayerParameters request,
        [FromServices] GolfDbContext dbContext,
        CancellationToken cancellationToken = default)
    {
        PlayerEntity? playerEntity = await dbContext.Players.FindAsync(new object[] { request.PlayerId }, cancellationToken);

        if (playerEntity is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(new GetPlayerResponse(
            Player: playerEntity.ToPlayer()
        ));
    }
}
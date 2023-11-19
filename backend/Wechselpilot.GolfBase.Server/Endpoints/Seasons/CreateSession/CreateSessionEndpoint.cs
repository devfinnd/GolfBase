using System.Net;
using GolfBase.ApiContracts.Seasons.CreateSession;
using Microsoft.AspNetCore.Mvc;
using Wechselpilot.GolfBase.Data;
using Wechselpilot.GolfBase.Data.Entities;
using Wechselpilot.GolfBase.Server.Extensions;

namespace Wechselpilot.GolfBase.Server.Endpoints.Seasons.CreateSession;

public sealed class CreateSessionEndpoint : IEndpoint
{
    public static string EndpointName => "CreateSession";

    public static RouteHandlerBuilder ConfigureEndpoint(IEndpointRouteBuilder builder, string route) =>
        builder.MapPost(route, HandleCreateSession)
            .Produces<CreateSessionResponse>((int)HttpStatusCode.Created)
            .ProducesValidationProblem()
            .ProducesInternalServerError();

    public static async Task<IResult> HandleCreateSession(
        [AsParameters] CreateSessionParameters parameters,
        [FromServices] GolfDbContext dbContext,
        CancellationToken cancellationToken = default)
    {
        SeasonEntity? season = await dbContext.Seasons.FindAsync(new object[] { parameters.SeasonId }, cancellationToken);

        if (season is null)
        {
            return TypedResults.NotFound();
        }

        SessionEntity session = new SessionEntity(
            parameters.SeasonId,
            parameters.Body.MapId,
            parameters.Body.Results.Select(r => new SessionResultEntity(
                parameters.SeasonId,
                r.PlayerId,
                r.Score
            )).ToArray()
        );

        await dbContext.Sessions.AddAsync(session, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok(new CreateSessionResponse(
            SessionId: session.SessionId
        ));
    }
}

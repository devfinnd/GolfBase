using System.Net;
using GolfBase.ApiContracts.Maps.CreateMap;
using Microsoft.AspNetCore.Mvc;
using Wechselpilot.GolfBase.Data;
using Wechselpilot.GolfBase.Data.Entities;
using Wechselpilot.GolfBase.Server.Endpoints.Maps.GetMap;
using Wechselpilot.GolfBase.Server.Extensions;

namespace Wechselpilot.GolfBase.Server.Endpoints.Maps.CreateMap;

public sealed class CreateMapEndpoint : IEndpoint
{
    public static string EndpointName => "CreateMap";

    public static RouteHandlerBuilder ConfigureEndpoint(IEndpointRouteBuilder builder, string route) =>
        builder.MapPost(route, HandleCreateMap)
            .Produces<CreateMapResponse>((int)HttpStatusCode.Created)
            .ProducesValidationProblem()
            .ProducesInternalServerError();

    private static async Task<IResult> HandleCreateMap(
        [AsParameters] CreateMapParameters parameters,
        [FromServices] GolfDbContext dbContext,
        CancellationToken cancellationToken = default)
    {
        MapEntity map = new MapEntity(
            parameters.Body.Name,
            parameters.Body.Par,
            parameters.Body.Holes
        );

        dbContext.Maps.Add(map);

        await dbContext.SaveChangesAsync(cancellationToken);

        return TypedResults.CreatedAtRoute(
            new CreateMapResponse(map.MapId),
            GetMapEndpoint.EndpointName,
            new { map.MapId }
        );
    }
}

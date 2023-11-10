using System.Net;
using JetBrains.Annotations;
using Wechselpilot.GolfBase.Server.Endpoints;
using ILogger = Serilog.ILogger;

namespace Wechselpilot.GolfBase.Server.Extensions;

public static class EndpointExtensions
{
    public static IEndpointRouteBuilder Map<TEndpoint>(this IEndpointRouteBuilder builder, [RouteTemplate] string route = "/") where TEndpoint : IEndpoint
    {
        builder.ServiceProvider
            .GetRequiredService<ILogger>()
            .Information("Mapping endpoint {EndpointName} to route {Route}", TEndpoint.EndpointName, route);

        TEndpoint.ConfigureEndpoint(builder, route)
            .WithName(TEndpoint.EndpointName);

        return builder;
    }

    public static RouteHandlerBuilder ProducesInternalServerError(this RouteHandlerBuilder builder) =>
        builder.ProducesProblem((int)HttpStatusCode.InternalServerError);

    public static RouteHandlerBuilder ProducesNotFound(this RouteHandlerBuilder builder) =>
        builder.ProducesProblem((int)HttpStatusCode.NotFound);
}

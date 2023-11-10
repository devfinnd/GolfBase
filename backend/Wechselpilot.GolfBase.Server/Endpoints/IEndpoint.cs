using JetBrains.Annotations;

namespace Wechselpilot.GolfBase.Server.Endpoints;

public interface IEndpoint
{
    public static abstract string EndpointName { get; }
    public static abstract RouteHandlerBuilder ConfigureEndpoint(IEndpointRouteBuilder builder, [RouteTemplate] string route);
}

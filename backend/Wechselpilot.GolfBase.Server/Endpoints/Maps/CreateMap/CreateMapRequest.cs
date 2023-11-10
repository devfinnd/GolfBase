namespace Wechselpilot.GolfBase.Server.Endpoints.Maps.CreateMap;

public sealed record CreateMapRequest(
    string Name,
    int Par,
    int Holes
);
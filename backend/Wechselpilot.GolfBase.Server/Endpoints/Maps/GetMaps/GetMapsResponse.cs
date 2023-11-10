using Wechselpilot.GolfBase.Server.Endpoints.Models;

namespace Wechselpilot.GolfBase.Server.Endpoints.Maps.GetMaps;

public sealed record GetMapsResponse(
    MapListItem[] Maps,
    int TotalItems,
    int TotalPages
);

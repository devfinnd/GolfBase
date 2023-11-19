using GolfBase.ApiContracts.Models;

namespace GolfBase.ApiContracts.Maps.GetMaps;

public sealed record GetMapsResponse(
    MapListItem[] Maps,
    int TotalItems,
    int TotalPages
);
using Wechselpilot.GolfBase.Server.Endpoints.Models;

namespace Wechselpilot.GolfBase.Server.Endpoints.Players.GetPlayers;

public sealed record GetPlayersResponse(
    PlayerListItem[] Players,
    int TotalItems,
    int TotalPages
);

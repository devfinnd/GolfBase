using GolfBase.ApiContracts.Models;

namespace GolfBase.ApiContracts.Players.GetPlayers;

public sealed record GetPlayersResponse(
    PlayerListItem[] Players,
    int TotalItems,
    int TotalPages
);
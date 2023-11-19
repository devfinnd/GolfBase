using GolfBase.ApiContracts.Models;

namespace GolfBase.ApiContracts.Players.GetPlayer;

public sealed record GetPlayerResponse(
    Player Player
);
using GolfBase.ApiContracts.Maps;
using GolfBase.ApiContracts.Models;
using GolfBase.ApiContracts.Players;
using GolfBase.ApiContracts.Seasons;
using Wechselpilot.GolfBase.Data.Entities;

namespace Wechselpilot.GolfBase.Server.Endpoints;

public static class MappingExtensions
{
    public static Player ToPlayer(this PlayerEntity entity) => new(
        entity.PlayerId,
        entity.Name
    );

    public static Map ToMap(this MapEntity entity) => new(
        entity.MapId,
        entity.Name,
        entity.Par,
        entity.Holes
    );

    public static Session ToSession(this SessionEntity entity) => new(
        entity.SessionId,
        entity.Timestamp,
        entity.Results.Select(x => x.ToSessionResult()).ToArray()
    );

    public static SessionResult ToSessionResult(this SessionResultEntity entity) => new(
        entity.Player.Name,
        entity.Score
    );

    public static Season ToSeason(this SeasonEntity entity) => new(
        entity.SeasonId,
        entity.Name,
        entity.StartDate,
        entity.EndDate,
        entity.Maps.Select(x => x.ToMap()).ToArray()
    );
}
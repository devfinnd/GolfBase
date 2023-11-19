using GolfBase.ApiContracts.Maps.CreateMap;
using Microsoft.AspNetCore.Mvc;

namespace Wechselpilot.GolfBase.Server.Endpoints.Maps.CreateMap;

public sealed record CreateMapParameters(
    [FromBody] CreateMapRequest Body
);
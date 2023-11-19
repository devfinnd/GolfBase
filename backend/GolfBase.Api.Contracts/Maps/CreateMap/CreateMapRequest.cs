namespace GolfBase.ApiContracts.Maps.CreateMap;

public sealed record CreateMapRequest(
    string Name,
    int Par,
    int Holes
);
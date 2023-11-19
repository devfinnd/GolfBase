using GolfBase.ApiContracts.Players.CreatePlayer;
using Refit;

namespace GolfBase.CompetitionClient.Abstractions;

public interface IGolfBaseApiClient
{
    [Post("/players")]
    public Task<IApiResponse<CreatePlayerResponse>> CreatePlayer(CreatePlayerRequest request, CancellationToken cancellationToken = default);
}

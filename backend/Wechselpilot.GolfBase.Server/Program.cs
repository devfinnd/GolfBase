using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Wechselpilot.GolfBase.Data;
using Wechselpilot.GolfBase.Server.Endpoints.Maps.CreateMap;
using Wechselpilot.GolfBase.Server.Endpoints.Maps.GetMap;
using Wechselpilot.GolfBase.Server.Endpoints.Maps.GetMaps;
using Wechselpilot.GolfBase.Server.Endpoints.Maps.GetMapSessions;
using Wechselpilot.GolfBase.Server.Endpoints.Players.CreatePlayer;
using Wechselpilot.GolfBase.Server.Endpoints.Players.GetPlayer;
using Wechselpilot.GolfBase.Server.Endpoints.Players.GetPlayers;
using Wechselpilot.GolfBase.Server.Endpoints.Players.GetPlayerSessions;
using Wechselpilot.GolfBase.Server.Endpoints.Seasons.CreateSeason;
using Wechselpilot.GolfBase.Server.Endpoints.Seasons.CreateSession;
using Wechselpilot.GolfBase.Server.Endpoints.Seasons.GetSeason;
using Wechselpilot.GolfBase.Server.Endpoints.Seasons.GetSeasons;
using Wechselpilot.GolfBase.Server.Endpoints.Seasons.GetSeasonSessions;
using Wechselpilot.GolfBase.Server.Extensions;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog((context, provider, logger) => logger
        .ReadFrom.Configuration(context.Configuration)
        .WriteTo.Console());

    builder.Services.AddProblemDetails();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "GolfBase API", Version = "v1" });
    });

    builder.Services.AddDbContext<GolfDbContext>(opt =>
    {
        opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

    WebApplication app = builder.Build();

    app.UseSerilogRequestLogging();

    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GolfBase API v1"));

    RouteGroupBuilder players = app.MapGroup("/players").WithTags("Players");
    players.Map<CreatePlayerEndpoint>();
    players.Map<GetPlayersEndpoint>();
    players.Map<GetPlayerEndpoint>("/{playerId}");
    players.Map<GetPlayerSessionsEndpoint>("/{playerId}/sessions");

    RouteGroupBuilder maps = app.MapGroup("/maps").WithTags("Maps");
    maps.Map<CreateMapEndpoint>();
    maps.Map<GetMapsEndpoint>();
    maps.Map<GetMapEndpoint>("/{mapId}");
    maps.Map<GetMapSessionsEndpoint>("/{mapId}/sessions");

    RouteGroupBuilder seasons = app.MapGroup("/seasons").WithTags("Seasons");
    seasons.Map<CreateSeasonEndpoint>();
    seasons.Map<CreateSessionEndpoint>("/{seasonId}/sessions");
    seasons.Map<GetSeasonsEndpoint>();
    seasons.Map<GetSeasonEndpoint>("/{seasonId}");
    seasons.Map<GetSeasonSessionsEndpoint>("/{seasonId}/sessions");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

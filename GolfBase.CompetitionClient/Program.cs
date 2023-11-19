// See https://aka.ms/new-console-template for more information

using GolfBase.CompetitionClient.Abstractions;
using GolfBase.CompetitionClient.Commands;
using GolfBase.CompetitionClient.Settings;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Spectre.Console.Cli;
using Spectre.Console.Cli.Extensions.DependencyInjection;

ServiceCollection services = new();

services.AddRefitClient<IGolfBaseApiClient>()
    .ConfigureHttpClient(client => { client.BaseAddress = new Uri("http://localhost:5045"); });
services.AddTransient<ISettingsStorage, LocalSettingsStorage>();

DependencyInjectionRegistrar registrar = new(services);

CommandApp app = new (registrar);

app.Configure(config =>
{
    config.AddCommand<SetupCommand>("setup");
    config.AddCommand<CompetetiveGameCommand>("compete");

    config.PropagateExceptions();
    config.ValidateExamples();
});

return await app.RunAsync(args);

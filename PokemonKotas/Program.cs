using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PokemonKotas.Domain.Interfaces;
using PokemonKotas.Web;
using PokemonKotas.Infra;
using PokemonKotas.Infra.Services;
using Microsoft.Extensions.Caching.Memory;
using PokemonKotas.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddPokemonClient()
    .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://beta.pokeapi.co/graphql/v1beta"));
builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7022/") });
builder.Services.AddSingleton<WebClientService>();
builder.Services.AddSingleton<IMemoryCache, MemoryCache>(x => new MemoryCache(new MemoryCacheOptions() { ExpirationScanFrequency = TimeSpan.FromMinutes(1) }));
builder.Services.AddSingleton<ICacheService<StrawberryShake.IOperationResult<IGetAllPokemonsResult>, IPokemonClient>,
    CacheService<StrawberryShake.IOperationResult<IGetAllPokemonsResult>, IPokemonClient>>();
builder.Services.AddSingleton<IPokemonService, PokemonService>();
builder.Services.AddScoped<SessionService>();

await builder.Build().RunAsync();

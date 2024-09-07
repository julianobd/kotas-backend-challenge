using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PokemonKotas.Domain.Interfaces;
using PokemonKotas.Web;
using PokemonKotas.Infra;
using PokemonKotas.Infra.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddPokemonClient()
    .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://beta.pokeapi.co/graphql/v1beta"));

builder.Services.AddSingleton<ICacheService<StrawberryShake.IOperationResult<IGetPokemonsResult>, IPokemonClient>,
    CacheService<StrawberryShake.IOperationResult<IGetPokemonsResult>, IPokemonClient>>();

await builder.Build().RunAsync();

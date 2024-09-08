using System.Net.Http.Json;

namespace PokemonKotas.Web.Services;

public class WebClientService(HttpClient client)
{
    public async Task<TOut?> GetAsync<TOut>(string url)
    {
        var result = await client.GetAsync(url).ConfigureAwait(false);
        if (result.IsSuccessStatusCode) return await result.Content.ReadFromJsonAsync<TOut>().ConfigureAwait(false);
        return default;
    }

    public async Task<TOut?> PutAsync<TIn, TOut>(string url, TIn obj)
    {
        var result = await client.PutAsJsonAsync(url, obj);
        if (result.IsSuccessStatusCode) return await result.Content.ReadFromJsonAsync<TOut>();
        return default;
    }

    public async Task<TOut?> PostAsync<TIn, TOut>(string url, TIn obj)
    {
        var x = client.BaseAddress;
        Console.WriteLine(client.BaseAddress);
        var result = await client.PostAsJsonAsync(url, obj);
        if (result.IsSuccessStatusCode) return await result.Content.ReadFromJsonAsync<TOut>();
        return default;
    }
}
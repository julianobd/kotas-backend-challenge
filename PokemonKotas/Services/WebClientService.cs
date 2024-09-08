using System.Net.Http.Json;
using PokemonKotas.Domain.Interfaces;

namespace PokemonKotas.Web.Services;

/// <summary>
///     Provides methods for making HTTP requests to a specified base address.
/// </summary>
/// <param name="client">The <see cref="HttpClient" /> used to send requests.</param>
public class WebClientService(HttpClient client) : IWebClientService
{
    /// <summary>
    ///     Sends an asynchronous GET request to the specified URL and deserializes the JSON response to the specified type.
    /// </summary>
    /// <typeparam name="TOut">The type to which the JSON response should be deserialized.</typeparam>
    /// <param name="url">The URL to which the GET request is sent.</param>
    /// <returns>
    ///     A task representing the asynchronous operation. The task result contains the deserialized response of type
    ///     <typeparamref name="TOut" /> if the request is successful; otherwise, the default value of
    ///     <typeparamref name="TOut" />.
    /// </returns>
    public async Task<TOut?> GetAsync<TOut>(string url)
    {
        var result = await client.GetAsync(url).ConfigureAwait(false);
        if (result.IsSuccessStatusCode) return await result.Content.ReadFromJsonAsync<TOut>().ConfigureAwait(false);
        return default;
    }

    /// <summary>
    ///     Sends a PUT request to the specified URL with the provided object and returns the response deserialized to the
    ///     specified type.
    /// </summary>
    /// <typeparam name="TIn">The type of the object to be sent in the PUT request.</typeparam>
    /// <typeparam name="TOut">The type to which the response content will be deserialized.</typeparam>
    /// <param name="url">The URL to which the PUT request is sent.</param>
    /// <param name="obj">The object to be sent in the PUT request.</param>
    /// <returns>
    ///     A task representing the asynchronous operation. The task result contains the deserialized response content, or
    ///     default if the request was not successful.
    /// </returns>
    public async Task<TOut?> PutAsync<TIn, TOut>(string url, TIn obj)
    {
        var result = await client.PutAsJsonAsync(url, obj);
        if (result.IsSuccessStatusCode) return await result.Content.ReadFromJsonAsync<TOut>();
        return default;
    }

    /// <summary>
    ///     Sends a POST request to the specified URL with the provided object as JSON content and returns the response
    ///     deserialized to the specified type.
    /// </summary>
    /// <typeparam name="TIn">The type of the object to be sent in the request body.</typeparam>
    /// <typeparam name="TOut">The type to which the response content should be deserialized.</typeparam>
    /// <param name="url">The URL to which the POST request is sent.</param>
    /// <param name="obj">The object to be sent in the request body.</param>
    /// <returns>
    ///     A task representing the asynchronous operation, with a result of the response content deserialized to the
    ///     specified type, or <c>null</c> if the request was not successful.
    /// </returns>
    public async Task<TOut?> PostAsync<TIn, TOut>(string url, TIn obj)
    {
        var x = client.BaseAddress;
        Console.WriteLine(client.BaseAddress);
        var result = await client.PostAsJsonAsync(url, obj);
        if (result.IsSuccessStatusCode) return await result.Content.ReadFromJsonAsync<TOut>();
        return default;
    }
}
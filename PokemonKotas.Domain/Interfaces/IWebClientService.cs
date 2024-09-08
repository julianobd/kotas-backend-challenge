namespace PokemonKotas.Domain.Interfaces;

/// <summary>
///     Defines methods for making HTTP requests to a specified URL and handling the responses.
/// </summary>
public interface IWebClientService
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
    Task<TOut?> GetAsync<TOut>(string url);

    /// <summary>
    ///     Sends an asynchronous PUT request to the specified URL with the provided object and deserializes the JSON response
    ///     to the specified type.
    /// </summary>
    /// <typeparam name="TIn">The type of the object to be sent in the PUT request.</typeparam>
    /// <typeparam name="TOut">The type to which the JSON response should be deserialized.</typeparam>
    /// <param name="url">The URL to which the PUT request is sent.</param>
    /// <param name="obj">The object to be sent in the PUT request.</param>
    /// <returns>
    ///     A task representing the asynchronous operation. The task result contains the deserialized response of type
    ///     <typeparamref name="TOut" /> if the request is successful; otherwise, the default value of
    ///     <typeparamref name="TOut" />.
    /// </returns>
    Task<TOut?> PutAsync<TIn, TOut>(string url, TIn obj);

    /// <summary>
    ///     Sends an asynchronous POST request to the specified URL with the provided object and deserializes the JSON response
    ///     to the specified type.
    /// </summary>
    /// <typeparam name="TIn">The type of the object to be sent in the POST request.</typeparam>
    /// <typeparam name="TOut">The type to which the JSON response should be deserialized.</typeparam>
    /// <param name="url">The URL to which the POST request is sent.</param>
    /// <param name="obj">The object to be sent in the POST request.</param>
    /// <returns>
    ///     A task representing the asynchronous operation. The task result contains the deserialized response of type
    ///     <typeparamref name="TOut" /> if the request is successful; otherwise, the default value of
    ///     <typeparamref name="TOut" />.
    /// </returns>
    Task<TOut?> PostAsync<TIn, TOut>(string url, TIn obj);
}
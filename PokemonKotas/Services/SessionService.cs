using Blazored.SessionStorage;
using PokemonKotas.Domain.Interfaces;

namespace PokemonKotas.Web.Services;

/// <summary>
///     Provides session management functionalities using the ISessionStorageService.
/// </summary>
/// <param name="sessionStorage">The session storage service used to manage session data.</param>
public class SessionService(ISessionStorageService sessionStorage) : ISessionService
{
    /// <summary>
    ///     Retrieves the Master ID from the session storage.
    /// </summary>
    /// <returns>
    ///     A task representing the asynchronous operation. The task result contains the Master ID if it exists in the session
    ///     storage; otherwise, null.
    /// </returns>
    public async Task<int?> GetMasterId()
    {
        var masterId = await sessionStorage.GetItemAsync<int?>("MasterId");
        return masterId;
    }

    /// <summary>
    ///     Stores the specified Master ID in the session storage.
    /// </summary>
    /// <param name="masterId">The Master ID to store in the session storage.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task SetMasterId(int masterId)
    {
        await sessionStorage.SetItemAsync("MasterId", masterId);
    }
}
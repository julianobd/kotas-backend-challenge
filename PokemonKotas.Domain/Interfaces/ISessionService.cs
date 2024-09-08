namespace PokemonKotas.Domain.Interfaces;

/// <summary>
///     Defines the contract for session management services.
/// </summary>
public interface ISessionService
{
    /// <summary>
    ///     Retrieves the Master ID from the session storage.
    /// </summary>
    /// <returns>
    ///     A task representing the asynchronous operation. The task result contains the Master ID if it exists in the session
    ///     storage; otherwise, null.
    /// </returns>
    Task<int?> GetMasterId();

    /// <summary>
    ///     Stores the specified Master ID in the session storage.
    /// </summary>
    /// <param name="masterId">The Master ID to store in the session storage.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SetMasterId(int masterId);
}
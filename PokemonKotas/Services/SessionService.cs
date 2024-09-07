using Blazored.SessionStorage;

namespace PokemonKotas.Web.Services
{
    public class SessionService(ISessionStorageService sessionStorage)
    {
        public async Task<int?> GetMasterId()
        {
            var masterId = await sessionStorage.GetItemAsync<int?>("MasterId");
            return masterId;
        }
        
        public async Task SetMasterId(int masterId)
        {
            await sessionStorage.SetItemAsync("MasterId", masterId);
        }
    }
}

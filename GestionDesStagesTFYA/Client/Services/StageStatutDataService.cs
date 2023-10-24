using GestionDesStagesTFYA.Client.Interfaces;
using GestionDesStagesTFYA.Server.Models;
using System.Text.Json;

namespace GestionDesStagesTFYA.Client.Services
{
    public class StageStatutDataService : IStageStatutDataService
    {
        private readonly HttpClient _httpClient;

        public StageStatutDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<StageStatut>> GetAllStageStatuts()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<StageStatut>>
                (await _httpClient.GetStreamAsync($"api/stagestatut"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}

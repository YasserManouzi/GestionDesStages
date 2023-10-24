using GestionDesStagesTFYA.Client.Interfaces;
using GestionDesStagesTFYA.Shared.Models;
using System.Text.Json;
using System.Text;

namespace GestionDesStagesTFYA.Client.Services
{
    public class StageDataService : IStageDataService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<StageDataService> _logger;

        public StageDataService(HttpClient httpClient, ILogger<StageDataService> logger)
        {
            _httpClient = httpClient;
            this._logger = logger;
        }
        public async Task<Stage> AddStage(Stage stage)
        {
            var donneesJson =
                new StringContent(JsonSerializer.Serialize(stage), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/stage", donneesJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Stage>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }
        public async Task<IEnumerable<Stage>> GetAllStages(string id = null)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return await JsonSerializer.DeserializeAsync<IEnumerable<Stage>>
                        (await _httpClient.GetStreamAsync("api/stage"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
                else
                {
                    return await JsonSerializer.DeserializeAsync<IEnumerable<Stage>>
                        (await _httpClient.GetStreamAsync($"api/stage/{id}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erreur dans l'obtention de données {ex}");
            }
            return null;
        }

        public async Task<Stage> GetStageByStageId(string StageId)
        {
            try
            {
                return await JsonSerializer.DeserializeAsync<Stage>
                    (await _httpClient.GetStreamAsync($"api/stage/GetStageByStageId/{StageId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erreur dans l'obtention de données d'un enregistrement {ex}");
            }
            return null;
        }

        public async Task UpdateStage(Stage stage)
        {
            var stageJson =
                new StringContent(JsonSerializer.Serialize(stage), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/stage", stageJson);
        }

    }

}

using Db1HealthPanelBack.Models.Requests;
using Db1HealthPanelBack.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Db1HealthPanelBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TechRadarController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public TechRadarController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]
        public async Task<IActionResult> GetRadarTechs()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://techradar.db1.com.br/db1-opinion.json");

            if (response.IsSuccessStatusCode)
            {
                string jsonContent = await response.Content.ReadAsStringAsync();
                var originalData = JsonConvert.DeserializeObject<TechRadarRequest>(jsonContent);
                var splitData = TechRadarService.Split(originalData);

                return Ok(splitData);
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }
    }
}

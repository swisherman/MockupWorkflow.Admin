
using MockupWorkflow.Admin.Web.Models;
using MockupWorkflow.Shared.Models;
using static System.Net.WebRequestMethods;

namespace MockupWorkflow.Admin.Web.Services
{
    public class RecordsApiClient
    {
        private readonly HttpClient _httpClient;

        public RecordsApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> GetHealthAsync()
        {
            return await _httpClient.GetStringAsync("/");
        }
        public async Task<ImportResult?> ImportAsync(
    IEnumerable<PodItem> items)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "/records/import",
                items);

            response.EnsureSuccessStatusCode();

            return await response.Content
                .ReadFromJsonAsync<ImportResult>();
        }
        public async Task<List<BatchSummary>> GetBatchesAsync()
        {
            
            return await _httpClient.GetFromJsonAsync<List<BatchSummary>>("/records/batches")
                   ?? new List<BatchSummary>();
        }
    }
}

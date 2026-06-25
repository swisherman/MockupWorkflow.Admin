
using MockupWorkflow.Shared.Models;
using MockupWorkflow.Admin.Web.Models;

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
    }
}

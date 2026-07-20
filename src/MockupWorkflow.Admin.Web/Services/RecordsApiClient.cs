
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
        public async Task<RetryFailedBatchResult?> RetryFailedBatchAsync(
    string batchId)
        {
            var response = await _httpClient.PostAsync(
                $"/records/batches/{Uri.EscapeDataString(batchId)}/retry-failed",
                content: null
            );

            response.EnsureSuccessStatusCode();

            return await response.Content
                .ReadFromJsonAsync<RetryFailedBatchResult>();
        }
        public async Task<List<BatchSummary>> GetBatchesAsync()
        {
            
            return await _httpClient.GetFromJsonAsync<List<BatchSummary>>("/records/batches")
                   ?? new List<BatchSummary>();
        }
        public async Task<List<PodItem>> GetBatchItemsAsync(string batchId, string? productType = null)
        {
            var url = string.IsNullOrWhiteSpace(productType)
                ? $"/records/batches/{batchId}"
                : $"/records/batches/{batchId}?productType={Uri.EscapeDataString(productType)}";

            return await _httpClient.GetFromJsonAsync<List<PodItem>>(url)
                   ?? new List<PodItem>();
        }
        public async Task ProcessBatchMockupsAsync(string batchId, string? productType = null)
        {
            var url = string.IsNullOrWhiteSpace(productType)
                ? $"/records/batches/{batchId}/process-mockups"
                : $"/records/batches/{batchId}/process-mockups?productType={Uri.EscapeDataString(productType)}";

            var response = await _httpClient.PostAsync(url, null);

            response.EnsureSuccessStatusCode();
        }
    }
}

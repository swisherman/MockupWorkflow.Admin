namespace MockupWorkflow.Admin.Web.Services
{
    public class FolderCreatorApiClient
    {
        private readonly HttpClient _http;

        public FolderCreatorApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task CreateFoldersAsync<T>(List<T> items)
        {
            var response = await _http.PostAsJsonAsync(
                "/folders/create",
                items);

            response.EnsureSuccessStatusCode();
        }
    }
}

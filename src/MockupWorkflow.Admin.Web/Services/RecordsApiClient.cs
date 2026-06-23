

namespace MockupWorkflow.Admin.Web.Services
{
    public class RecordsApiClient
    {
        private readonly HttpClient _httpClient;

        public RecordsApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}

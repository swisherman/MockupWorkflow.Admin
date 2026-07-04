using Microsoft.AspNetCore.Components.Forms;
using MockupWorkflow.Shared.Models;
using System.Net.Http.Headers;

namespace MockupWorkflow.Admin.Web.Services;

public class PngUploadService
{
    private readonly HttpClient _http;

    public PngUploadService(HttpClient http)
    {
        _http = http;
    }

    public async Task UploadInputFilesAsync(
    IEnumerable<IBrowserFile> files,
    IEnumerable<PodItem> items,
    string batchId,
    string productType)
    {
        var fileByName = files
         .GroupBy(f => f.Name, StringComparer.OrdinalIgnoreCase)
         .ToDictionary(
             g => g.Key,
             g => g.First(),
             StringComparer.OrdinalIgnoreCase
         );

        foreach (var item in items)
        {
            if (string.IsNullOrWhiteSpace(item.FolderName) ||
                string.IsNullOrWhiteSpace(item.Filename))
                continue;

            if (!fileByName.TryGetValue(item.Filename, out var file))
                throw new FileNotFoundException($"Selected upload files did not include {item.Filename}");

            var remotePath =
                $"{batchId}/{productType}/input_folders/{Uri.EscapeDataString(item.FolderName)}/{Uri.EscapeDataString(item.Filename)}";

            await UploadBrowserFileAsync(file, remotePath);
        }
    }

    private async Task UploadFileAsync(string localFilePath, string remotePath)
    {
        byte[] fileBytes = await File.ReadAllBytesAsync(localFilePath);

        using var content = new ByteArrayContent(fileBytes);
        content.Headers.ContentType = new MediaTypeHeaderValue("image/png");

        string url = $"/api/files/{remotePath}";

        using HttpResponseMessage response = await _http.PostAsync(url, content);

        if (!response.IsSuccessStatusCode)
        {
            string responseText = await response.Content.ReadAsStringAsync();

            throw new InvalidOperationException(
                $"PNG upload failed: {(int)response.StatusCode} {response.ReasonPhrase}\n{responseText}");
        }
    }
    private async Task UploadBrowserFileAsync(IBrowserFile file, string remotePath)
    {
        await using var stream = file.OpenReadStream(maxAllowedSize: 50 * 1024 * 1024);

        using var content = new StreamContent(stream);
        content.Headers.ContentType =
            new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");

        var response = await _http.PostAsync($"/api/files/{remotePath}", content);

        if (!response.IsSuccessStatusCode)
        {
            var responseText = await response.Content.ReadAsStringAsync();
            throw new InvalidOperationException(
                $"PNG upload failed for {file.Name}: {(int)response.StatusCode} {response.ReasonPhrase}\n{responseText}");
        }
    }
}
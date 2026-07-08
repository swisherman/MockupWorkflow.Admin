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
    public async Task UploadInputFoldersAsync(
    string inputFoldersRoot,
    string batchId,
    string productType)
    {
        if (!Directory.Exists(inputFoldersRoot))
            throw new DirectoryNotFoundException(inputFoldersRoot);

        var files = Directory.GetFiles(
            inputFoldersRoot,
            "*.png",
            SearchOption.AllDirectories);

        foreach (var file in files)
        {
            var folderName = Path.GetFileName(Path.GetDirectoryName(file));
            var fileName = Path.GetFileName(file);

            var remotePath =
                $"{batchId}/{productType}/input_folders/{Uri.EscapeDataString(folderName)}/{Uri.EscapeDataString(fileName)}";

            await UploadFileAsync(file, remotePath);
        }
    }
}
using Microsoft.AspNetCore.SignalR;
using MockupWorkflow.Admin.Web.Components;
using MockupWorkflow.Admin.Web.Services;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<HubOptions>(options =>
{
    options.MaximumReceiveMessageSize = 1024 * 1024;
});

builder.Services.AddAntiforgery(options =>
{
    options.Cookie.Name = "MockupWorkflow.Admin.Antiforgery";
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = CookieSecurePolicy.None;
});
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


// Add MudBlazor services
builder.Services.AddMudServices();

// Register TimeProvider
builder.Services.AddSingleton(TimeProvider.System);

builder.Services.AddHttpClient<RecordsApiClient>((sp, client) =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();

    client.BaseAddress = new Uri(
        configuration["WorkflowApi:BaseUrl"]!);
});
builder.Services.AddHttpClient<PngUploadService>((sp, client) =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    client.BaseAddress = new Uri(configuration["PngApi:BaseUrl"]!);
});

builder.Services.AddHttpClient<FolderCreatorApiClient>((sp, client) =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();

    client.BaseAddress = new Uri(
        configuration["FolderCreatorApi:BaseUrl"]!);
});



var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found");




//app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

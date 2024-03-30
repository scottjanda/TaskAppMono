using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskAppMono.Data;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    options.FallbackPolicy = options.DefaultPolicy;
});
builder.Services.AddRazorPages()
    .AddMicrosoftIdentityUI();

if (builder.Environment.IsProduction())
{
    var KeyVaultURL = builder.Configuration.GetSection("KeyVault:KeyVaultURL");
    var KeyVaultClientId = builder.Configuration.GetSection("KeyVault:ClientId");
    var KeyVaultClientSecret = builder.Configuration.GetSection("KeyVault:ClientSecret");
    var KeyVaultDirectoryID = builder.Configuration.GetSection("KeyVault:DirectoryID");

    var credential = new ClientSecretCredential(KeyVaultDirectoryID.Value!.ToString(), KeyVaultClientId.Value!.ToString(), KeyVaultClientSecret.Value!.ToString());

    var client = new SecretClient(new Uri(KeyVaultURL.Value!.ToString()), credential);
}

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<TaskAppMonoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'TaskAppMonoContext' not found.")));
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();

using BlazorOpenBank.Data;
using BlazorOpenBank.Data.Services;
using Igor_AIS_Proj.Business.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using BlazorOpenBank.Helpers;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

var baseAddress = builder.Configuration.GetValue<string>("apiUrl");
//builder.Services.AddHttpClient();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddLocalization();
//builder.Services.AddTransient<UserService>();
builder.Services.AddScoped(sp => new HttpClient {  BaseAddress = new Uri(baseAddress) });
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
builder.Services.AddScoped<IAccountService, AccountService>();
   builder.Services
    .AddBlazorise(options =>
    {
    options.Immediate = true;
} )
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();
builder.Services.AddControllers();


var app = builder.Build();

var supportedCultures = new[] { "en-US", "es-ES" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);
app.MapControllers();
//app.MapControllers();
//app.MapBlazorHub();
//app.MapFallbackToPage("/_Host");

//await app.RunAsync();


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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.UseStaticFiles();
app.Run();

using BlazorOpenBank.Data;
using BlazorOpenBank.Data.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;


var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddHttpClient();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
//builder.Services.AddTransient<UserService>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7271/") });
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
builder.Services.AddScoped<IAlertService, AlertService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IHttpService, HttpService>();
//builder.Services.AddScoped(x =>
//{
//    var apiUrl = new Uri(builder.Configuration["apiUrl"]);
//    return new HttpClient() { BaseAddress = apiUrl };
//});
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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

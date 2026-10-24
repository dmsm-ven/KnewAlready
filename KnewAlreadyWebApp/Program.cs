using KnewAlreadyWebApp.Data;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

#if DEBUG
//Локальная запущенная версия
string apiHost = "https://localhost:7052/";
#else
//Хост хонтейнер docker в сети на сервере
string apiHost = "http://knewalready-api/";
#endif


builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddSingleton<JwtTokenValidator>();

builder.Services.AddScoped<HttpClient>(x => new HttpClient(new HttpClientHandler()));
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

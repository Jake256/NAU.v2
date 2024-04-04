using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Configuration;
using MudBlazor.Services;
using NAUIdeaHub.Configuration;
using NAUIdeaHub.Repositories;
using NAUIdeaHub.Services;
using NAUIdeaHub.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddScoped<IIdeaHubRepository, IdeaHubRepository>();
builder.Services.AddScoped<IIdeaHubService, IdeaHubService>();
builder.Services.AddScoped<ILoggedUserService, LoggedUserService>();
builder.Services.AddScoped<IEmailService, EmailService>();

IConfiguration configuration = builder.Configuration;
var connectionStrings = new ConnectionStringsConfig();
configuration.GetSection("ConnectionStrings").Bind(connectionStrings);
Console.WriteLine(connectionStrings.DefaultConnection);
builder.Services.AddSingleton(connectionStrings);

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
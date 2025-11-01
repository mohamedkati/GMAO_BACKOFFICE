using GMAO.API.Extensions;
using GMAO.Infrastructure.DIHelpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GMAO.Application.DIExtensions;
using GMAO.Application.Common.AppSettings;
using GMAO.Infrastructure.Persistance.Seed;
using GMAO.Application.Common.Behaviours;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
builder.Services.Configure<EmailSetting>(builder.Configuration.GetSection("MailSettings"));
builder.Services.Configure<AppSetting>(builder.Configuration.GetSection(nameof(AppSetting)));
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));
builder.Services.AddHttpContextAccessor();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.ConfigureSwagger();

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});
builder.Services.ConfigureInfraServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration).AddDefaultTokenProviders();
builder.Services.ConfigureAppAuthenticationServices();
builder.Services.RegisterAuthentication(builder.Configuration);
builder.Services.AddApplicationLayer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapSwagger();
}

await DbInitializer.SeedAsync(app.Services);

app.UseMiddleware<ExceptionHandlerMiddelware>();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

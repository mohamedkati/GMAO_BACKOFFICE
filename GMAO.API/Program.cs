using GMAO.API.Configurations;
using GMAO.API.Extensions;
using GMAO.Application.Common.AppSettings;
using GMAO.Application.Common.Behaviours;
using GMAO.Application.DIExtensions;
using GMAO.Infrastructure.DIHelpers;
using GMAO.Infrastructure.Persistance.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers(cnf =>
{
    cnf.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
    cnf.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});
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

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:3000/login")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials()
                .SetIsOriginAllowed(origin => true) // ? Ajoutez cette ligne
              .WithExposedHeaders("*"); ;
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapSwagger();
}
await DbInitializer.SeedAsync(app.Services);

app.UseCors("CorsPolicy");

app.UseMiddleware<ExceptionHandlerMiddelware>();
app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

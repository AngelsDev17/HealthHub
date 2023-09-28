
// Builder
using Angels.Packages.JwtToken.ExtensionMethods;
using Angels.Packages.Logger.Enums;
using Angels.Packages.Logger.ExtensionMethods;
using HealthHub.BusinessLogic.ExtensionMethods;
using HealthHub.Persistence.ExtensionMethods;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBusinessLogicLayerExtensions();
builder.Services.AddPersistenceLayerExtensions(configuration: builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        name: "auth-page",
        info: new OpenApiInfo { Title = "PORTAL DE USUARIOS", Version = "v1" });

    c.SwaggerDoc(
        name: "management-page",
        info: new OpenApiInfo { Title = "ADMINISTRACIÓN DE CLIENTES Y PROFESIONALES", Version = "v1" });

    c.SwaggerDoc(
        name: "profile-page",
        info: new OpenApiInfo { Title = "ADMINISTRACIÓN DEL PERFIL", Version = "v1" });

    c.ExampleFilters();
});

builder.Services.AddSwaggerExamplesFromAssemblies(assemblies: Assembly.GetEntryAssembly());
builder.Services.AddJwtTokenExtensions(configuration: builder.Configuration);

builder.Host.AddLoggingExtensions("HealtHub");


// App
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint(
            url: "/swagger/auth-page/swagger.json",
            name: "Portal de usuarios");

        c.SwaggerEndpoint(
            url: "/swagger/management-page/swagger.json",
            name: "Administración de clientes y profesionales");

        c.SwaggerEndpoint(
            url: "/swagger/profile-page/swagger.json",
            name: "Administración del perfil");
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

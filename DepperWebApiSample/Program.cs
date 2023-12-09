using DapperConsoleSample.Context;
using DapperConsoleSample.Contracts;
using DapperConsoleSample.Repository;
using DepperWebApiSample.Contracts;
using DepperWebApiSample.Repository;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setupAction =>
{
    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

    //Aplica los comentarios C# de las API's ( con ///)
    //Propiedades del Proyecto -> Build -> Output: Marcar casilla "Documentation File", dejar nombre por defecto. 
    setupAction.IncludeXmlComments(xmlCommentsFullPath);
    setupAction.SwaggerDoc("v1", new OpenApiInfo{
                    Version = "v1",
                    Title = "Dapper Web Api example",
                    Description = "This is an ASP.NET Core Web API example for Dapper usage and Swagger Documentation."
                }
            );

    //setupAction.AddSecurityDefinition("Dapper Web Api example Security", new OpenApiSecurityScheme()
    //{
    //	Type = SecuritySchemeType.Http,
    //	Scheme = "Bearer";
    //	Description = "Input a valid token to access this API"
    //});
});

builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseAuthorization();

app.MapControllers();

app.Run();

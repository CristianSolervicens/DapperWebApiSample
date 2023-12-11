using DapperConsoleSample.Context;
using DapperConsoleSample.Contracts;
using DapperWebApiSample.Contracts;
using DapperWebApiSample.Repository;
using DepperConsoleSample.Repository;
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

////Controlar "media type", (sólo aceptará json, si se pide application/xml dará 406 not acceptable)
//builder.Services.AddControllers(configure =>
//        {
//            configure.ReturnHttpNotAcceptable = true;
//        }
//    );

//Controlar "media type", (sólo aceptará json, si se pide application/xml dará 406 not acceptable)
//Si quiero agregar soporte XML reemplazo la anterior por esta...
builder.Services.AddControllers(configure =>
        {
            configure.ReturnHttpNotAcceptable = true;
        }
    ).AddXmlDataContractSerializerFormatters();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    //No desplegar el Stack de Error cuando es Producción
    //OJO hay que definirlo en las Propiedades de Pruyecto
    app.UseExceptionHandler(appBuilder =>
    {
        appBuilder.Run(async context =>
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync(
                "An unexpected fault happened. Try again later.");
        });
    });
}

//app.UseAuthorization();

app.MapControllers();

app.Run();

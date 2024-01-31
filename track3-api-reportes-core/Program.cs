using Application.interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using track3_api_reportes_core.Aplicacion.Interfaces;
using track3_api_reportes_core.Aplicacion.Main;
using track3_api_reportes_core.Aplicacion.Reportes;
using track3_api_reportes_core.Infraestructura.Dao;
using track3_api_reportes_core.Infraestructura.Interfaces;
using track3_api_reportes_core.Infraestructura.Repositorios;
using track3_api_reportes_core.Middleware.Dto;
using track3_api_reportes_core.Middleware.Interfaces;
using track3_api_reportes_core.Middleware.ReportesMiddleware;
using track3_api_reportes_core.Middleware.secureway;

var builder = WebApplication.CreateBuilder(args);

//Para SQL
var provider = builder.Services.BuildServiceProvider();
Infraestructure.Infraestructura._configuration = provider.GetRequiredService<IConfiguration>();

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
                .AddJwtBearer(option =>
                {
                    option.RequireHttpsMetadata = false;
                    option.SaveToken = true;
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Clave.ClaveSecreta))
                    };
                    option.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            Microsoft.Extensions.Primitives.StringValues accessToken = context.Request.Query["access_token"];

                            PathString path = context.HttpContext.Request.Path;

                            return Task.CompletedTask;
                        }
                    };
                });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
var version = "0.0.12";
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = version,
        Title = "Track3 Reportes .Net Core"
    });
    OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter JWT Bearer TOKEN",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                                        {
                                            {securityScheme, new string[] { }}
                                        });
});


builder.Services.AddScoped<IOracleDao, OracleDao>();
builder.Services.AddScoped<ISqlDao, SqlDao>();
builder.Services.AddScoped<IReportes, Reportes>();
builder.Services.AddScoped<IReportesMiddleware, ReportesMiddleware>();
builder.Services.AddScoped<IDaoReportes, DaoReportes>();
builder.Services.AddScoped<ISqlDaoReportes, SqlDaoReportes>();
builder.Services.AddScoped<ISecurewayRepository, SecurewayRepository>();

builder.Services.AddScoped<IRemitosApplication, RemitoApplication>();
builder.Services.AddScoped<IDispatchApplication, DispatchApplication>();
builder.Services.AddScoped<IRemitosMiddleware, RemitosMiddleware>();
builder.Services.AddScoped<IRemitosRepository, RemitosRepository>();
builder.Services.AddScoped<IServiceHelper,Application.Application>();

builder.Services.AddScoped<IMaestros, Maestros>();
builder.Services.AddScoped<IMaestrosMiddleware, MaestrosMiddleware>();
builder.Services.AddScoped<ISecureWayMiddleware, SecureWayMiddleware>();
builder.Services.AddScoped<IDaoMaestros, DaoMaestros>();


builder.Services.AddScoped<IServicios,AppServicios>();
builder.Services.AddScoped<IServiciosMiddleware,ServiciosMiddleware>();
builder.Services.AddScoped<IHojaRutaRepository,HojaRutaRepository>();
builder.Services.AddScoped<IMappingFormularios, ProfileForm>();

builder.Services.AddScoped<IPedidosRepositorio, PedidosRepositorio>();

builder.Services.AddCors(o => o.AddPolicy("CorsAll", x => x.AllowAnyHeader()
                                                           .AllowAnyMethod()
                                                           .AllowCredentials()
                                                           .SetIsOriginAllowed(isOriginAllowed: _ => true)
                                                           .AllowCredentials()
                                          ));

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();



//  app.UseSwaggerUI();
app.UseSwaggerUI(c =>
{
    c.InjectStylesheet("/swagger-ui/custom.css");
    c.SwaggerEndpoint("/swagger/" + "v1" + "/swagger.json", "Track3-api-reportes-core" + version);
});



//}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("CorsAll");

app.UseAuthorization();

app.MapControllers();

app.Run();

public static class Clave
{
    public static string ClaveSecreta { get; } = "clave-secreta-api";
}


using API.Domain.IRepositories;
using API.Domain.IServices;
using API.Domain.Models;
using API.Domain.Models.Esquema;
using API.Persistence.Context;
using API.Persistence.Repositories;
using API.Services;
using API.Mappings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var das = 1;
var builder = WebApplication.CreateBuilder(args);
WebApplication app;
if (das == 1)
{
    // Configuración de DbContext
    builder.Services.AddDbContext<AplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("Conexion")));

    // AutoMapper
    builder.Services.AddAutoMapper(typeof(MappingProfile));

    // Servicios
    builder.Services.AddScoped<IUsuarioService, UsuarioService>();
    builder.Services.AddScoped<ILoginService, LoginService>();
    builder.Services.AddScoped<IVacunaService, VacunaService>();
    builder.Services.AddScoped<IJeringaService, JeringaService>();
    builder.Services.AddScoped<IDiluyenteService, DiluyenteService>();
    builder.Services.AddScoped<IMadreService, MadreService>();
    builder.Services.AddScoped<ICuidadorService, CuidadorService>();
    builder.Services.AddScoped<IAntecedenteService, AntecedenteService>();
    builder.Services.AddScoped<ICondicionUsuariaService, CondicionUsuariaService>();
    builder.Services.AddScoped<IAntecedentesMedicosService, AntecedentesMedicosService>();
    builder.Services.AddScoped<IEsquemaVacunacionService, EsquemaVacunacionService>();
    builder.Services.AddScoped<IPacienteService, PacienteService>();
    builder.Services.AddScoped<IAlarmaVacunacionService, AlarmaVacunacionService>();
    builder.Services.AddScoped<IStatisticsService, StatisticsService>();
    builder.Services.AddScoped<IStatisticsService, StatisticsService>();
    builder.Services.AddMemoryCache();

    // Repositorios
    builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
    builder.Services.AddScoped<ILoginRepository, LoginRepository>();
    builder.Services.AddScoped<IVacunaRepository, VacunaRepository>();
    builder.Services.AddScoped<IJeringaRepository, JeringaRepository>();
    builder.Services.AddScoped<IDiluyenteRepository, DiluyenteRepository>();
    builder.Services.AddScoped<IMadreRepository, MadreRepository>();
    builder.Services.AddScoped<ICuidadorRepository, CuidadorRepository>();
    builder.Services.AddScoped<IAntecedenteRepository, AntecedenteRepository>();
    builder.Services.AddScoped<ICondicionUsuariaRepository, CondicionUsuariaRepository>();
    builder.Services.AddScoped<IAntecedentesMedicosRepository, AntecedentesMedicosRepository>();
    builder.Services.AddScoped<IEsquemaVacunacionRepository, EsquemaVacunacionRepository>();
    builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
    builder.Services.AddScoped<IAlarmaVacunacionRepository, AlarmaVacunacionRepository>();

    builder.Services.AddScoped<IRepository<Vacuna>, VacunaRepository>();
    builder.Services.AddScoped<IRepository<Jeringa>, JeringaRepository>();
    builder.Services.AddScoped<IRepository<Diluyente>, DiluyenteRepository>();
    builder.Services.AddScoped<IRepository<Madre>, MadreRepository>();
    builder.Services.AddScoped<IRepository<Cuidador>, CuidadorRepository>();
    builder.Services.AddScoped<IRepository<Antecedente>, AntecedenteRepository>();
    builder.Services.AddScoped<IRepository<CondicionUsuaria>, CondicionUsuariaRepository>();
    builder.Services.AddScoped<IRepository<AntecedentesMedicos>, AntecedentesMedicosRepository>();
    builder.Services.AddScoped<IRepository<EsquemaVacunacion>, EsquemaVacunacionRepository>();
    builder.Services.AddScoped<IRepository<Paciente>, PacienteRepository>();
    builder.Services.AddScoped<IRepository<AlarmaVacunacion>, AlarmaVacunacionRepository>();

    builder.Services.AddScoped<IBaseService<Vacuna>, VacunaService>();
    builder.Services.AddScoped<IBaseService<Jeringa>, JeringaService>();
    builder.Services.AddScoped<IBaseService<Diluyente>, DiluyenteService>();
    builder.Services.AddScoped<IBaseService<Madre>, MadreService>();
    builder.Services.AddScoped<IBaseService<Cuidador>, CuidadorService>();
    builder.Services.AddScoped<IBaseService<Antecedente>, AntecedenteService>();
    builder.Services.AddScoped<IBaseService<CondicionUsuaria>, CondicionUsuariaService>();
    builder.Services.AddScoped<IBaseService<AntecedentesMedicos>, AntecedentesMedicosService>();
    builder.Services.AddScoped<IBaseService<Paciente>, PacienteService>();

    // Caching para endpoints de estadísticas
    builder.Services.AddMemoryCache();

    // Configuración de CORS
    builder.Services.AddCors(options =>
        options.AddPolicy("AllowWebapp", builder =>
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod()));

    // Configuración de autenticación JWT
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])),
                ClockSkew = TimeSpan.Zero
            };
        });

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("ADMINISTRADOR"));
    });

    // Configuración de controladores y serialización JSON
    builder.Services.AddControllers()
        .AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
        .AddJsonOptions(o =>
        {
            // Conversores para formatear fechas DateTime como yyyy-MM-dd (solo fecha)
            o.JsonSerializerOptions.Converters.Add(new Utils.Json.DateOnlyConverter());
            o.JsonSerializerOptions.Converters.Add(new Utils.Json.NullableDateOnlyConverter());
        });

    // Configuración de Swagger
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "API Documentation",
            Description = "Documentación de la API usando Swagger en .NET 6.0 o superior"
        });
    });

    app = builder.Build();

    // Configuración de entorno
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error"); // Página de error genérica en producción
        app.UseHsts(); // Añade cabecera Strict-Transport-Security
    }

    // Redireccionamiento a HTTPS solo si hay puerto HTTPS configurado
    var httpsPortEnv = Environment.GetEnvironmentVariable("ASPNETCORE_HTTPS_PORT");
    if (!string.IsNullOrEmpty(httpsPortEnv))
    {
        app.UseHttpsRedirection();
    }

    // Configuración de Swagger para todos los entornos
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        c.RoutePrefix = string.Empty; // Hace que Swagger sea la página principal
    });

    // Configuración de CORS
    app.UseCors("AllowWebapp");

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    // Seeding de base de datos (solo primera vez / ambiente dev)
    using (var scope = app.Services.CreateScope())
    {
        var ctx = scope.ServiceProvider.GetRequiredService<AplicationDbContext>();
        await API.Services.DatabaseSeeder.SeedAsync(ctx);
    }

    app.Run();
}
else
{
    // Add services to the container.

    // Configuración de DbContext
    builder.Services.AddDbContext<AplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("Conexion")));

    // Configuración de CORS
    builder.Services.AddCors(options =>
        options.AddPolicy("AllowWebapp", builder =>
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod()));

    // Configuración de autenticación JWT
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])),
                ClockSkew = TimeSpan.Zero
            });

    // Configuración de controladores y serialización JSON
    builder.Services.AddControllers().AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

    // Servicios y caché para estadísticas (modo alterno)
    builder.Services.AddScoped<IStatisticsService, StatisticsService>();
    builder.Services.AddMemoryCache();

    // Configuración de Swagger
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "API Documentation",
            Description = "Documentación de la API usando Swagger en .NET 6.0 o superior"
        });
    });

    app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage(); // Muestra la página de errores solo en desarrollo
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
            c.RoutePrefix = string.Empty; // Hace que Swagger sea la página principal
        });
    }

    app.UseCors("AllowWebapp");

    var httpsPortEnv2 = Environment.GetEnvironmentVariable("ASPNETCORE_HTTPS_PORT");
    if (!string.IsNullOrEmpty(httpsPortEnv2))
    {
        app.UseHttpsRedirection();
    }

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}

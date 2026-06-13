using Admision.API.Configuracion;
using Admision.API.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ---------- Servicios ----------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Persistencia y dependencias del microservicio
builder.Services.AgregarPersistencia(builder.Configuration);
builder.Services.AgregarDependenciasDeAplicacion();

// JWT
var configuracionJwt = builder.Configuration.GetSection("Jwt");
var claveSecreta = Encoding.UTF8.GetBytes(configuracionJwt["Key"]!);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opciones =>
    {
        opciones.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuracionJwt["Emisor"],
            ValidAudience = configuracionJwt["Audiencia"],
            IssuerSigningKey = new SymmetricSecurityKey(claveSecreta)
        };
    });

builder.Services.AddAuthorization();

// Swagger con soporte JWT
builder.Services.AddSwaggerGen(opciones =>
{
    opciones.SwaggerDoc("v1", new OpenApiInfo { Title = "Admision API", Version = "v1" });

    opciones.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese el token JWT con el formato: Bearer {token}"
    });

    opciones.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

var app = builder.Build();

// ---------- Pipeline ----------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UsarManejadorGlobalDeExcepciones();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
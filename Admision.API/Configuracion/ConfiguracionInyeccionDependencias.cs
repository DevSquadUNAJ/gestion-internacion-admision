using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Admision.Aplicacion.CasosDeUso;
using Admision.Aplicacion.Interfaces.ICasosDeUso;
using Admision.Aplicacion.Interfaces.IComandos;
using Admision.Aplicacion.Interfaces.IConsultas;
using Admision.Infraestructura.Comandos;
using Admision.Infraestructura.Consultas;
using Admision.Infraestructura.Persistencia;

namespace Admision.API.Configuracion
{
    public static class ConfiguracionInyeccionDependencias
    {
        public static IServiceCollection AgregarDependenciasDeAplicacion(this IServiceCollection servicios)
        {
            // Casos de uso
            servicios.AddScoped<IRegistrarInternacionCasoDeUso, RegistrarInternacionCasoDeUso>();

            // Consultas
            servicios.AddScoped<IPacienteConsulta, PacienteConsulta>();
            servicios.AddScoped<IInternacionConsulta, InternacionConsulta>();
            servicios.AddScoped<ICamaConsulta, CamaConsulta>();

            // Comandos
            servicios.AddScoped<IInternacionComando, InternacionComando>();
            servicios.AddScoped<ICamaComando, CamaComando>();

            return servicios;
        }

        public static IServiceCollection AgregarPersistencia(this IServiceCollection servicios, IConfiguration configuracion)
        {
            servicios.AddDbContext<ContextoBaseDeDatos>(opciones =>
                opciones.UseSqlServer(configuracion.GetConnectionString("AdmisionDb")));

            return servicios;
        }
    }
}
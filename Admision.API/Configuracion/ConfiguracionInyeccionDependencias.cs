using Admision.Aplicacion.CasosDeUso;
using Admision.Aplicacion.Interfaces.ICasosDeUso;
using Admision.Aplicacion.Interfaces.IComandos;
using Admision.Aplicacion.Interfaces.IConsultas;
using Admision.Aplicacion.Interfaces.IMapeadores;
using Admision.Aplicacion.Mapeadores;
using Admision.Infraestructura.Comandos;
using Admision.Infraestructura.Consultas;
using Admision.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Admision.API.Configuracion
{
    public static class ConfiguracionInyeccionDependencias
    {
        public static IServiceCollection AgregarDependenciasDeAplicacion(this IServiceCollection servicios)
        {
            // Casos de uso
            servicios.AddScoped<IRegistrarInternacionCasoDeUso, RegistrarInternacionCasoDeUso>();
            servicios.AddScoped<ICambiarEstadoCamaCasoDeUso, CambiarEstadoCamaCasoDeUso>();
            servicios.AddScoped<ITrasladarPacienteInternadoCasoDeUso, TrasladarPacienteInternadoCasoDeUso>();
            servicios.AddScoped<IObtenerSectoresCasoDeUso, ObtenerSectoresCasoDeUso>();
            servicios.AddScoped<IObtenerCamasPorSectorCasoDeUso, ObtenerCamasPorSectorCasoDeUso>();

            // Consultas
            servicios.AddScoped<IPacienteConsulta, PacienteConsulta>();
            servicios.AddScoped<IInternacionConsulta, InternacionConsulta>();
            servicios.AddScoped<ICamaConsulta, CamaConsulta>();
            servicios.AddScoped<ISectorConsulta, SectorConsulta>();

            // Comandos
            servicios.AddScoped<IInternacionComando, InternacionComando>();
            servicios.AddScoped<ICamaComando, CamaComando>();

            // Mapeadores
            servicios.AddSingleton<IRegistrarInternacionMapeador, RegistrarInternacionMapeador>();
            servicios.AddSingleton<ICambiarEstadoCamaMapeador, CambiarEstadoCamaMapeador>();
            servicios.AddSingleton<ITrasladarPacienteInternadoMapeador, TrasladarPacienteInternadoMapeador>();
            servicios.AddSingleton<ISectorMapeador, SectorMapeador>();
            servicios.AddSingleton<ICamaMapeador, CamaMapeador>();

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
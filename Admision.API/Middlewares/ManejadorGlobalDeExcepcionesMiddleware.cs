using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Admision.Aplicacion.Excepciones;

namespace Admision.API.Middlewares
{
    public class ManejadorGlobalDeExcepcionesMiddleware
    {
        private readonly RequestDelegate _siguiente;
        private readonly ILogger<ManejadorGlobalDeExcepcionesMiddleware> _logger;

        public ManejadorGlobalDeExcepcionesMiddleware(
            RequestDelegate siguiente,
            ILogger<ManejadorGlobalDeExcepcionesMiddleware> logger)
        {
            _siguiente = siguiente;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext contexto)
        {
            try
            {
                await _siguiente(contexto);
            }
            catch (Exception ex)
            {
                await ManejarExcepcionAsync(contexto, ex);
            }
        }

        private async Task ManejarExcepcionAsync(HttpContext contexto, Exception excepcion)
        {
            var (codigoHttp, tipoError) = excepcion switch
            {
                PacienteNoEncontradoException => (HttpStatusCode.NotFound, "PacienteNoEncontrado"),
                CamaNoEncontradaException => (HttpStatusCode.NotFound, "CamaNoEncontrada"),
                InternacionNoEncontradaException => (HttpStatusCode.NotFound, "InternacionNoEncontrada"),
                PacienteYaInternadoException => (HttpStatusCode.Conflict, "PacienteYaInternado"),
                CamaNoDisponibleException => (HttpStatusCode.Conflict, "CamaNoDisponible"),
                CambioEstadoCamaNoPermitidoException => (HttpStatusCode.Conflict, "CambioEstadoCamaNoPermitido"),
                InternacionNoActivaException => (HttpStatusCode.Conflict, "InternacionNoActiva"),
                InternacionSinCamaAsignadaException => (HttpStatusCode.Conflict, "InternacionSinCamaAsignada"),
                TrasladoAMismaCamaException => (HttpStatusCode.Conflict, "TrasladoAMismaCama"),
                EstadoCamaInvalidoException => (HttpStatusCode.BadRequest, "EstadoCamaInvalido"),
                EstadoEgresoInvalidoException => (HttpStatusCode.BadRequest, "EstadoEgresoInvalido"),
                ExcepcionDeNegocio => (HttpStatusCode.BadRequest, "ErrorDeNegocio"),
                _ => (HttpStatusCode.InternalServerError, "ErrorInterno")
            };

            if (codigoHttp == HttpStatusCode.InternalServerError)
                _logger.LogError(excepcion, "Error no controlado: {Mensaje}", excepcion.Message);
            else
                _logger.LogWarning("Excepcion de negocio: {Tipo} - {Mensaje}", tipoError, excepcion.Message);

            var respuesta = new
            {
                title = tipoError,
                detail = codigoHttp == HttpStatusCode.InternalServerError
                    ? "Ocurrio un error interno en el servidor."
                    : excepcion.Message
            };

            contexto.Response.ContentType = "application/json";
            contexto.Response.StatusCode = (int)codigoHttp;
            await contexto.Response.WriteAsync(JsonSerializer.Serialize(respuesta));
        }
    }
}
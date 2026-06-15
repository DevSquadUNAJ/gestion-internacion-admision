using System;
using System.Threading.Tasks;
using Admision.Aplicacion.DTOs.Solicitudes;
using Admision.Aplicacion.DTOs.Respuestas;
using Admision.Aplicacion.Excepciones;
using Admision.Aplicacion.Interfaces.ICasosDeUso;
using Admision.Aplicacion.Interfaces.IComandos;
using Admision.Aplicacion.Interfaces.IConsultas;
using Admision.Dominio.Constantes;
using Admision.Dominio.Entidades;

namespace Admision.Aplicacion.CasosDeUso
{
    public class TrasladarPacienteInternadoCasoDeUso : ITrasladarPacienteInternadoCasoDeUso
    {
        private readonly IInternacionConsulta _internacionConsulta;
        private readonly ICamaConsulta _camaConsulta;
        private readonly IInternacionComando _internacionComando;
        private readonly ICamaComando _camaComando;

        public TrasladarPacienteInternadoCasoDeUso(
            IInternacionConsulta internacionConsulta,
            ICamaConsulta camaConsulta,
            IInternacionComando internacionComando,
            ICamaComando camaComando)
        {
            _internacionConsulta = internacionConsulta;
            _camaConsulta = camaConsulta;
            _internacionComando = internacionComando;
            _camaComando = camaComando;
        }

        public async Task<TrasladarPacienteInternadoRespuesta> EjecutarAsync(TrasladarPacienteInternadoSolicitud solicitud)
        {
            // 1. Validar que la internacion exista.
            var internacion = await _internacionConsulta.ObtenerPorIdAsync(solicitud.InternacionId);
            if (internacion is null)
                throw new InternacionNoEncontradaException(solicitud.InternacionId);

            // 2. Validar que la internacion este activa.
            if (internacion.Estado != EstadoInternacion.Activa)
                throw new InternacionNoActivaException(solicitud.InternacionId);

            // 3. Obtener la asignacion actual de cama.
            var asignacionActual = await _internacionConsulta.ObtenerAsignacionActualAsync(solicitud.InternacionId);
            if (asignacionActual is null)
                throw new InternacionSinCamaAsignadaException(solicitud.InternacionId);

            // 4. Validar que la cama destino no sea la misma que la actual.
            if (asignacionActual.CamaId == solicitud.CamaDestinoId)
                throw new TrasladoAMismaCamaException(solicitud.CamaDestinoId);

            // 5. Validar cama destino: existe y esta disponible.
            var camaDestino = await _camaConsulta.ObtenerPorIdAsync(solicitud.CamaDestinoId);
            if (camaDestino is null)
                throw new CamaNoEncontradaException(solicitud.CamaDestinoId);

            if (camaDestino.Estado != EstadoCama.Disponible)
                throw new CamaNoDisponibleException(camaDestino.Id, camaDestino.Estado);

            // 6. Obtener la cama anterior para liberarla.
            var camaAnterior = await _camaConsulta.ObtenerPorIdAsync(asignacionActual.CamaId);
            if (camaAnterior is null)
                throw new CamaNoEncontradaException(asignacionActual.CamaId);

            var fechaTraslado = DateTime.UtcNow;

            // 7. Cerrar la asignacion anterior.
            asignacionActual.EsActual = false;
            asignacionActual.FechaSalidaCama = fechaTraslado;
            asignacionActual.MotivoTraslado = solicitud.MotivoTraslado;
            _internacionComando.ActualizarInternacionCama(asignacionActual);

            // 8. Crear la nueva asignacion.
            var nuevaAsignacion = new InternacionCama
            {
                Id = Guid.NewGuid(),
                InternacionId = internacion.Id,
                CamaId = camaDestino.Id,
                FechaIngresoCama = fechaTraslado,
                FechaSalidaCama = null,
                EsActual = true,
                MotivoTraslado = null
            };

            await _internacionComando.AgregarInternacionCamaAsync(nuevaAsignacion);

            // 9. Liberar la cama anterior (queda en Limpieza).
            camaAnterior.Estado = EstadoCama.Limpieza;
            _camaComando.Actualizar(camaAnterior);

            // 10. Ocupar la cama destino.
            camaDestino.Estado = EstadoCama.Ocupada;
            _camaComando.Actualizar(camaDestino);

            // 11. Persistir todo 
            await _internacionComando.GuardarCambiosAsync();

            return new TrasladarPacienteInternadoRespuesta
            {
                InternacionId = internacion.Id,
                CamaAnteriorId = camaAnterior.Id,
                CamaNuevaId = camaDestino.Id,
                FechaTraslado = fechaTraslado
            };
        }
    }
}
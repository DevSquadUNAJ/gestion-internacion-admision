using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Admision.Aplicacion.DTOs.Solicitudes;
using Admision.Aplicacion.DTOs.Respuestas;
using Admision.Aplicacion.Excepciones;
using Admision.Aplicacion.Interfaces.ICasosDeUso;
using Admision.Aplicacion.Interfaces.IComandos;
using Admision.Aplicacion.Interfaces.IConsultas;
using Admision.Aplicacion.Interfaces.IMapeadores;
using Admision.Dominio.Constantes;

namespace Admision.Aplicacion.CasosDeUso
{
    public class ProcesarAltaInternacionCasoDeUso : IProcesarAltaInternacionCasoDeUso
    {
        private static readonly HashSet<EstadoInternacion> EstadosDeEgresoPermitidos = new()
        {
            EstadoInternacion.AltaMedica,
            EstadoInternacion.Traslado,
            EstadoInternacion.Defuncion
        };

        private readonly IInternacionConsulta _internacionConsulta;
        private readonly ICamaConsulta _camaConsulta;
        private readonly IInternacionComando _internacionComando;
        private readonly ICamaComando _camaComando;
        private readonly IProcesarAltaInternacionMapeador _mapeador;

        public ProcesarAltaInternacionCasoDeUso(
            IInternacionConsulta internacionConsulta,
            ICamaConsulta camaConsulta,
            IInternacionComando internacionComando,
            ICamaComando camaComando,
            IProcesarAltaInternacionMapeador mapeador)
        {
            _internacionConsulta = internacionConsulta;
            _camaConsulta = camaConsulta;
            _internacionComando = internacionComando;
            _camaComando = camaComando;
            _mapeador = mapeador;
        }

        public async Task<ProcesarAltaInternacionRespuesta> EjecutarAsync(ProcesarAltaInternacionSolicitud solicitud)
        {
            if (!Enum.TryParse<EstadoInternacion>(solicitud.EstadoEgreso, ignoreCase: true, out var estadoEgreso))
                throw new EstadoEgresoInvalidoException(solicitud.EstadoEgreso);

            if (!EstadosDeEgresoPermitidos.Contains(estadoEgreso))
                throw new EstadoEgresoInvalidoException(solicitud.EstadoEgreso);

            var internacion = await _internacionConsulta.ObtenerPorIdParaActualizarAsync(solicitud.InternacionId);
            if (internacion is null)
                throw new InternacionNoEncontradaException(solicitud.InternacionId);

            if (internacion.Estado != EstadoInternacion.Activa)
                throw new InternacionNoActivaException(solicitud.InternacionId);

            var fechaEgreso = DateTime.UtcNow;
            Guid? camaLiberadaId = null;

            var asignacionActual = await _internacionConsulta.ObtenerAsignacionActualAsync(solicitud.InternacionId);
            if (asignacionActual is not null)
            {
                asignacionActual.EsActual = false;
                asignacionActual.FechaSalidaCama = fechaEgreso;
                _internacionComando.ActualizarInternacionCama(asignacionActual);

                var cama = await _camaConsulta.ObtenerPorIdAsync(asignacionActual.CamaId);
                if (cama is not null)
                {
                    cama.Estado = EstadoCama.Limpieza;
                    _camaComando.Actualizar(cama);
                    camaLiberadaId = cama.Id;
                }
            }
            internacion.Estado = estadoEgreso;
            internacion.FechaEgreso = fechaEgreso;
            _internacionComando.Actualizar(internacion);

            await _internacionComando.GuardarCambiosAsync();

            return _mapeador.Mapear(internacion, camaLiberadaId);
        }
    }
}
using System;
using System.Threading.Tasks;
using Admision.Aplicacion.DTOs.Solicitudes;
using Admision.Aplicacion.DTOs.Respuestas;
using Admision.Aplicacion.Excepciones;
using Admision.Aplicacion.Interfaces.ICasosDeUso;
using Admision.Aplicacion.Interfaces.IComandos;
using Admision.Aplicacion.Interfaces.IConsultas;
using Admision.Aplicacion.Interfaces.IMapeadores;
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
        private readonly ITrasladarPacienteInternadoMapeador _mapeador;

        public TrasladarPacienteInternadoCasoDeUso(
            IInternacionConsulta internacionConsulta,
            ICamaConsulta camaConsulta,
            IInternacionComando internacionComando,
            ICamaComando camaComando,
            ITrasladarPacienteInternadoMapeador mapeador)
        {
            _internacionConsulta = internacionConsulta;
            _camaConsulta = camaConsulta;
            _internacionComando = internacionComando;
            _camaComando = camaComando;
            _mapeador = mapeador;
        }

        public async Task<TrasladarPacienteInternadoRespuesta> EjecutarAsync(TrasladarPacienteInternadoSolicitud solicitud)
        {
            var internacion = await _internacionConsulta.ObtenerPorIdAsync(solicitud.InternacionId);
            if (internacion is null)
                throw new InternacionNoEncontradaException(solicitud.InternacionId);

            if (internacion.Estado != EstadoInternacion.Activa)
                throw new InternacionNoActivaException(solicitud.InternacionId);

            var asignacionActual = await _internacionConsulta.ObtenerAsignacionActualAsync(solicitud.InternacionId);
            if (asignacionActual is null)
                throw new InternacionSinCamaAsignadaException(solicitud.InternacionId);

            if (asignacionActual.CamaId == solicitud.CamaDestinoId)
                throw new TrasladoAMismaCamaException(solicitud.CamaDestinoId);

            var camaDestino = await _camaConsulta.ObtenerPorIdAsync(solicitud.CamaDestinoId);
            if (camaDestino is null)
                throw new CamaNoEncontradaException(solicitud.CamaDestinoId);

            if (camaDestino.Estado != EstadoCama.Disponible)
                throw new CamaNoDisponibleException(camaDestino.Id, camaDestino.Estado);

            var camaAnterior = await _camaConsulta.ObtenerPorIdAsync(asignacionActual.CamaId);
            if (camaAnterior is null)
                throw new CamaNoEncontradaException(asignacionActual.CamaId);

            var fechaTraslado = DateTime.UtcNow;

            asignacionActual.EsActual = false;
            asignacionActual.FechaSalidaCama = fechaTraslado;
            asignacionActual.MotivoTraslado = solicitud.MotivoTraslado;
            _internacionComando.ActualizarInternacionCama(asignacionActual);

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

            camaAnterior.Estado = EstadoCama.Limpieza;
            _camaComando.Actualizar(camaAnterior);

            camaDestino.Estado = EstadoCama.Ocupada;
            _camaComando.Actualizar(camaDestino);

            await _internacionComando.GuardarCambiosAsync();

            return _mapeador.Mapear(internacion.Id, camaAnterior.Id, camaDestino.Id, fechaTraslado);
        }
    }
}
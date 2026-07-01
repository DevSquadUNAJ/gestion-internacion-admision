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
    public class CambiarEstadoCamaCasoDeUso : ICambiarEstadoCamaCasoDeUso
    {
        private static readonly HashSet<EstadoCama> EstadosPermitidosManualmente = new()
        {
            EstadoCama.Disponible,
            EstadoCama.Mantenimiento,
            EstadoCama.Limpieza
        };

        private readonly ICamaConsulta _camaConsulta;
        private readonly ICamaComando _camaComando;
        private readonly ICambiarEstadoCamaMapeador _mapeador;

        public CambiarEstadoCamaCasoDeUso(
            ICamaConsulta camaConsulta,
            ICamaComando camaComando,
            ICambiarEstadoCamaMapeador mapeador)
        {
            _camaConsulta = camaConsulta;
            _camaComando = camaComando;
            _mapeador = mapeador;
        }

        public async Task<CambiarEstadoCamaRespuesta> EjecutarAsync(CambiarEstadoCamaSolicitud solicitud)
        {
            if (!Enum.TryParse<EstadoCama>(solicitud.NuevoEstado, ignoreCase: true, out var nuevoEstado))
                throw new EstadoCamaInvalidoException(solicitud.NuevoEstado);

            if (!EstadosPermitidosManualmente.Contains(nuevoEstado))
                throw new EstadoCamaInvalidoException(solicitud.NuevoEstado);

            var cama = await _camaConsulta.ObtenerPorIdAsync(solicitud.CamaId);
            if (cama is null)
                throw new CamaNoEncontradaException(solicitud.CamaId);

            if (cama.Estado == EstadoCama.Ocupada)
                throw new CambioEstadoCamaNoPermitidoException(cama.Estado, nuevoEstado);

            if (cama.Estado == nuevoEstado)
                throw new CambioEstadoCamaNoPermitidoException(cama.Estado);

            var estadoAnterior = cama.Estado;
            var fechaCambio = DateTime.UtcNow;

            cama.Estado = nuevoEstado;
            _camaComando.Actualizar(cama);
            await _camaComando.GuardarCambiosAsync();

            return _mapeador.Mapear(cama, estadoAnterior, fechaCambio);
        }
    }
}
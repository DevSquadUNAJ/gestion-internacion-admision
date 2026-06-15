using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Admision.Aplicacion.DTOs.Solicitudes;
using Admision.Aplicacion.DTOs.Respuestas;
using Admision.Aplicacion.Excepciones;
using Admision.Aplicacion.Interfaces.ICasosDeUso;
using Admision.Aplicacion.Interfaces.IComandos;
using Admision.Aplicacion.Interfaces.IConsultas;
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

        public CambiarEstadoCamaCasoDeUso(
            ICamaConsulta camaConsulta,
            ICamaComando camaComando)
        {
            _camaConsulta = camaConsulta;
            _camaComando = camaComando;
        }

        public async Task<CambiarEstadoCamaRespuesta> EjecutarAsync(CambiarEstadoCamaSolicitud solicitud)
        {
            // 1. Parsear el string recibido de la solicitud al enum EstadoCama.
            if (!Enum.TryParse<EstadoCama>(solicitud.NuevoEstado, ignoreCase: true, out var nuevoEstado))
                throw new EstadoCamaInvalidoException(solicitud.NuevoEstado);

            // 2. Validar que el estado parseado sea uno permitido manualmente.
            //    Ocupada se excluye porque solo se gestiona desde el flujo de internacion.
            if (!EstadosPermitidosManualmente.Contains(nuevoEstado))
                throw new EstadoCamaInvalidoException(solicitud.NuevoEstado);

            // 3. Validar existencia de la cama.
            var cama = await _camaConsulta.ObtenerPorIdAsync(solicitud.CamaId);
            if (cama is null)
                throw new CamaNoEncontradaException(solicitud.CamaId);

            // 4. No se permite cambio manual si la cama esta ocupada.
            if (cama.Estado == EstadoCama.Ocupada)
                throw new CambioEstadoCamaNoPermitidoException(cama.Estado, nuevoEstado);

            // 5. Evitar cambios nulos.
            if (cama.Estado == nuevoEstado)
                throw new CambioEstadoCamaNoPermitidoException(cama.Estado, nuevoEstado);

            var estadoAnterior = cama.Estado;
            var fechaCambio = DateTime.UtcNow;

            // 6. Aplicar el cambio y persistir.
            cama.Estado = nuevoEstado;
            _camaComando.Actualizar(cama);
            await _camaComando.GuardarCambiosAsync();

            return new CambiarEstadoCamaRespuesta
            {
                CamaId = cama.Id,
                EstadoAnterior = estadoAnterior,
                EstadoNuevo = cama.Estado,
                FechaCambio = fechaCambio
            };
        }
    }
}
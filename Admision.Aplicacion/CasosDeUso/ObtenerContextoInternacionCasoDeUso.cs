using Admision.Aplicacion.DTOs.Respuestas;
using Admision.Aplicacion.Excepciones;
using Admision.Aplicacion.Interfaces.ICasosDeUso;
using Admision.Aplicacion.Interfaces.IConsultas;
using Admision.Aplicacion.Interfaces.IMapeadores;
using Admision.Dominio.Constantes;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Admision.Aplicacion.CasosDeUso
{
    public class ObtenerContextoInternacionCasoDeUso : IObtenerContextoInternacionCasoDeUso
    {
        private readonly IInternacionConsulta _internacionConsulta;
        private readonly IContextoInternacionMapeador _mapeador;

        public ObtenerContextoInternacionCasoDeUso(
            IInternacionConsulta internacionConsulta,
            IContextoInternacionMapeador mapeador)
        {
            _internacionConsulta = internacionConsulta;
            _mapeador = mapeador;
        }

        public async Task<ContextoInternacionRespuesta> EjecutarAsync(Guid internacionId)
        {
            var internacion = await _internacionConsulta.ObtenerConContextoCompletoAsync(internacionId);

            if (internacion is null)
                throw new InternacionNoEncontradaException(internacionId);

            if (internacion.Estado != EstadoInternacion.Activa)
                throw new InternacionNoActivaException(internacionId);

            var asignacionActual = internacion.HistorialCamas.FirstOrDefault();
            if (asignacionActual is null)
                throw new InternacionSinCamaAsignadaException(internacionId);

            return _mapeador.Mapear(internacion, asignacionActual);
        }
    }
}
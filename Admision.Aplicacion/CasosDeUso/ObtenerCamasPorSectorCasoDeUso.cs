using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Admision.Aplicacion.DTOs.Respuestas;
using Admision.Aplicacion.Interfaces.ICasosDeUso;
using Admision.Aplicacion.Interfaces.IConsultas;
using Admision.Aplicacion.Interfaces.IMapeadores;

namespace Admision.Aplicacion.CasosDeUso
{
    public class ObtenerCamasPorSectorCasoDeUso : IObtenerCamasPorSectorCasoDeUso
    {
        private readonly ICamaConsulta _camaConsulta;
        private readonly ICamaMapeador _camaMapeador;

        public ObtenerCamasPorSectorCasoDeUso(ICamaConsulta camaConsulta, ICamaMapeador camaMapeador)
        {
            _camaConsulta = camaConsulta;
            _camaMapeador = camaMapeador;
        }

        public async Task<IEnumerable<DetalleCamaRespuesta>> EjecutarAsync(Guid sectorId)
        {
            var camas = await _camaConsulta.ObtenerCamasPorSectorConPacienteAsync(sectorId);
            return _camaMapeador.Mapear(camas);
        }
    }
}
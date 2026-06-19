using System.Collections.Generic;
using System.Threading.Tasks;
using Admision.Aplicacion.DTOs.Respuestas;
using Admision.Aplicacion.Interfaces.ICasosDeUso;
using Admision.Aplicacion.Interfaces.IConsultas;
using Admision.Aplicacion.Interfaces.IMapeadores;

namespace Admision.Aplicacion.CasosDeUso
{
    public class ObtenerSectoresCasoDeUso : IObtenerSectoresCasoDeUso
    {
        private readonly ISectorConsulta _sectorConsulta;
        private readonly ISectorMapeador _sectorMapeador;

        public ObtenerSectoresCasoDeUso(ISectorConsulta sectorConsulta, ISectorMapeador sectorMapeador)
        {
            _sectorConsulta = sectorConsulta;
            _sectorMapeador = sectorMapeador;
        }

        public async Task<IEnumerable<SectorOcupacionRespuesta>> EjecutarAsync()
        {
            var sectores = await _sectorConsulta.ObtenerTodosConCamasAsync();
            return _sectorMapeador.Mapear(sectores);
        }
    }
}
using Admision.Aplicacion.DTOs.Respuestas;
using Admision.Aplicacion.Interfaces.ICasosDeUso;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Admision.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admision,Medico,Enfermera")]
    public class SectoresController : ControllerBase
    {
        private readonly IObtenerSectoresCasoDeUso _obtenerSectoresCasoDeUso;
        private readonly IObtenerCamasPorSectorCasoDeUso _obtenerCamasPorSectorCasoDeUso;

        public SectoresController(
            IObtenerSectoresCasoDeUso obtenerSectoresCasoDeUso,
            IObtenerCamasPorSectorCasoDeUso obtenerCamasPorSectorCasoDeUso)
        {
            _obtenerSectoresCasoDeUso = obtenerSectoresCasoDeUso;
            _obtenerCamasPorSectorCasoDeUso = obtenerCamasPorSectorCasoDeUso;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SectorOcupacionRespuesta>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> ObtenerSectores()
        {
            var respuesta = await _obtenerSectoresCasoDeUso.EjecutarAsync();
            return Ok(respuesta);
        }

        [HttpGet("{sectorId}/camas")]
        [ProducesResponseType(typeof(IEnumerable<DetalleCamaRespuesta>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> ObtenerCamasPorSector(Guid sectorId)
        {
            var respuesta = await _obtenerCamasPorSectorCasoDeUso.EjecutarAsync(sectorId);
            return Ok(respuesta);
        }
    }
}
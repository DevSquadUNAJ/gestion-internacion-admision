using Admision.Aplicacion.DTOs.Respuestas;
using Admision.Aplicacion.DTOs.Solicitudes;
using Admision.Aplicacion.Interfaces.ICasosDeUso;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Admision.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admision,Enfermera")]
    public class CamasController : ControllerBase
    {
        private readonly ICambiarEstadoCamaCasoDeUso _cambiarEstadoCamaCasoDeUso;

        public CamasController(ICambiarEstadoCamaCasoDeUso cambiarEstadoCamaCasoDeUso)
        {
            _cambiarEstadoCamaCasoDeUso = cambiarEstadoCamaCasoDeUso;
        }

        [HttpPatch("{id}/estado")]
        [ProducesResponseType(typeof(CambiarEstadoCamaRespuesta), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CambiarEstado(Guid id, [FromBody] CambiarEstadoCamaSolicitud solicitud)
        {
            solicitud.CamaId = id;
            var respuesta = await _cambiarEstadoCamaCasoDeUso.EjecutarAsync(solicitud);
            return Ok(respuesta);
        }
    }
}
using Admision.Aplicacion.DTOs.Respuestas;
using Admision.Aplicacion.DTOs.Solicitudes;
using Admision.Aplicacion.Interfaces.ICasosDeUso;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Admision.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admision")]
    public class InternacionesController : ControllerBase
    {
        private readonly IRegistrarInternacionCasoDeUso _registrarInternacionCasoDeUso;

        public InternacionesController(IRegistrarInternacionCasoDeUso registrarInternacionCasoDeUso)
        {
            _registrarInternacionCasoDeUso = registrarInternacionCasoDeUso;
        }

        [HttpPost]
        [ProducesResponseType(typeof(RegistrarInternacionRespuesta), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Registrar([FromBody] RegistrarInternacionSolicitud solicitud)
        {
            var respuesta = await _registrarInternacionCasoDeUso.EjecutarAsync(solicitud);
            return CreatedAtAction(nameof(Registrar), new { id = respuesta.InternacionId }, respuesta);
        }
    }
}
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
    [Authorize(Roles = "Admision,Medico,Enfermera")]
    public class InternacionesController : ControllerBase
    {
        private readonly IRegistrarInternacionCasoDeUso _registrarInternacionCasoDeUso;
        private readonly ITrasladarPacienteInternadoCasoDeUso _trasladarPacienteInternadoCasoDeUso;
        private readonly IObtenerContextoInternacionCasoDeUso _obtenerContextoInternacionCasoDeUso;

        public InternacionesController(
            IRegistrarInternacionCasoDeUso registrarInternacionCasoDeUso,
            ITrasladarPacienteInternadoCasoDeUso trasladarPacienteInternadoCasoDeUso,
            IObtenerContextoInternacionCasoDeUso obtenerContextoInternacionCasoDeUso)
        {
            _registrarInternacionCasoDeUso = registrarInternacionCasoDeUso;
            _trasladarPacienteInternadoCasoDeUso = trasladarPacienteInternadoCasoDeUso;
            _obtenerContextoInternacionCasoDeUso = obtenerContextoInternacionCasoDeUso;
        }

        [HttpPost]
        [Authorize(Roles = "Admision")]
        [ProducesResponseType(typeof(RegistrarInternacionRespuesta), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Registrar([FromBody] RegistrarInternacionSolicitud solicitud)
        {
            var respuesta = await _registrarInternacionCasoDeUso.EjecutarAsync(solicitud);
            return CreatedAtAction(nameof(Registrar), new { id = respuesta.InternacionId }, respuesta);
        }

        [HttpPatch("{internacionId}/trasladar")]
        [Authorize(Roles = "Admision")]
        [ProducesResponseType(typeof(TrasladarPacienteInternadoRespuesta), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Trasladar(Guid internacionId, [FromBody] TrasladarPacienteInternadoSolicitud solicitud)
        {
            solicitud.InternacionId = internacionId;
            var respuesta = await _trasladarPacienteInternadoCasoDeUso.EjecutarAsync(solicitud);
            return Ok(respuesta);
        }

        [HttpGet("{internacionId}/contexto")]
        // No necesita [Authorize] específico porque hereda el de la clase (Admision, Medico, Enfermera)
        [ProducesResponseType(typeof(ContextoInternacionRespuesta), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> ObtenerContexto(Guid internacionId)
        {
            var respuesta = await _obtenerContextoInternacionCasoDeUso.EjecutarAsync(internacionId);
            return Ok(respuesta);
        }
    }
}
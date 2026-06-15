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
    [Authorize(Roles = "Admision")]
    public class InternacionesController : ControllerBase
    {
        private readonly IRegistrarInternacionCasoDeUso _registrarInternacionCasoDeUso;
        private readonly ITrasladarPacienteInternadoCasoDeUso _trasladarPacienteInternadoCasoDeUso;


        public InternacionesController(
            IRegistrarInternacionCasoDeUso registrarInternacionCasoDeUso,
            ITrasladarPacienteInternadoCasoDeUso trasladarPacienteInternadoCasoDeUso)
        {
            _registrarInternacionCasoDeUso = registrarInternacionCasoDeUso;
            _trasladarPacienteInternadoCasoDeUso = trasladarPacienteInternadoCasoDeUso;
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

        [HttpPatch("{id}/trasladar")]
        [ProducesResponseType(typeof(TrasladarPacienteInternadoRespuesta), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Trasladar(Guid id, [FromBody] TrasladarPacienteInternadoSolicitud solicitud)
        {
            solicitud.InternacionId = id;
            var respuesta = await _trasladarPacienteInternadoCasoDeUso.EjecutarAsync(solicitud);
            return Ok(respuesta);
        }
    }
}
using Admision.Aplicacion.DTOs.Respuestas;
using Admision.Aplicacion.Interfaces.ICasosDeUso;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Admision.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admision,Medico,Enfermera")]
    public class PacientesController : ControllerBase
    {
        private readonly IBuscarPacientePorDniCasoDeUso _buscarPacientePorDniCasoDeUso;

        public PacientesController(IBuscarPacientePorDniCasoDeUso buscarPacientePorDniCasoDeUso)
        {
            _buscarPacientePorDniCasoDeUso = buscarPacientePorDniCasoDeUso;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PacienteRespuesta), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Obtener([FromQuery] string dni)
        {
            var respuesta = await _buscarPacientePorDniCasoDeUso.EjecutarAsync(dni);
            return Ok(respuesta);
        }
    }
}
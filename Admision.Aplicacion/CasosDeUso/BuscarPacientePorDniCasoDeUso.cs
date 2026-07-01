using Admision.Aplicacion.DTOs.Respuestas;
using Admision.Aplicacion.Excepciones;
using Admision.Aplicacion.Interfaces.ICasosDeUso;
using Admision.Aplicacion.Interfaces.IConsultas;
using System.Linq;
using System.Threading.Tasks;

namespace Admision.Aplicacion.CasosDeUso
{
    public class BuscarPacientePorDniCasoDeUso : IBuscarPacientePorDniCasoDeUso
    {
        private readonly IPacienteConsulta _pacienteConsulta;

        public BuscarPacientePorDniCasoDeUso(IPacienteConsulta pacienteConsulta)
        {
            _pacienteConsulta = pacienteConsulta;
        }

        public async Task<PacienteRespuesta> EjecutarAsync(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                throw new DniInvalidoException();

            var paciente = await _pacienteConsulta.ObtenerPorDniAsync(dni);

            if (paciente == null)
                throw new PacienteNoEncontradoException(dni);

            return new PacienteRespuesta
            {
                PacienteId = paciente.Id,
                Nombre = paciente.Nombre,
                Dni = paciente.Dni,
                EstaInternado = paciente.Internaciones.Any()
            };
        }
    }
}
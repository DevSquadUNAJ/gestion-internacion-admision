using System;

namespace Admision.Aplicacion.Excepciones
{
    public class PacienteNoEncontradoException : ExcepcionDeNegocio
    {
        public PacienteNoEncontradoException(Guid pacienteId)
            : base($"No se encontró el paciente con Id {pacienteId}.") { }

        public PacienteNoEncontradoException(string dni)
            : base($"No se encontró ningún paciente con el DNI {dni}.") { }
    }
}
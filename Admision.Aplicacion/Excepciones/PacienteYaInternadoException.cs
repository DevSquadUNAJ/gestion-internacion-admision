using System;

namespace Admision.Aplicacion.Excepciones
{
    public class PacienteYaInternadoException : ExcepcionDeNegocio
    {
        public PacienteYaInternadoException(Guid pacienteId)
            : base($"El paciente con Id {pacienteId} ya posee una internación activa.") { }
    }
}
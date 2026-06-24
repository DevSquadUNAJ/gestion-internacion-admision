using System;

namespace Admision.Aplicacion.Excepciones
{
    public class PacienteYaInternadoException : ExcepcionDeNegocio
    {
        public PacienteYaInternadoException(string pacienteDni)
            : base($"El paciente con DNI {pacienteDni} ya posee una internación activa.") { }
    }
}
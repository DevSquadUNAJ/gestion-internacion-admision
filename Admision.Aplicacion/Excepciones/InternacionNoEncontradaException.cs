using System;

namespace Admision.Aplicacion.Excepciones
{
    public class InternacionNoEncontradaException : ExcepcionDeNegocio
    {
        public InternacionNoEncontradaException(Guid internacionId)
            : base($"No se encontró la internación con Id {internacionId}.") { }
    }
}
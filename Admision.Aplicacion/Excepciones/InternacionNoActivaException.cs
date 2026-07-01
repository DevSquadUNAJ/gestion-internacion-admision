using System;

namespace Admision.Aplicacion.Excepciones
{
    public class InternacionNoActivaException : ExcepcionDeNegocio
    {
        public InternacionNoActivaException(Guid internacionId)
            : base($"La internación con Id {internacionId} no se encuentra activa y no puede ser trasladada.") { }
    }
}
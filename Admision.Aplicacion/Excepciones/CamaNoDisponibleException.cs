using System;

namespace Admision.Aplicacion.Excepciones
{
    public class CamaNoDisponibleException : ExcepcionDeNegocio
    {
        public CamaNoDisponibleException(Guid camaId, string estadoActual)
            : base($"La cama con Id {camaId} no está disponible. Estado actual: {estadoActual}.") { }
    }
}
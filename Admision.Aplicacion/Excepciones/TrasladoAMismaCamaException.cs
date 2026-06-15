using System;

namespace Admision.Aplicacion.Excepciones
{
    public class TrasladoAMismaCamaException : ExcepcionDeNegocio
    {
        public TrasladoAMismaCamaException(Guid camaId)
            : base($"La cama destino con Id {camaId} es la misma que la cama actual del paciente.") { }
    }
}
using System;

namespace Admision.Aplicacion.Excepciones
{
    public abstract class ExcepcionDeNegocio : Exception
    {
        protected ExcepcionDeNegocio(string mensaje) : base(mensaje) { }
    }
}
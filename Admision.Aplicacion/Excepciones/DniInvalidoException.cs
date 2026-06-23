namespace Admision.Aplicacion.Excepciones
{
    public class DniInvalidoException : ExcepcionDeNegocio
    {
        public DniInvalidoException()
            : base("El DNI es requerido para la búsqueda.") { }
    }
}
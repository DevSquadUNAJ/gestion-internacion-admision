using System;
using System.Collections.Generic;

namespace Admision.Dominio.Entidades
{
    public class Paciente
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Dni { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public string Sexo { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;

        public virtual ICollection<Internacion> Internaciones { get; set; } = new List<Internacion>();
    }
}
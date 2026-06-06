using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;

namespace Admision.Dominio.Entidades
{
    public class Paciente
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Dni { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public string Telefono { get; set; }

        public virtual ICollection<Internacion> Internaciones { get; set; } = new List<Internacion>();
    }
}
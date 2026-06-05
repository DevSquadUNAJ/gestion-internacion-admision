using System;
using System.Collections.Generic;

namespace Admision.Dominio.Entidades
{
    public class Sector
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public int Piso { get; set; }

        public virtual ICollection<Cama> Camas { get; set; } = new List<Cama>();
    }
}
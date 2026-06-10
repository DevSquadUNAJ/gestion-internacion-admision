using System;
using System.Collections.Generic;

namespace Admision.Dominio.Entidades
{
    public class Cama
    {
        public Guid Id { get; set; }
        public Guid SectorId { get; set; }
        public int Numero { get; set; }
        public string Estado { get; set; } = string.Empty;
        public virtual Sector Sector { get; set; }
        public virtual ICollection<InternacionCama> HistorialInternaciones { get; set; } = new List<InternacionCama>();
    }
}
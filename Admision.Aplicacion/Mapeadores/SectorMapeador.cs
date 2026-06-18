using Admision.Aplicacion.DTOs.Respuestas;
using Admision.Aplicacion.Interfaces.IMapeadores;
using Admision.Dominio.Constantes;
using Admision.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Admision.Aplicacion.Mapeadores
{
    public class SectorMapeador : ISectorMapeador
    {
        public IEnumerable<SectorOcupacionRespuesta> Mapear(IEnumerable<Sector> sectores)
        {
            return sectores.Select(s => {
                var totalCamas = s.Camas.Count;
                var disponibles = s.Camas.Count(c => c.Estado == EstadoCama.Disponible);
                var ocupadas = s.Camas.Count(c => c.Estado == EstadoCama.Ocupada);

                var porcentaje = totalCamas > 0 ? Math.Round((double)ocupadas / totalCamas * 100, 2) : 0;

                return new SectorOcupacionRespuesta
                {
                    SectorId = s.Id,
                    Nombre = s.Nombre,
                    Piso = s.Piso,
                    CantidadTotalCamas = totalCamas,
                    CantidadCamasDisponibles = disponibles,
                    CantidadCamasOcupadas = ocupadas,
                    PorcentajeOcupacion = porcentaje
                };
            }).OrderBy(s => s.Piso).ThenBy(s => s.Nombre);
        }
    }
}
using System;
using System.Threading.Tasks;
using Admision.Aplicacion.DTOs.Solicitudes;
using Admision.Aplicacion.DTOs.Respuestas;
using Admision.Aplicacion.Excepciones;
using Admision.Aplicacion.Interfaces.ICasosDeUso;
using Admision.Aplicacion.Interfaces.IComandos;
using Admision.Aplicacion.Interfaces.IConsultas;
using Admision.Aplicacion.Interfaces.IMapeadores;
using Admision.Dominio.Constantes;
using Admision.Dominio.Entidades;

namespace Admision.Aplicacion.CasosDeUso
{
    public class RegistrarInternacionCasoDeUso : IRegistrarInternacionCasoDeUso
    {
        private readonly IPacienteConsulta _pacienteConsulta;
        private readonly IInternacionConsulta _internacionConsulta;
        private readonly ICamaConsulta _camaConsulta;
        private readonly IInternacionComando _internacionComando;
        private readonly ICamaComando _camaComando;
        private readonly IRegistrarInternacionMapeador _mapeador;

        public RegistrarInternacionCasoDeUso(
            IPacienteConsulta pacienteConsulta,
            IInternacionConsulta internacionConsulta,
            ICamaConsulta camaConsulta,
            IInternacionComando internacionComando,
            ICamaComando camaComando,
            IRegistrarInternacionMapeador mapeador)
        {
            _pacienteConsulta = pacienteConsulta;
            _internacionConsulta = internacionConsulta;
            _camaConsulta = camaConsulta;
            _internacionComando = internacionComando;
            _camaComando = camaComando;
            _mapeador = mapeador;
        }

        public async Task<RegistrarInternacionRespuesta> EjecutarAsync(RegistrarInternacionSolicitud solicitud)
        {
            var paciente = await _pacienteConsulta.ObtenerPorIdAsync(solicitud.PacienteId);
            if (paciente is null)
                throw new PacienteNoEncontradoException(solicitud.PacienteId);

            var yaInternado = await _internacionConsulta.TieneInternacionActivaAsync(solicitud.PacienteId);
            if (yaInternado)
                throw new PacienteYaInternadoException(paciente.Dni);

            Cama? camaAsignada = null;
            if (solicitud.CamaId.HasValue)
            {
                camaAsignada = await _camaConsulta.ObtenerPorIdAsync(solicitud.CamaId.Value);

                if (camaAsignada is null)
                    throw new CamaNoEncontradaException(solicitud.CamaId.Value);

                if (camaAsignada.Estado != EstadoCama.Disponible)
                    throw new CamaNoDisponibleException(camaAsignada.Id, camaAsignada.Estado);
            }

            var internacion = new Internacion
            {
                Id = Guid.NewGuid(),
                PacienteId = solicitud.PacienteId,
                FechaIngreso = DateTime.UtcNow,
                FechaEgreso = null,
                Motivo = solicitud.Motivo,
                Estado = EstadoInternacion.Activa
            };

            await _internacionComando.AgregarAsync(internacion);

            if (camaAsignada is not null)
            {
                var asignacion = new InternacionCama
                {
                    Id = Guid.NewGuid(),
                    InternacionId = internacion.Id,
                    CamaId = camaAsignada.Id,
                    FechaIngresoCama = internacion.FechaIngreso,
                    FechaSalidaCama = null,
                    EsActual = true
                };

                await _internacionComando.AgregarInternacionCamaAsync(asignacion);

                camaAsignada.Estado = EstadoCama.Ocupada;
                _camaComando.Actualizar(camaAsignada);
            }

            await _internacionComando.GuardarCambiosAsync();

            return _mapeador.Mapear(internacion, camaAsignada);
        }
    }
}
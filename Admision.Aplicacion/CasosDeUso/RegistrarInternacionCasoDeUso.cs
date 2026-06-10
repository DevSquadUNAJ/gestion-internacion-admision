using System;
using System.Threading.Tasks;
using Admision.Aplicacion.DTOs.Solicitudes;
using Admision.Aplicacion.DTOs.Respuestas;
using Admision.Aplicacion.Excepciones;
using Admision.Aplicacion.Interfaces.ICasosDeUso;
using Admision.Aplicacion.Interfaces.IComandos;
using Admision.Aplicacion.Interfaces.IConsultas;
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

        public RegistrarInternacionCasoDeUso(
            IPacienteConsulta pacienteConsulta,
            IInternacionConsulta internacionConsulta,
            ICamaConsulta camaConsulta,
            IInternacionComando internacionComando,
            ICamaComando camaComando)
        {
            _pacienteConsulta = pacienteConsulta;
            _internacionConsulta = internacionConsulta;
            _camaConsulta = camaConsulta;
            _internacionComando = internacionComando;
            _camaComando = camaComando;
        }

        // Acontinuación dejo comentarios en cada paso para explicar la lógica de negocio y las validaciones que se realizan, luego de verificar lo pueden borrar si lo desean.
        public async Task<RegistrarInternacionRespuesta> EjecutarAsync(RegistrarInternacionSolicitud solicitud)
        {
            // 1. Validar existencia del paciente.
            var paciente = await _pacienteConsulta.ObtenerPorIdAsync(solicitud.PacienteId);
            if (paciente is null)
                throw new PacienteNoEncontradoException(solicitud.PacienteId);

            // 2. Validar que el paciente no tenga otra internacion activa.
            var yaInternado = await _internacionConsulta.TieneInternacionActivaAsync(solicitud.PacienteId);
            if (yaInternado)
                throw new PacienteYaInternadoException(solicitud.PacienteId);

            // 3. Si vino una cama, validarla y reservarla.
            Cama? camaAsignada = null;
            if (solicitud.CamaId.HasValue)
            {
                camaAsignada = await _camaConsulta.ObtenerPorIdAsync(solicitud.CamaId.Value);

                if (camaAsignada is null)
                    throw new CamaNoEncontradaException(solicitud.CamaId.Value);

                if (camaAsignada.Estado != EstadoCama.Disponible)
                    throw new CamaNoDisponibleException(camaAsignada.Id, camaAsignada.Estado);
            }

            // 4. Crear la internacion.
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

            // 5. Si hay cama, registrar la asignacion y ocupar la cama.
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

            // 6. Persistir todo en una unica transaccion.
            await _internacionComando.GuardarCambiosAsync(); // Aplicar optimistic concurrency control para evitar problemas de concurrencia en la asignacion de camas.

            // 7. Armar respuesta.
            return new RegistrarInternacionRespuesta
            {
                InternacionId = internacion.Id,
                PacienteId = internacion.PacienteId,
                FechaIngreso = internacion.FechaIngreso,
                Estado = internacion.Estado,
                CamaAsignadaId = camaAsignada?.Id
            };
        }
    }
}
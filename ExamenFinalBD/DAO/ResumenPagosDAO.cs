using ExamenFinalBD.BD;
using ExamenFinalBD.DAO;
using ExamenFinalBD.Utils;
using System;
using System.Linq;

namespace TuProyecto.DAO
{
    public class ResumenPagosDAO
    {
        General general = new General();
        public ResumenPagosBarraDTO ObtenerResumenActual(string idContrato)
        {
            using (var milinq = new DBLinQ(general.CadenaConexion()))
            {
                // 1. Contrato
                var contrato = milinq.Contrato
                                     .FirstOrDefault(c => c.id_contrato == idContrato);

                if (contrato == null)
                    return null;

                // 2. Encontrar la ÚLTIMA cuota (por fecha_vencimiento) del contrato
                DateTime? ultimaFechaCuota =
                    (from servicio in milinq.Servicio
                     join cuota in milinq.Cuota
                         on servicio.id_servicio equals cuota.id_servicio
                     where servicio.id_contrato == idContrato
                     select (DateTime?)cuota.fecha_vencimiento
                    ).Max();   // <-- en lugar de OrderBy + LastOrDefault


                // Si todavía no hay cuotas, devolvemos solo info básica
                if (!ultimaFechaCuota.HasValue)
                {
                    return new ResumenPagosBarraDTO
                    {
                        IdContrato = idContrato,
                        DireccionInstalacion = contrato.direccion_instalacion,
                        FechaInicioContratoTexto = contrato.fecha_inicio,
                        FechaFinContrato = contrato.fecha_fin,
                        TotalCable = 0m,
                        TotalInternet = 0m,
                        TotalTelefono = 0m,
                        TotalPendiente = 0m
                    };
                }

                DateTime fechaUltimaCuota = ultimaFechaCuota.Value.Date;
                DateTime inicioDia = fechaUltimaCuota;
                DateTime finDia = inicioDia.AddDays(1);

                // 3. Cuotas de ese contrato para ESA fecha (todos los servicios del mes actual)
                var cuotasPeriodo = (
                    from servicio in milinq.Servicio
                    join cuota in milinq.Cuota
                        on servicio.id_servicio equals cuota.id_servicio
                    join tipoServ in milinq.Tipo_servicio
                        on servicio.id_tipo_servicio equals tipoServ.id_tipo_servicio
                    where servicio.id_contrato == idContrato
                          && cuota.fecha_vencimiento >= inicioDia
                          && cuota.fecha_vencimiento < finDia
                    select new
                    {
                        cuota.total,
                        tipoServ.nombre_tipo_servicio
                    }
                ).ToList();

                // 4. Total por tipo de servicio (ajusta los Contains si usas nombres distintos)
                decimal totalCable = cuotasPeriodo
                    .Where(c => c.nombre_tipo_servicio
                        .IndexOf("cable", StringComparison.OrdinalIgnoreCase) >= 0)
                    .Sum(c => c.total);

                decimal totalInternet = cuotasPeriodo
                    .Where(c => c.nombre_tipo_servicio
                        .IndexOf("internet", StringComparison.OrdinalIgnoreCase) >= 0)
                    .Sum(c => c.total);

                decimal totalTelefono = cuotasPeriodo
                    .Where(c => c.nombre_tipo_servicio
                        .IndexOf("telefono", StringComparison.OrdinalIgnoreCase) >= 0
                          || c.nombre_tipo_servicio
                        .IndexOf("teléfono", StringComparison.OrdinalIgnoreCase) >= 0)
                    .Sum(c => c.total);

                // 5. Saldo pendiente ACTUAL del contrato
                //    (último BalanceForward de PagoDAO.ObtenerPagosPorContrato)
                var pagoDAO = new PagoDAO();
                var listaPagos = pagoDAO.ObtenerPagosPorContrato(idContrato);

                var ultimaLinea = listaPagos
                    .OrderBy(p => p.FechaVencimientoCuota)
                    .ThenBy(p => p.NumeroCuota)
                    .LastOrDefault();

                decimal saldoPendiente = ultimaLinea?.BalanceForward ?? 0m;

                // 6. Armar DTO de salida
                return new ResumenPagosBarraDTO
                {
                    IdContrato = idContrato,
                    DireccionInstalacion = contrato.direccion_instalacion,
                    FechaInicioContratoTexto = contrato.fecha_inicio,
                    FechaFinContrato = contrato.fecha_fin,

                    TotalCable = totalCable,
                    TotalInternet = totalInternet,
                    TotalTelefono = totalTelefono,

                    TotalPendiente = saldoPendiente
                };
            }
        }
        public class ResumenPagosBarraDTO
        {
            public string IdContrato { get; set; }

            public string DireccionInstalacion { get; set; }
            public string FechaInicioContratoTexto { get; set; }  // CHAR(10) en la tabla
            public DateTime FechaFinContrato { get; set; }

            public decimal TotalCable { get; set; }
            public decimal TotalInternet { get; set; }
            public decimal TotalTelefono { get; set; }

            // saldo pendiente total del contrato (igual al último BalanceForward)
            public decimal TotalPendiente { get; set; }
        }

    }
}

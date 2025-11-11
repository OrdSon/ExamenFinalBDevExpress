using ExamenFinalBD.BD;
using ExamenFinalBD.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenFinalBD.DAO
{
    internal class PagoDAO
    {
        General general = new General();
        public List<PagoGridDTO> ObtenerPagosPorContrato(string idContrato)
        {
            using (var milinq = new DBLinQ(general.CadenaConexion()))
            {
                decimal montoMora = milinq.Configuracion
                                           .Select(c => c.monto_mora)
                                           .FirstOrDefault();

                // 2. Query base: contrato → servicio → cuota → factura → pago
                var queryBase =
                    from contrato in milinq.Contrato
                    where contrato.id_contrato == idContrato

                    join servicio in milinq.Servicio
                        on contrato.id_contrato equals servicio.id_contrato

                    join cuota in milinq.Cuota
                        on servicio.id_servicio equals cuota.id_servicio

                    join factura in milinq.Factura
                        on cuota.id_cuota equals factura.id_cuota into facturas
                    from factura in facturas.DefaultIfEmpty()

                    join pago in milinq.Pago
                        on factura.id_pago equals pago.id_pago into pagos
                    from pago in pagos.DefaultIfEmpty()

                    join estadoPago in milinq.Estado_pago
                        on pago.id_estado_pago equals estadoPago.id_estado_pago into estadosPago
                    from estadoPago in estadosPago.DefaultIfEmpty()

                    select new PagoGridDTO
                    {
                        IdContrato = contrato.id_contrato,
                        IdServicio = servicio.id_servicio,
                        IdCuota = cuota.id_cuota,
                        IdFactura = factura != null ? factura.id_factura : null,
                        IdPago = pago != null ? pago.id_pago : null,

                        FechaVencimientoCuota = cuota.fecha_vencimiento,
                        FechaPago = pago != null ? (DateTime?)pago.fecha_pago : null,

                        // Estado del pago según código
                        EstadoPago =
                            pago == null ? "Sin pago" :
                            pago.id_estado_pago == "EPA001" ? "Completo" :
                            pago.id_estado_pago == "EPA002" ? "Pendiente" :
                            (estadoPago != null ? estadoPago.nombre_estado_pago : "Desconocido"),

                        MontoCuota = cuota.total,
                        MontoFactura = factura != null ? factura.total_factura : 0m,
                        MontoPago = pago != null ? pago.sub_total : 0m,

                        // Se rellenan después
                        PagoAtrasado = false,
                        MoraAplicada = 0m,
                        TotalConMora = 0m,
                        BalanceForward = 0m
                    };

                // 3. Materializar y ordenar (por vencimiento y luego fecha de pago)
                var lista = queryBase
                    .OrderBy(x => x.FechaVencimientoCuota)
                    .ThenBy(x => x.FechaPago ?? x.FechaVencimientoCuota)
                    .ToList();

                // 4. Determinar qué facturas llevan mora (una vez por factura)
                //    Una factura está atrasada si alguno de sus pagos se hizo después del vencimiento
                var moraPorFactura = lista
                    .Where(x => !string.IsNullOrEmpty(x.IdFactura) && x.FechaPago.HasValue)
                    .GroupBy(x => x.IdFactura)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Any(item => item.FechaPago.Value.Date > item.FechaVencimientoCuota.Date)
                    );

                decimal saldoAcumulado = 0m;
                var facturasConMoraAplicada = new HashSet<string>();

                // 5. Recorrer en orden y calcular mora, pago atrasado y balance forward
                foreach (var item in lista)
                {
                    // Saldo antes de esta línea
                    item.BalanceForward = saldoAcumulado;

                    bool facturaTieneMora = false;

                    if (!string.IsNullOrEmpty(item.IdFactura) &&
                        moraPorFactura.TryGetValue(item.IdFactura, out bool atrasada) &&
                        atrasada)
                    {
                        facturaTieneMora = true;
                    }

                    // Si la factura está atrasada, marcamos PagoAtrasado en todas sus filas
                    item.PagoAtrasado = facturaTieneMora;

                    // Aplicar la mora SOLO una vez por factura
                    item.MoraAplicada = 0m;

                    if (facturaTieneMora &&
                        !string.IsNullOrEmpty(item.IdFactura) &&
                        !facturasConMoraAplicada.Contains(item.IdFactura))
                    {
                        item.MoraAplicada = montoMora;
                        facturasConMoraAplicada.Add(item.IdFactura);
                    }

                    // Total de la factura con mora aplicada (por registro)
                    item.TotalConMora = item.MontoFactura + item.MoraAplicada;

                    // Actualizar saldo acumulado:
                    // saldo nuevo = saldo anterior + (cuota + mora - pago)
                    saldoAcumulado = saldoAcumulado
                                     + item.MontoCuota
                                     + item.MoraAplicada
                                     - item.MontoPago;
                }

                return lista;
            }
        }
    }
    public class PagoGridDTO
    {
        public string IdContrato { get; set; }
        public string IdServicio { get; set; }
        public string IdCuota { get; set; }
        public string IdFactura { get; set; }
        public string IdPago { get; set; }

        public DateTime FechaVencimientoCuota { get; set; }
        public DateTime? FechaPago { get; set; }

        public string EstadoPago { get; set; }      // Completo / Pendiente / Sin pago
        public bool PagoAtrasado { get; set; }      // Mora sí/no

        public decimal MontoCuota { get; set; }     // Cuota.total
        public decimal MontoFactura { get; set; }   // Factura.total_factura
        public decimal MontoPago { get; set; }      // Pago.sub_total

        public decimal MoraAplicada { get; set; }   // Solo una vez por factura
        public decimal TotalConMora { get; set; }   // MontoFactura + MoraAplicada

        public decimal BalanceForward { get; set; } // Saldo anterior antes de este registro
    }
}

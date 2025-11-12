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
                // 1. monto de mora
                decimal montoMora = milinq.Configuracion
                                           .Select(c => c.monto_mora)
                                           .FirstOrDefault();

                // 2. Agregado por factura: suma de cuotas y fechas para saber si hay atraso
                var facturasAgg = (
                    from contrato in milinq.Contrato
                    where contrato.id_contrato == idContrato

                    join servicio in milinq.Servicio
                        on contrato.id_contrato equals servicio.id_contrato

                    join cuota in milinq.Cuota
                        on servicio.id_servicio equals cuota.id_servicio

                    join factura in milinq.Factura
                        on cuota.id_cuota equals factura.id_cuota

                    join pago in milinq.Pago
                        on factura.id_pago equals pago.id_pago

                    group new { cuota, pago } by factura.id_factura into g
                    select new
                    {
                        IdFactura = g.Key,
                        TotalCuotas = g.Sum(x => x.cuota.total),
                        FechaVencimiento = g.Min(x => x.cuota.fecha_vencimiento),
                        FechaPago = g.Max(x => x.pago.fecha_pago)
                    }
                ).ToList();

                var idsFacturas = facturasAgg.Select(f => f.IdFactura).Distinct().ToList();

                var facturasEntities = milinq.Factura
                                             .Where(f => idsFacturas.Contains(f.id_factura))
                                             .ToList();

                var facturasDict = facturasEntities
                    .ToDictionary(f => f.id_factura, f => f);

                var infoFacturaDict = new Dictionary<string, InfoFacturaAux>();

                // 3. Actualizar total_factura si corresponde aplicar mora
                foreach (var fAgg in facturasAgg)
                {
                    if (!facturasDict.TryGetValue(fAgg.IdFactura, out var facturaEntity))
                        continue;

                    bool pagoAtrasado = fAgg.FechaPago.Date > fAgg.FechaVencimiento.Date;

                    // Si está atrasado y todavía no tiene mora (total == suma cuotas)
                    if (pagoAtrasado && facturaEntity.total_factura == fAgg.TotalCuotas)
                    {
                        facturaEntity.total_factura = fAgg.TotalCuotas + montoMora;
                    }

                    decimal moraAplicada =
                        Math.Max(0m, facturaEntity.total_factura - fAgg.TotalCuotas);

                    infoFacturaDict[fAgg.IdFactura] = new InfoFacturaAux
                    {
                        PagoAtrasado = pagoAtrasado,
                        MoraAplicada = moraAplicada
                    };
                }

                // guardar cambios de factura (ya con mora)
                milinq.SubmitChanges();

                // 4. Query base para el grid, incluyendo TIPO DE SERVICIO
                var queryBase =
                    from contrato in milinq.Contrato
                    where contrato.id_contrato == idContrato

                    join servicio in milinq.Servicio
                        on contrato.id_contrato equals servicio.id_contrato

                    join tipoServicio in milinq.Tipo_servicio
                        on servicio.id_tipo_servicio equals tipoServicio.id_tipo_servicio

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

                        TipoServicio = tipoServicio.nombre_tipo_servicio,
                        NumeroCuota = 0, // se asigna después

                        IdFactura = factura != null ? factura.id_factura : null,
                        IdPago = pago != null ? pago.id_pago : null,

                        FechaVencimientoCuota = cuota.fecha_vencimiento,
                        FechaPago = pago != null ? (DateTime?)pago.fecha_pago : null,

                        EstadoPago =
                            pago == null ? "Sin pago" :
                            pago.id_estado_pago == "EPA001" ? "Completo" :
                            pago.id_estado_pago == "EPA002" ? "Pendiente" :
                            (estadoPago != null ? estadoPago.nombre_estado_pago : "Desconocido"),

                        MontoCuota = cuota.total,
                        MontoFactura = factura != null ? factura.total_factura : 0m,
                        MontoPago = pago != null ? pago.sub_total : 0m,

                        PagoAtrasado = false,
                        MoraAplicada = 0m,
                        TotalConMora = 0m,
                        BalanceForward = 0m
                    };

                var lista = queryBase
                    .OrderBy(x => x.FechaVencimientoCuota)
                    .ThenBy(x => x.FechaPago ?? x.FechaVencimientoCuota)
                    .ToList();

                // 5. Asignar número de cuota POR SERVICIO (1,2,3,...) — aquí corregimos el 0
                var contadorPorServicio = new Dictionary<string, int>();

                foreach (var item in lista)
                {
                    if (!contadorPorServicio.TryGetValue(item.IdServicio, out int n))
                        n = 0;

                    n++;
                    contadorPorServicio[item.IdServicio] = n;

                    item.NumeroCuota = n;
                }

                // 6. Calcular mora visible por fila y balance acumulado mes a mes
                decimal saldoAcumulado = 0m;
                var facturasMoraMostrada = new HashSet<string>();
                var facturasPagoAplicado = new HashSet<string>();

                foreach (var item in lista)
                {
                    item.TotalConMora = item.MontoFactura; // la factura ya viene con mora

                    decimal moraFila = 0m;
                    decimal pagoFila = 0m;

                    if (!string.IsNullOrEmpty(item.IdFactura) &&
                        infoFacturaDict.TryGetValue(item.IdFactura, out var info))
                    {
                        item.PagoAtrasado = info.PagoAtrasado;

                        if (info.MoraAplicada > 0m &&
                            !facturasMoraMostrada.Contains(item.IdFactura))
                        {
                            item.MoraAplicada = info.MoraAplicada;
                            moraFila = info.MoraAplicada;
                            facturasMoraMostrada.Add(item.IdFactura);
                        }
                    }

                    // aplicar el pago solo una vez por factura al saldo acumulado
                    if (!string.IsNullOrEmpty(item.IdFactura) &&
                        item.MontoPago > 0m &&
                        !facturasPagoAplicado.Contains(item.IdFactura))
                    {
                        pagoFila = item.MontoPago;
                        facturasPagoAplicado.Add(item.IdFactura);
                    }

                    // saldo acumulado de TODO el contrato (varios servicios y meses)
                    saldoAcumulado = saldoAcumulado
                                     + item.MontoCuota   // cuota de ese servicio
                                     + moraFila          // mora (una vez por factura)
                                     - pagoFila;         // pago (una vez por factura)

                    item.BalanceForward = saldoAcumulado;
                }

                return lista;
            }
        }
    }

    public class InfoFacturaAux
    {
        public bool PagoAtrasado { get; set; }
        public decimal MoraAplicada { get; set; }
    }


public class PagoGridDTO
{
    public string IdContrato { get; set; }

    // Lo puedes dejar solo para uso interno y ocultar la columna en el Grid
    public string IdServicio { get; set; }

    public string TipoServicio { get; set; }   // Internet, Cable, etc.
    public int NumeroCuota { get; set; }       // 1, 2, 3, ... por servicio

    public string IdCuota { get; set; }
    public string IdFactura { get; set; }
    public string IdPago { get; set; }

    public DateTime FechaVencimientoCuota { get; set; }
    public DateTime? FechaPago { get; set; }

    public string EstadoPago { get; set; }      // Completo / Pendiente / Sin pago
    public bool PagoAtrasado { get; set; }

    public decimal MontoCuota { get; set; }     // Cuota.total
    public decimal MontoFactura { get; set; }   // Factura.total_factura (ya con mora)
    public decimal MontoPago { get; set; }      // Pago.sub_total

    public decimal MoraAplicada { get; set; }   // Solo 1 vez por factura
    public decimal TotalConMora { get; set; }   // Igual a MontoFactura

    public decimal BalanceForward { get; set; } // Saldo acumulado DESPUÉS de esta fila
}

}

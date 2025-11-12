using ExamenFinalBD.BD;
using ExamenFinalBD.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq; 

public class ContratoDAO
{
    General general = new General();
    DBLinQ milinq;

    public ContratoDAO()
    {
        milinq = new DBLinQ(general.CadenaConexion());
    }

    private string GenerarSiguienteIdContrato()
    {
        string ultimoId = milinq.Contrato
                                .OrderByDescending(c => c.id_contrato)
                                .Select(c => c.id_contrato)
                                .FirstOrDefault();

        if (string.IsNullOrEmpty(ultimoId))
        {
            return "CONT001"; 
        }

        if (ultimoId.Length < 4)
        {
            return "CONT001";
        }

        string parteNumericaStr = ultimoId.Substring(4);

        if (int.TryParse(parteNumericaStr, out int numeroActual))
        {
            int siguienteNumero = numeroActual + 1;
            string siguienteNumeroStr = siguienteNumero.ToString("D3");
            return "CONT" + siguienteNumeroStr;
        }

        throw new InvalidOperationException("El formato del último ID de contrato es incorrecto. No se pudo parsear la parte numérica.");
    }

    public bool InsertarContrato(Contrato nuevoContrato)
    {
        try
        {
            if (string.IsNullOrEmpty(nuevoContrato.id_contrato))
            {
                nuevoContrato.id_contrato = GenerarSiguienteIdContrato();
            }

            milinq.Contrato.InsertOnSubmit(nuevoContrato);
            milinq.SubmitChanges();
            return true;
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("Error de formato al generar el ID: " + ex.Message);
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al insertar contrato: " + ex.Message);
            return false;
        }
    }

    public Contrato ObtenerContratoPorID(string id)
    {
        return milinq.Contrato.SingleOrDefault(c => c.id_contrato == id);
    }

    public List<Contrato> ObtenerTodosLosContratos()
    {
        return milinq.Contrato.ToList();
    }

    public Contrato ObtenerUltimoContratoAnadido()
    {
        return milinq.Contrato
                        .OrderByDescending(c => c.id_contrato)
                        .FirstOrDefault();
    }

    public bool ActualizarContrato(Contrato contratoModificado)
    {
        try
        {
            Contrato contratoExistente = milinq.Contrato.SingleOrDefault(c => c.id_contrato == contratoModificado.id_contrato);

            if (contratoExistente != null)
            {
                contratoExistente.id_info_cobro = contratoModificado.id_info_cobro;
                contratoExistente.id_municipio = contratoModificado.id_municipio;
                contratoExistente.id_cliente = contratoModificado.id_cliente;
                contratoExistente.id_estado_contrato = contratoModificado.id_estado_contrato;
                contratoExistente.id_usuario = contratoModificado.id_usuario;
                contratoExistente.fecha_inicio = contratoModificado.fecha_inicio;
                contratoExistente.fecha_fin = contratoModificado.fecha_fin;
                contratoExistente.direccion_instalacion = contratoModificado.direccion_instalacion;
                contratoExistente.saldo_total = contratoModificado.saldo_total;
                contratoExistente.monto_cuota = contratoModificado.monto_cuota;
                contratoExistente.meses = contratoModificado.meses;

                milinq.SubmitChanges();
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al actualizar contrato: " + ex.Message);
            return false;
        }
    }
        
    public bool EliminarContrato(string id)
    {
        try
        {
            Contrato contratoAEliminar = milinq.Contrato.SingleOrDefault(c => c.id_contrato == id);

            if (contratoAEliminar != null)
            {
                milinq.Contrato.DeleteOnSubmit(contratoAEliminar);
                milinq.SubmitChanges();
                return true;
            }
            return false;
        }
        catch (ChangeConflictException ex)
        {
            milinq.ChangeConflicts.ResolveAll(RefreshMode.OverwriteCurrentValues);
            milinq.SubmitChanges();
            Console.WriteLine("Conflicto al eliminar contrato: " + ex.Message);
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al eliminar contrato: " + ex.Message);
            return false;
        }
    }

    public List<string> ObtenerIdsContratosPorCliente(string idCliente)
    {
        try
        {
            return milinq.Contrato
                         .Where(c => c.id_cliente == idCliente)
                         .Select(c => c.id_contrato)
                         .ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al obtener IDs de contratos por cliente: " + ex.Message);
            return new List<string>(); 
        }
    }


}
using ExamenFinalBD.BD;
using ExamenFinalBD.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;

public class ClienteDAO
{
    General general = new General();
    DBLinQ milinq;

    public ClienteDAO()
    {
        milinq = new DBLinQ(general.CadenaConexion());
    }
    private string GenerarSiguienteIdCliente()
    {
        string ultimoId = milinq.Cliente
                                   .OrderByDescending(c => c.id_cliente)
                                   .Select(c => c.id_cliente)
                                   .FirstOrDefault();

        if (string.IsNullOrEmpty(ultimoId))
        {
            return "CLI001";
        }

        string parteNumericaStr = ultimoId.Substring(3);

        if (int.TryParse(parteNumericaStr, out int numeroActual))
        {
            int siguienteNumero = numeroActual + 1;
            string siguienteNumeroStr = siguienteNumero.ToString("D3");
            return "CLI" + siguienteNumeroStr;
        }

        throw new InvalidOperationException("El formato del último ID de cliente es incorrecto.");
    }
    public bool InsertarCliente(Cliente nuevoCliente)
    {
        try
        {
            if (string.IsNullOrEmpty(nuevoCliente.id_cliente))
            {
                nuevoCliente.id_cliente = GenerarSiguienteIdCliente();
            }
            milinq.Cliente.InsertOnSubmit(nuevoCliente);
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
            Console.WriteLine("Error al insertar cliente: " + ex.Message);
            return false;
        }
    }

    public Cliente ObtenerClientePorID(string id)
    {
        return milinq.Cliente.SingleOrDefault(c => c.id_cliente == id);
    }

    public Cliente ObtenerClientePorDPI(string dpi)
    {
        return milinq.Cliente.FirstOrDefault(c => c.dpi == dpi);
    }

    public List<Cliente> ObtenerTodosLosClientes()
    {
        return milinq.Cliente.ToList();
    }

    public Cliente ObtenerUltimoClienteAnadido()
    {
        return milinq.Cliente
                        .OrderByDescending(c => c.id_cliente)
                        .FirstOrDefault();
    }
    public bool ActualizarCliente(Cliente clienteModificado)
    {
        try
        {
            Cliente clienteExistente = milinq.Cliente.SingleOrDefault(c => c.id_cliente == clienteModificado.id_cliente);

            if (clienteExistente != null)
            {
                clienteExistente.dpi = clienteModificado.dpi;
                clienteExistente.nombre = clienteModificado.nombre;
                clienteExistente.telefono_primario = clienteModificado.telefono_primario;
                clienteExistente.telefono_secundario = clienteModificado.telefono_secundario;
                clienteExistente.email = clienteModificado.email;

                milinq.SubmitChanges();
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al actualizar cliente: " + ex.Message);
            return false;
        }
    }

    public bool EliminarCliente(string id)
    {
        try
        {
            Cliente clienteAEliminar = milinq.Cliente.SingleOrDefault(c => c.id_cliente == id);

            if (clienteAEliminar != null)
            {
                milinq.Cliente.DeleteOnSubmit(clienteAEliminar);
                milinq.SubmitChanges();
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al eliminar cliente: " + ex.Message);
            return false;
        }
    }
}
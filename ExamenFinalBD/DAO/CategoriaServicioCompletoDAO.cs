using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ExamenFinalBD.BD;
using ExamenFinalBD.Utils;

namespace ProyectoTelecom.DataAccessObjects
{
    public class CategoriaServicioCompletoDAO
    {
        General general = new General();

        /// <summary>
        /// Obtiene los detalles de una categoría de servicio específica, incluyendo
        /// precio, tipo de servicio, canales, velocidad y minutos.
        /// </summary>
        public CategoriaServicioDetalleDTO ObtenerDetalleCategoria(string idCategoria)
        {
            try
            {
                using (DBLinQ milinq = new DBLinQ(general.CadenaConexion()))
                {
                    var categoria = (from cat in milinq.Categoria_servicio
                                     where cat.id_categoria_servicio == idCategoria
                                     select cat).FirstOrDefault();

                    if (categoria == null)
                    {
                        return null; // Categoría no encontrada
                    }

                    // 1. Obtener la cantidad de canales
                    int canales = (from ccs in milinq.Canal_categoria_servicio
                                   where ccs.id_categoria_servicio == idCategoria
                                   select ccs.id_canal).Distinct().Count();

                    // 2. Obtener la velocidad de Internet (Si aplica)
                    string detalleVelocidad = null;
                    if (!string.IsNullOrEmpty(categoria.id_velocidad_internet))
                    {
                        var vel = milinq.Velocidad_internet
                                        .FirstOrDefault(v => v.id_velocidad_internet == categoria.id_velocidad_internet);

                        if (vel != null)
                        {
                            detalleVelocidad = $"{vel.velocidad_descarga} / {vel.velocidad_subida} Mbps";
                        }
                    }

                    // 3. Obtener la cantidad de minutos (Si aplica)
                    string detalleMinutos = null;
                    if (!string.IsNullOrEmpty(categoria.id_disponibilidad_telefono))
                    {
                        var min = milinq.Disponibilidad_telefono
                                        .FirstOrDefault(d => d.id_disponibilidad_telefono == categoria.id_disponibilidad_telefono);

                        if (min != null)
                        {
                            detalleMinutos = $"{min.cantidad_minutos} Minutos";
                        }
                    }

                    // 4. Determinar el Tipo de Servicio
                    string tipoServicio = DeterminarTipoServicio(categoria.id_velocidad_internet, categoria.id_disponibilidad_telefono);

                    // 5. Construir el DTO de detalle
                    return new CategoriaServicioDetalleDTO
                    {
                        IdCategoriaServicio = categoria.id_categoria_servicio,
                        NombreCategoriaServicio = categoria.nombre_categoria_servicio,
                        Precio = Convert.ToDecimal(categoria.precio),
                        TipoServicio = tipoServicio,
                        CantidadCanales = canales,
                        DetalleVelocidad = detalleVelocidad,
                        DetalleMinutos = detalleMinutos
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el detalle de la categoría: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        // --- Método Auxiliar DeterminarTipoServicio (Reutilizado) ---

        /// <summary>
        /// Método privado para determinar el tipo de servicio basado en la existencia de IDs.
        /// </summary>
        private string DeterminarTipoServicio(string idVelocidadInternet, string idDisponibilidadTelefono)
        {
            bool tieneInternet = !string.IsNullOrEmpty(idVelocidadInternet);
            bool tieneTelefono = !string.IsNullOrEmpty(idDisponibilidadTelefono);

            if (tieneInternet && tieneTelefono)
            {
                return "Combinado";
            }
            else if (tieneInternet)
            {
                return "Internet";
            }
            else if (tieneTelefono)
            {
                return "Teléfono";
            }
            else
            {
                return "Cable";
            }
        }

        // --- Inserción (Mantenido) ---

        // Si necesitas el método de Inserción, debes agregarlo aquí, 
        // usando el DTO de entrada simple CategoriaServicioInsertDTO.
        /*
        public bool InsertarCategoria(CategoriaServicioInsertDTO datos)
        {
            // ... (Lógica de inserción anterior) ...
        }
        */
    }

    public class CategoriaServicioDetalleDTO
    {
        public string IdCategoriaServicio { get; set; }
        public string NombreCategoriaServicio { get; set; }
        public decimal Precio { get; set; }

        // Campos de resumen y detalle
        public string TipoServicio { get; set; } // Cable, Internet, Teléfono, Combinado
        public int CantidadCanales { get; set; }
        public string DetalleVelocidad { get; set; } // Ej: 100/50 Mbps (Subida/Descarga)
        public string DetalleMinutos { get; set; } // Ej: 500 Minutos
    }
}
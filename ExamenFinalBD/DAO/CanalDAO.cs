using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ExamenFinalBD.BD;
using ExamenFinalBD.Utils;
namespace ProyectoTelecom.DataAccessObjects
{
    public class CanalDAO
    {
        General general = new General();
        /// <summary>
        /// Obtiene la cantidad total de canales asociados a una categoría específica.
        /// </summary>
        public int ContarCanalesPorCategoria(string idCategoria)
        {
            try
            {
                using (DBLinQ milinq = new DBLinQ(general.CadenaConexion()))
                {
                    var conteo = (from ccs in milinq.Canal_categoria_servicio
                                  where ccs.id_categoria_servicio == idCategoria
                                  select ccs.id_canal).Distinct().Count();

                    return conteo;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al contar los canales: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        /// <summary>
        /// Consulta los canales asociados a una categoría específica y los vincula a un ComboBox o GridControl.
        /// </summary>
        public void ConsultarCanalesPorCategoria(string idCategoria, ComboBox comboBoxDestino)
        {
            try
            {
                using (DBLinQ milinq = new DBLinQ(general.CadenaConexion()))
                {
                    var consulta = (from ccs in milinq.Canal_categoria_servicio
                                    join c in milinq.Canal on ccs.id_canal equals c.id_canal
                                    where ccs.id_categoria_servicio == idCategoria
                                    orderby c.numero_canal
                                    select new
                                    {
                                        ID = c.id_canal,
                                        Nombre = c.nombre_canal,
                                        Numero = c.numero_canal
                                    }).ToList();

                    comboBoxDestino.DataSource = consulta;
                    comboBoxDestino.ValueMember = "ID";
                    comboBoxDestino.DisplayMember = "Nombre";
                    comboBoxDestino.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar canales: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Devuelve una lista de canales (nombre y número) asociados a una categoría.
        /// </summary>
        public List<CanalDTO> ObtenerListaCanalesPorCategoria(string idCategoria)
        {
            try
            {
                using (DBLinQ milinq = new DBLinQ(general.CadenaConexion()))
                {
                    var lista = (from ccs in milinq.Canal_categoria_servicio
                                 join c in milinq.Canal on ccs.id_canal equals c.id_canal
                                 where ccs.id_categoria_servicio == idCategoria
                                 orderby c.numero_canal
                                 select new CanalDTO
                                 {
                                     IdCanal = c.id_canal,
                                     NombreCanal = c.nombre_canal,
                                     NumeroCanal = c.numero_canal
                                 }).ToList();

                    return lista;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener lista de canales: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<CanalDTO>();
            }
        }
    }

    /// <summary>
    /// Objeto de transferencia para mostrar información básica de canales.
    /// </summary>
    public class CanalDTO
    {
        public string IdCanal { get; set; }
        public string NombreCanal { get; set; }
        public int NumeroCanal { get; set; }
    }
}

using DevExpress.XtraEditors;
using DevExpress.XtraPrinting.Localization;
using DevExpress.XtraPrinting.Shape.Native;
using ExamenFinalBD.Tecnico.BD;
using ExamenFinalBD.Tecnico.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamenFinalBD.Tecnico
{
    public partial class CreaReportes : DevExpress.XtraEditors.XtraForm
    {
        BD.LinqDataContext miLinq=new BD.LinqDataContext(General.cadena);
        private General DatosGenerales;
         DataTable tablaDetalle=new DataTable();
        public CreaReportes()
        {
            InitializeComponent();
            
        }

        private void simpleButtonBucaContrato_Click(object sender, EventArgs e)
        {
            if(textEditNoContrato.Text=="")
            {
                redireccionaContrato();
            }
            else
            {
                var consulta = from contra in miLinq.Contrato
                               where  contra.id_contrato==textEditNoContrato.Text
                               select new
                               {
                                   ID=contra.id_contrato,
                                   Nombre=contra.Cliente.nombre,
                                   Direccion=contra.direccion_instalacion
                               };
                if(consulta!=null)
                {
                    textEditDireccion.Text = consulta.FirstOrDefault().Direccion.ToString();
                    textEditNombCLiente.Text=consulta.FirstOrDefault().Nombre.ToString();
                }
                else
                {
                    MessageBox.Show("No se encontro el numero de contrato","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }

        }


        public void redireccionaContrato()
        {
            using (BuscaContrato busca = new BuscaContrato())
            {
                if (busca.ShowDialog() == DialogResult.OK)
                {
                    DatosGenerales = busca.ContratoSelect;
                    textEditNoContrato.Text =DatosGenerales.NoContrato;
                    textEditNombCLiente.Text = DatosGenerales.Nombre;
                    textEditDireccion.Text=DatosGenerales.Direccion;
                }
            }
        }

        private void CreaReportes_Load(object sender, EventArgs e)
        {
            string preBita = "BIT";
            var bita = from bta in miLinq.Bitacora
                       select bta.id_bitacora;
            string ultBta = bita.Max();
            string nbt = "";
            if (ultBta == null)
            {
                nbt = "BIT001";
            }
            else
            {
                nbt ="BIT"+(Convert.ToInt32(ultBta.Substring(3,3)) + 1).ToString("D3");
            }
            textEditNoBitacora.Text = nbt;
            var vista = from vst in miLinq.Visita_tecnica
                       select vst.id_visita_tecnica;
            string ultvst = bita.Max();
            string nvst = "";
            if (ultvst == null)
            {
                nvst = "001";
            }
            else
            {
                nvst ="VIS"+(Convert.ToInt32(ultvst.Substring(3,3)) + 1).ToString("D3");
            }
            textEditIDVisita.Text = nvst;

            tablaDetalle.Columns.Add("Codigo");
            tablaDetalle.Columns.Add("Repuesto");
            tablaDetalle.Columns.Add("Precio");
            tablaDetalle.Columns.Add("Cantidad");
            tablaDetalle.Columns.Add("Subtotal");
            gridControlRepuesto.DataSource = tablaDetalle;
        }
        

        private void simpleButtonAgregaRepuesto_Click(object sender, EventArgs e)
        {
            bool bandera = false;
            if (textEditCodigoRepuesto.Text=="")
            {
                using (BuscarRepuesto busca = new BuscarRepuesto())
                {
                    if (busca.ShowDialog() == DialogResult.OK)
                    {
                        DatosGenerales = busca.repuestoSelect;
                        textEditCodigoRepuesto.Text = DatosGenerales.IdRepuesto;
                    }
                }
            }
            else
            {
                var consulta = from repu in miLinq.Repuesto
                               where repu.id_respuesto == textEditCodigoRepuesto.Text
                               select new { 
                                   Codigo=repu.id_respuesto,
                                   Nombre=repu.nombre_repuesto,
                                   Precio=repu.precio_repuesto
                               };

                DataRow nuevaFila = tablaDetalle.NewRow();
                if (consulta == null)
                {
                    MessageBox.Show("No se encontro el repuesto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if(spinEditCantidad.Value<=0)
                    {
                        MessageBox.Show("Ingrese una cantidad valida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        foreach (var fila in consulta)
                        {
                            if (tablaDetalle.Rows.Count == 0)
                            {
                                nuevaFila = tablaDetalle.NewRow();
                                nuevaFila[0] = fila.Codigo;
                                nuevaFila[1] = fila.Nombre;
                                nuevaFila[2] = fila.Precio;
                                nuevaFila[3] = spinEditCantidad.Value;
                                nuevaFila[4] = fila.Precio * Convert.ToDecimal(spinEditCantidad.Value);
                                tablaDetalle.Rows.Add(nuevaFila);
                            }
                            else//para comprobar existencias 
                            {
                                foreach(DataRow filasDet in tablaDetalle.Rows)
                                {
                                    if (fila.Codigo == filasDet[0].ToString())
                                    {
                                        filasDet[3] = (Convert.ToInt32(filasDet[3].ToString()) + spinEditCantidad.Value);
                                        filasDet[4] = Convert.ToInt32(filasDet[3].ToString()) * Convert.ToDecimal(fila.Precio.ToString());
                                        bandera = true; break;
                                    }
                                }
                                if (!bandera)
                                {
                                    nuevaFila = tablaDetalle.NewRow();
                                    nuevaFila[0] = fila.Codigo;
                                    nuevaFila[1] = fila.Nombre;
                                    nuevaFila[2] = fila.Precio;
                                    nuevaFila[3] = spinEditCantidad.Value;
                                    nuevaFila[4] = fila.Precio * Convert.ToDecimal(spinEditCantidad.Value);
                                    tablaDetalle.Rows.Add(nuevaFila);
                                }
                            }

                        }
                        spinEditCantidad.Value = 0;
                        textEditCodigoRepuesto.Text = "";
                        decimal total = 0;
                        foreach(DataRow fila in tablaDetalle.Rows)
                        {
                            total += Convert.ToDecimal(fila[4].ToString());
                        }
                        labelControlTotalCompra.Text= total.ToString();
                    }
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(textEditIdTecnico.Text!="")
            { 
                var consulta=from Tecnico in miLinq.Tecnico
                             where Tecnico.id_tecnico==textEditIdTecnico.Text
                             select Tecnico;
                if(consulta!=null)
                {
                    textEditNombTecnico.Text=consulta.FirstOrDefault().nombre_tecnico;
                }
                else
                {
                    MessageBox.Show("No se encontro al tecnico", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
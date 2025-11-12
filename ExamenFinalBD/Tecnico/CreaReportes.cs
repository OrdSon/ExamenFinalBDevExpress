using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Localization;
using DevExpress.XtraPrinting.Shape.Native;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraReports.UI;
using ExamenFinalBD.Tecnico.BD;
using ExamenFinalBD.Tecnico.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
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
                    textEditNoContrato.Enabled=simpleButtonBucaContrato.Enabled=false;
                }
            }
        }

        private void CreaReportes_Load(object sender, EventArgs e)
        {
            var conslutaTfalla=from falla in miLinq.Tipo_falla
                               select new
                               {
                                   ID=falla.id_tipo_falla,
                                   Tipo=falla.nombre_falla
                               };
            lookUpEditTipoFalla.Properties.DataSource = conslutaTfalla;
            lookUpEditTipoFalla.Properties.DisplayMember = "Tipo";
            lookUpEditTipoFalla.Properties.ValueMember = "ID";
            asigna();
            tablaDetalle.Columns.Add("Codigo");
            tablaDetalle.Columns.Add("Repuesto");
            tablaDetalle.Columns.Add("Precio");
            tablaDetalle.Columns.Add("Cantidad");
            tablaDetalle.Columns.Add("Subtotal");
            tablaDetalle.Columns.Add("Costo");
            gridControlRepuesto.DataSource = tablaDetalle;
            textEditNoBitacora.Enabled=textEditDireccion.Enabled=textEditNombCLiente.Enabled=textEditNombTecnico.Enabled=textEditIDVisita.Enabled=false;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.Columns["Costo"].Visible = false;
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
                                   Precio=repu.precio_repuesto,
                                   Costo=repu.costo_repuesto
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
                                nuevaFila[5] = fila.Costo;
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
                                    nuevaFila[5] = fila.Costo;
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
        public void asigna()
        {
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
                nbt = "BIT" + (Convert.ToInt32(ultBta.Substring(3, 3)) + 1).ToString("D3");
            }
            textEditNoBitacora.Text = nbt;
            var vista = from vst in miLinq.Visita_tecnica
                        select vst.id_visita_tecnica;
            string ultvst = vista.Max();
            string nvst = "";
            if (ultvst == null)
            {
                nvst = "001";
            }
            else
            {
                nvst = "VIS" + (Convert.ToInt32(ultvst.Substring(3, 3)) + 1).ToString("D3");
            }
            textEditIDVisita.Text = nvst;
        }
        public void limpia()
        {
            textEditNoContrato.Enabled = textEditIdTecnico.Enabled=simpleButtonBucaContrato.Enabled=simpleButton1.Enabled = true;
            textEditNoContrato.Text=textEditNombCLiente.Text=textEditDireccion.Text=textEditIdTecnico.Text=textEditNombTecnico.Text="";
            memoEditDetalle.Text=memoEditSolucion.Text="";
            tablaDetalle.Rows.Clear();
            labelControlTotalCompra.Text = "0";
            asigna();
            dateEditEjecucion.EditValue = dateEditFSolicitud.EditValue = null;
            lookUpEditTipoFalla.EditValue = null;
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
                    textEditIdTecnico.Enabled=false;
                    simpleButton1.Enabled=false;
                }
                else
                {
                    MessageBox.Show("No se encontro al tecnico", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void simpleButtonPresentar_Click(object sender, EventArgs e)
        {
            if(textEditNoContrato.Text!="")
            {
                if (dateEditFSolicitud.EditValue != null)
                {
                    if (lookUpEditTipoFalla.EditValue!=null)
                    {
                        if (textEditIdTecnico.Text != "")
                        {
                            if (!RETOMA)
                            {
                                using (TransactionScope transa = new TransactionScope())
                                {
                                    bool bandera=false;
                                    try
                                    {
                                        // ingresa la visita tecnica
                                        var consultaPeloton = from pel in miLinq.Peloton
                                                              where pel.id_tecnico == textEditIdTecnico.Text
                                                              select pel.id_peloton;
                                        DateTime? Fejecucion = null;
                                        if (dateEditEjecucion.EditValue != null)
                                        {
                                            Fejecucion = Convert.ToDateTime(dateEditEjecucion.EditValue);
                                        }
                                        BD.Visita_tecnica nuevaVista = new BD.Visita_tecnica
                                        {
                                            id_visita_tecnica = textEditIDVisita.Text,
                                            id_contrato = textEditNoContrato.Text,
                                            id_peloton = consultaPeloton.FirstOrDefault().ToString(),
                                            fecha_solicitud = Convert.ToDateTime(dateEditFSolicitud.EditValue),
                                            fecha_ejecucion = Fejecucion,
                                        };
                                        miLinq.Visita_tecnica.InsertOnSubmit(nuevaVista);
                                        //ingresa la falla
                                        var falla = from fll in miLinq.Falla
                                                    select fll.id_falla;
                                        string ultfll = falla.Max();
                                        string nvfll = "";
                                        if (ultfll == null)
                                        {
                                            nvfll = "FAL001";
                                        }
                                        else
                                        {
                                            nvfll = "FAL" + (Convert.ToInt32(ultfll.Substring(3, 3)) + 1).ToString("D3");
                                        }
                                        var servicio = from serv in miLinq.Servicio
                                                       where serv.id_contrato == textEditNoContrato.Text
                                                       select serv.id_tipo_servicio;

                                        BD.Falla nuevaFalla = new BD.Falla
                                        {
                                            id_visita_tecnica = textEditIDVisita.Text,
                                            id_tipo_servicio = servicio.FirstOrDefault().ToString(),
                                            id_tipo_falla = lookUpEditTipoFalla.EditValue.ToString(),
                                            id_estado_falla = "EFA001",
                                            id_falla = nvfll
                                        };
                                        miLinq.Falla.InsertOnSubmit(nuevaFalla);
                                        miLinq.SubmitChanges();

                                        //el repuesto de la falla
                                        var rep = from repu in miLinq.repuesto_falla
                                                  select repu.id_repuesto_falla;
                                        string ultrep = rep.Max();
                                        string nvrep = "";
                                        if (ultrep == null)
                                        {
                                            nvrep = "RF001";
                                        }

                                        int cont = 1;


                                        foreach (DataRow fila in tablaDetalle.Rows)
                                        {
                                            nvrep = "RF" + (Convert.ToInt32(ultrep.Substring(3, 3)) + cont).ToString("D3");
                                            BD.repuesto_falla repu = new BD.repuesto_falla
                                            {
                                                id_repuesto_falla = nvrep,
                                                id_respuesto = fila[0].ToString(),
                                                id_falla = nvfll,
                                                costo_repuesto_falla = Convert.ToDecimal(fila[5].ToString()),
                                                precio_repuesto_falla = Convert.ToDecimal(fila[2].ToString())
                                            };
                                            cont++;
                                            miLinq.repuesto_falla.InsertOnSubmit(repu);
                                        }
                                        //Ingresa la bitacora
                                        BD.Bitacora nuevaBitacora = new BD.Bitacora
                                        {
                                            id_bitacora = textEditNoBitacora.Text,
                                            id_falla = nvfll,
                                            id_peloton = consultaPeloton.FirstOrDefault().ToString(),
                                            id_visita_tecnica = textEditIDVisita.Text
                                        };
                                        miLinq.Bitacora.InsertOnSubmit(nuevaBitacora);
                                        miLinq.SubmitChanges();
                                        MessageBox.Show("Los datos se ingresaron correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        limpia();
                                        transa.Complete();
                                    }
                                    catch
                                    {
                                        MessageBox.Show("Ocurrio un error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        transa.Dispose();
                                    }

                                }
                            }
                            else
                            {
                                if (dateEditEjecucion.EditValue != null)
                                {
                                        string idfalla = miLinq.Falla.Where(f => f.id_visita_tecnica == textEditIDVisita.Text).FirstOrDefault().id_falla;
                                        BD.Bitacora nuevaBitacora = new BD.Bitacora
                                        {
                                            id_bitacora = textEditNoBitacora.Text,
                                            id_falla = idfalla,
                                            id_peloton = miLinq.Peloton.Where(p => p.id_tecnico == textEditIdTecnico.Text).FirstOrDefault().id_peloton,
                                            id_visita_tecnica = textEditIDVisita.Text
                                        };
                                        miLinq.Bitacora.InsertOnSubmit(nuevaBitacora);
                                        //el repuesto de la falla

                                        var rep = from repu in miLinq.repuesto_falla
                                                  select repu.id_repuesto_falla;
                                        string ultrep = rep.Max();
                                        string nvrep = "";
                                        if (ultrep == null)
                                        {
                                            nvrep = "RF001";
                                        }

                                        int cont = 1;


                                        foreach (DataRow fila in tablaDetalle.Rows)
                                        {
                                            nvrep = "RF" + (Convert.ToInt32(ultrep.Substring(3, 3)) + cont).ToString("D3");
                                            BD.repuesto_falla repu = new BD.repuesto_falla
                                            {
                                                id_repuesto_falla = nvrep,
                                                id_respuesto = fila[0].ToString(),
                                                id_falla = idfalla,
                                                costo_repuesto_falla = Convert.ToDecimal(fila[5].ToString()),
                                                precio_repuesto_falla = Convert.ToDecimal(fila[2].ToString())
                                            };
                                            cont++;
                                            miLinq.repuesto_falla.InsertOnSubmit(repu);
                                        }
                                        miLinq.SubmitChanges();
                                        using (var db = new BD.LinqDataContext(General.cadena))
                                        {

                                            var visita = db.Visita_tecnica.FirstOrDefault(v => v.id_visita_tecnica == textEditIDVisita.Text);

                                            if (visita != null)
                                            {

                                                visita.fecha_ejecucion = Convert.ToDateTime(dateEditEjecucion.EditValue);
                                                visita.id_contrato = textEditNoContrato.Text;

                                                db.SubmitChanges();
                                            }
                                        }
                                    limpia();
                                    MessageBox.Show("Los datos se actualizaron correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    RETOMA = false;
                                }
                                else
                                {
                                    MessageBox.Show("Ingrese una fecha de ejecucion", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ingrese un tecnico", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Seleccione una falla", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese una fecha en la solicitud", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                {
                    MessageBox.Show("Ingrese un numero de contrato", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void simpleButtonCancelar_Click(object sender, EventArgs e)
        {
           if(MessageBox.Show("¿Esta seguro de canselar la operacion?", "Advertencia", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)==DialogResult.OK)
            {
                limpia();
            }
        }
        bool RETOMA = false;


        private void simpleButtonImprimir_Click(object sender, EventArgs e)
        {
            XtraReport report = new XtraReport();
            report.PaperKind = System.Drawing.Printing.PaperKind.A4;
            report.Margins = new Margins(50, 50, 50, 50);

            DetailBand detail = new DetailBand();
            report.Bands.Add(detail);

            int y = 0;

            // Campos del formulario
            detail.Controls.Add(CreateLabel("Contrato: " + textEditNoContrato.Text, ref y));
            detail.Controls.Add(CreateLabel("Bitácora: " + textEditNoBitacora.Text, ref y));
            detail.Controls.Add(CreateLabel("Cliente: " + textEditNombCLiente.Text, ref y));
            detail.Controls.Add(CreateLabel("Dirección: " + textEditDireccion.Text, ref y));

            detail.Controls.Add(CreateLabel("Fecha Solicitud: " + dateEditFSolicitud.Text, ref y));
            detail.Controls.Add(CreateLabel("Fecha Ejecución: " + dateEditEjecucion.Text, ref y));
            detail.Controls.Add(CreateLabel("Tipo de Falla: " + lookUpEditTipoFalla.Text, ref y));

            detail.Controls.Add(CreateLabel("Detalle: " + memoEditDetalle.Text, ref y, 40));
            detail.Controls.Add(CreateLabel("Solución: " + memoEditSolucion.Text, ref y, 40));

            detail.Controls.Add(CreateLabel("ID Visita: " + textEditIDVisita.Text, ref y));
            detail.Controls.Add(CreateLabel("ID Técnico: " + textEditIdTecnico.Text, ref y));
            detail.Controls.Add(CreateLabel("Nombre Técnico: " + textEditNombTecnico.Text, ref y));

            // Crear tabla desde el GridControl
            XRTable table = CreateTableFromGrid(gridControlRepuesto, ref y);
            detail.Controls.Add(table);

            // Vista previa
            ReportPrintTool printTool = new ReportPrintTool(report);
            printTool.ShowPreviewDialog();

            // Exportar a PDF en escritorio
            string rutaPDF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "FormularioExportado.pdf");
            report.ExportToPdf(rutaPDF);

            MessageBox.Show("PDF generado correctamente en el escritorio:\n" + rutaPDF);
        }

        // Etiquetas de texto
        private XRLabel CreateLabel(string text, ref int y, int height = 20)
        {
            var label = new XRLabel
            {
                Text = text,
                LocationF = new PointF(0, y),
                SizeF = new SizeF(700, height),
                Font = new Font("Arial", 10)
            };
            y += height;
            return label;
        }

        // Crear tabla desde GridControl
        private XRTable CreateTableFromGrid(GridControl grid, ref int y)
        {
            XRTable table = new XRTable
            {
                LocationF = new PointF(0, y += 30),
                SizeF = new SizeF(700, 25),
                Font = new Font("Arial", 9),
                Borders = DevExpress.XtraPrinting.BorderSide.All
            };

            table.BeginInit();

            GridView view = grid.MainView as GridView;
            if (view == null || view.RowCount == 0)
                return table;

            // Encabezado
            XRTableRow headerRow = new XRTableRow();
            foreach (GridColumn col in view.VisibleColumns)
            {
                XRTableCell cell = new XRTableCell
                {
                    Text = col.Caption,
                    Font = new Font("Arial", 9, FontStyle.Bold),
                    BackColor = Color.LightGray,
                    WidthF = 700f / view.VisibleColumns.Count
                };
                headerRow.Cells.Add(cell);
            }
            table.Rows.Add(headerRow);

            // Filas de datos
            for (int i = 0; i < view.RowCount; i++)
            {
                XRTableRow dataRow = new XRTableRow();
                foreach (GridColumn col in view.VisibleColumns)
                {
                    object value = view.GetRowCellValue(i, col);
                    XRTableCell cell = new XRTableCell
                    {
                        Text = value?.ToString(),
                        WidthF = 700f / view.VisibleColumns.Count
                    };
                    dataRow.Cells.Add(cell);
                }
                table.Rows.Add(dataRow);
            }

            table.EndInit();
            y += (view.RowCount + 1) * 25;

            return table;
        }
        
        private void simpleButtonRetomar_Click(object sender, EventArgs e)
        {
            
            if (textEditNoContrato.Text != "")
            {
                using (Retomar busca = new Retomar(textEditNoContrato.Text))
                {
                    if (busca.ShowDialog() == DialogResult.OK)
                    {
                        DatosGenerales = busca.RetomarSelect;
                        textEditIDVisita.Text = DatosGenerales.Visita;
                        var consulta = from visita in miLinq.Visita_tecnica
                                       join fll in miLinq.Falla on visita.id_visita_tecnica equals fll.id_visita_tecnica
                                       join tfll in miLinq.Tipo_falla on fll.id_tipo_falla equals tfll.id_tipo_falla
                                       select new
                                       {
                                           fecha=visita.fecha_solicitud,
                                           idtfll = tfll.id_tipo_falla
                                       };
                        dateEditFSolicitud.EditValue = Convert.ToDateTime(consulta.FirstOrDefault().fecha);
                        lookUpEditTipoFalla.EditValue = consulta.FirstOrDefault().idtfll;
                        RETOMA = true;
                    }
                }

            }
            else
            {
                MessageBox.Show("Debe ingresar un contrato", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
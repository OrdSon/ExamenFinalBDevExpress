using DevExpress.XtraEditors;
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
    public partial class BuscarRepuesto : DevExpress.XtraEditors.XtraForm
    {
        public General repuestoSelect {  get; set; }
        public BuscarRepuesto()
        {
            InitializeComponent();
        }
        BD.LinqDataContext miLinq = new BD.LinqDataContext(General.cadena);
        private void gridControlListaRepuestos_DoubleClick(object sender, EventArgs e)
        {
            repuestoSelect = new General
            {
                IdRepuesto = gridView1.GetFocusedRowCellValue("Codigo").ToString(),
                Repuesto = gridView1.GetFocusedRowCellValue("Repuesto").ToString(),
                Precio = Convert.ToDecimal(gridView1.GetFocusedRowCellValue("Precio").ToString())
            };
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BuscarRepuesto_Load(object sender, EventArgs e)
        {
            var consulta = from repu in miLinq.Repuesto
                           select new
                           {
                               Codigo=repu.id_respuesto,
                               Repuesto=repu.nombre_repuesto,
                               Precio=repu.precio_repuesto
                           };
            gridControlListaRepuestos.DataSource = consulta;
            gridView1.OptionsBehavior.Editable = false;
        }
    }
}
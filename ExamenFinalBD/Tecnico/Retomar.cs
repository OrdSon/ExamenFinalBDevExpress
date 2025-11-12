using DevExpress.Xpo.DB;
using DevExpress.XtraEditors;
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
    public partial class Retomar : DevExpress.XtraEditors.XtraForm
    {
        public General RetomarSelect { get; set; }
        BD.LinqDataContext miLinq=new BD.LinqDataContext(General.cadena);
        public string contra="";
        public Retomar(string contrato)
        {
            InitializeComponent();
            contra = contrato;
        }

        private void Retomar_Load(object sender, EventArgs e)
        {
            var consultaF= from consul in miLinq.Visita_tecnica
                           join pelo in miLinq.Peloton on consul.id_peloton equals pelo.id_peloton
                           join falla in miLinq.Falla on consul.id_visita_tecnica equals falla.id_visita_tecnica
                           where consul.id_contrato==contra&&consul.fecha_ejecucion== null
                           select new
                           {
                                Id_Visita=consul.id_visita_tecnica,
                                Fecha_solicitud=consul.fecha_solicitud,
                                Nombre_de_tecnico=pelo.Tecnico.nombre_tecnico,
                                Tipo_falla= falla.Tipo_falla.nombre_falla
                           };
            gridControlListaPendientes.DataSource = consultaF;
            gridView1.OptionsBehavior.Editable = false;
        }

        private void gridControlListaPendientes_DoubleClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Esta seguro de retomar esta operacion?", "Advertencia", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                RetomarSelect = new General
                {
                    Visita = gridView1.GetFocusedRowCellValue("Id_Visita").ToString(),

                };
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}
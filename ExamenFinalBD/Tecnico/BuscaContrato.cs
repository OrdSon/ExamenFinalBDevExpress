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
    public partial class BuscaContrato : DevExpress.XtraEditors.XtraForm
    {
        public General ContratoSelect { get; private set; }
        BD.LinqDataContext miLinq=new BD.LinqDataContext(General.cadena);
        public BuscaContrato()
        {
            InitializeComponent();
        }

        private void BuscaContrato_Load(object sender, EventArgs e)
        {
            var consulta = from contra in miLinq.Contrato
                           select new
                           {
                              ID_Contrato=contra.id_contrato,
                              Nombre= contra.Cliente.nombre,
                              Direccion = contra.direccion_instalacion,
                              Estado= contra.Estado_contrato.nombre_estado_contrato
                           };
            gridControlContratos.DataSource = consulta;
            gridView1.OptionsBehavior.Editable = false;
        }
        public void seleccion()
        {
            ContratoSelect = new General
            {
                NoContrato = gridView1.GetFocusedRowCellValue("ID_Contrato").ToString(),
                Nombre = gridView1.GetFocusedRowCellValue("Nombre").ToString(),
                Direccion = gridView1.GetFocusedRowCellValue("Direccion").ToString()
            };
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void gridControlContratos_DoubleClick(object sender, EventArgs e)
        {
            seleccion();
        }
    }
}
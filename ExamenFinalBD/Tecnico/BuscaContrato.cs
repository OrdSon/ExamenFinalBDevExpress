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
        public BuscaContrato()
        {
            InitializeComponent();
        }

        private void BuscaContrato_Load(object sender, EventArgs e)
        {

        }
        public void seleccion()
        {
            ContratoSelect = new General
            {
                NoContrato = gridView1.GetFocusedRowCellValue("No.Contrato").ToString(),
                Nombre = gridView1.GetFocusedRowCellValue("Nombre").ToString() + " " + gridView1.GetFocusedRowCellValue("Apellido").ToString(),
                Direccion = gridView1.GetFocusedRowCellValue("Direccion").ToString()
            };
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
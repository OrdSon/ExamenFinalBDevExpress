using DevExpress.XtraEditors;
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
            
        }
        public void redireccionaContrato()
        {
            using (BuscaContrato busca = new BuscaContrato())
            {
                if (busca.ShowDialog() == DialogResult.OK)
                {
                    
                }
            }
        }
    }
}
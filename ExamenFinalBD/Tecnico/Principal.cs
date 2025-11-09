using DevExpress.XtraBars;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.XtraBars.Ribbon;
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
    public partial class Principal : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Principal()
        {
            InitializeComponent();
        }

        private void barButtonItemCrear_ItemClick(object sender, ItemClickEventArgs e)
        {
            CreaReportes crea = new CreaReportes();
            crea.MdiParent=this;
            crea.Show();
        }
    }

}

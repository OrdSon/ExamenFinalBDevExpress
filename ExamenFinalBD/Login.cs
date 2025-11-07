using DevExpress.XtraEditors;
using ExamenFinalBD.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamenFinalBD
{
    public partial class Login : DevExpress.XtraEditors.DirectXForm
    {
        private PanelControl panelLogin;
        private UIUtils uiutils = new UIUtils();
        public Login()
        {
            inicializarPanelLogin();
            InitializeComponent();
            this.Text = "Login";
            this.WindowState = FormWindowState.Maximized;
        }

        private void inicializarPanelLogin()
        {
            panelLogin = new PanelControl
            {
                Size = new Size(600, 600),
                BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder,
                Appearance = {BackColor = Color.Wheat}
            };
            this.Controls.Add(panelLogin);
            this.uiutils.CenterControl(panelLogin, this);
        }

        private void CenterPanel()
        {
           
        }
        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void directXFormContainerControl1_SizeChanged(object sender, EventArgs e)
        {
            uiutils.CenterControl(panelLogin, this);
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelControl1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {

        }

        private void panelControl2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;


namespace ExamenFinalBD
{
    public partial class AdminHome : DevExpress.XtraEditors.XtraForm
    {
        public AdminHome()
        {
            InitializeComponent();
            Text = "Administrador - Panel principal";

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 4,
                Padding = new Padding(16),
                AutoSize = true
            };
            for (int r = 0; r < layout.RowCount; r++)
                layout.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            for (int c = 0; c < layout.ColumnCount; c++)
                layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            var btnConfig = MakeButton("Configuración", () => new Configuración().ShowDialog(this));
            var btnUsuario = MakeButton("Usuarios", () => new FrmUsuario().ShowDialog(this));
            var btnCliente = MakeButton("Clientes", () => new FrmCliente().ShowDialog(this));
            var btnContrato = MakeButton("Contratos", () => new FrmContrato().ShowDialog(this));
            

            layout.Controls.Add(btnConfig, 0, 0);
            layout.Controls.Add(btnUsuario, 1, 0);
            layout.Controls.Add(btnCliente, 0, 1);
            layout.Controls.Add(btnContrato, 1, 1);
            

            var lbl = new Label
            {
                Dock = DockStyle.Bottom,
                TextAlign = System.Drawing.ContentAlignment.MiddleRight,
                Padding = new Padding(0, 8, 8, 8),
                Text = $"Build: {Application.ProductVersion}"
            };

            Controls.Add(layout);
            Controls.Add(lbl);


        }


        private SimpleButton MakeButton(string text, Action onClick)
        {
            var b = new SimpleButton
            {
                Text = text,
                Dock = DockStyle.Fill,
                Height = 48
            };
            b.Click += (s, e) => onClick();
            return b;
        }
        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void AdminHome_Load(object sender, EventArgs e)
        {

        }

        private void ribbonControl1_Click(object sender, EventArgs e)
        {

        }
    }
}

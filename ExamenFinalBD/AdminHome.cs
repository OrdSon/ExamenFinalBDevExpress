using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamenFinalBD
{
    public class AdminHome : XtraForm
    {
        private RibbonControl ribbon;
        private BarButtonItem btnClientes;
        private BarButtonItem btnContratos;
        private BarButtonItem btnUsuarios;
        private BarButtonItem btnConfig;
        private BarButtonItem btnSalir;

        public AdminHome()
        {
            // Ventana principal
            IsMdiContainer = true;
            Text = "Panel de Administración";
            WindowState = FormWindowState.Maximized;

            InitializeRibbon();
        }

        private void InitializeRibbon()
        {
            ribbon = new RibbonControl
            {
                Dock = DockStyle.Top
            };

            // Botones
            btnClientes = new BarButtonItem() { Caption = "Clientes" };
            btnContratos = new BarButtonItem() { Caption = "Contratos" };
            btnUsuarios = new BarButtonItem() { Caption = "Usuarios" };
            btnConfig = new BarButtonItem() { Caption = "Configuración" };
            btnSalir = new BarButtonItem() { Caption = "Salir" };

            btnClientes.ItemClick += (s, e) => OpenForm<FrmCliente>();
            btnContratos.ItemClick += (s, e) => OpenForm<FrmContrato>();
            btnUsuarios.ItemClick += (s, e) => OpenForm<FrmUsuario>();
            btnConfig.ItemClick += (s, e) => OpenForm<Configuración>();
            btnSalir.ItemClick += (s, e) => Close();

            ribbon.Items.AddRange(new BarItem[] { btnClientes, btnContratos, btnUsuarios, btnConfig, btnSalir });

            // Página y grupos
            var pageInicio = new RibbonPage("Inicio");
            var grpOperaciones = new RibbonPageGroup("Operaciones");
            var grpSistema = new RibbonPageGroup("Sistema");

            grpOperaciones.ItemLinks.Add(btnClientes);
            grpOperaciones.ItemLinks.Add(btnContratos);
            grpOperaciones.ItemLinks.Add(btnUsuarios);
            grpSistema.ItemLinks.Add(btnConfig);
            grpSistema.ItemLinks.Add(btnSalir);

            pageInicio.Groups.Add(grpOperaciones);
            pageInicio.Groups.Add(grpSistema);

            ribbon.Pages.Add(pageInicio);
            Controls.Add(ribbon);
        }

        // Abre el formulario como MDI hijo; si ya está abierto, lo trae al frente.
        private void OpenForm<T>() where T : Form, new()
        {
            var existing = MdiChildren.FirstOrDefault(f => f is T);
            if (existing != null)
            {
                existing.Activate();
                return;
            }

            var frm = new T
            {
                MdiParent = this,
                StartPosition = FormStartPosition.CenterParent
            };
            frm.Show();
        }
    }
}

using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Helpers;
using ExamenFinalBD.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamenFinalBD
{
    public partial class NuevoClienteForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        UIUtils utils = new UIUtils();
        ClienteDAO clienteDAO = new ClienteDAO();
        Cajero cajero { get; set; }
        public NuevoClienteForm()
        {
            InitializeComponent();
        }

        public NuevoClienteForm(Cajero cajeroActual) : this()
        {
            cajero = cajeroActual;
        }

        private void labelControl5_Click(object sender, EventArgs e)
        {

        }

        private void saveBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (utils.validarNoVacio(dpiTxt, this.errorProvider1, "Campo de DPI")
                && utils.validarLongitudMaxima(dpiTxt, this.errorProvider1, 13, "Campo de DPI")
                && utils.validarNoVacio(nombreTxt, this.errorProvider1, "Campo de nombre")
                && utils.validarNoVacio(tel1Txt, this.errorProvider1, "Campo de telefono primario")
                && utils.validarLongitudMaxima(tel1Txt, this.errorProvider1, 8, "Campo de telfono primario")
                && utils.validarTxtEditEmail(emailTxt, this.errorProvider1))

            {
                if(utils.validarNoVacio(tel2Txt, this.errorProvider1, "Campo de telefono secundario"))
                {
                    utils.validarLongitudMaxima(tel2Txt, this.errorProvider1, 8, "Campo de telefono secundario");
                }
                if (clienteDAO.InsertarCliente(new BD.Cliente
                {
                    dpi = dpiTxt.Text.Trim(),
                    nombre = nombreTxt.Text.Trim(),
                    telefono_primario = tel1Txt.Text.Trim(),
                    telefono_secundario = tel2Txt.Text.Trim(),
                    email = emailTxt.Text.Trim(),
                }))
                {
                    MessageBox.Show("Cliente agregado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cajero.setNuevoClienteComoActivo(clienteDAO.ObtenerUltimoClienteAnadido());
                    this.Close();
                    this.Dispose();
                }
            }
            else
            {
                MessageBox.Show("Por favor, corrija los errores antes de guardar.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }



        private void NuevoClienteForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.cajero.resetNuevoClienteButton();
        }

        private void lblDireccion(object sender, EventArgs e)
        {

        }

        private void bbiReset_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            utils.clearTextEdits(nombreTxt, tel1Txt, tel2Txt, emailTxt, dpiTxt);
        }

        private void dpiTxt_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void dpiTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            utils.restringirSoloNumeros(sender, e as KeyPressEventArgs);

        }
    }
}

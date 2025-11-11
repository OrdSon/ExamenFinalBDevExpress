using DevExpress.XtraBars;
using ExamenFinalBD.BD;
using ExamenFinalBD.DAO;
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
    public partial class Cajero : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        Cliente cliente;
        ContratoDAO contratoDAO = new ContratoDAO();
        ClienteDAO clienteDAO = new ClienteDAO();
        PagoDAO pagoDAO = new PagoDAO();
        public Cajero()
        {
            InitializeComponent();
        }

        private void setLabels()
        {

        }

        private void pictureEdit4_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void pictureEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void pictureEdit1_EditValueChanged_1(object sender, EventArgs e)
        {

        }

        private void barStaticItem1_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (idTxt.EditValue != null)
            {
                string textoId = idTxt.EditValue.ToString();
                Cliente temp = clienteDAO.ObtenerClientePorID(textoId);
                if (temp != null)
                {
                    setNuevoClienteComoActivo(temp);
                }
                else
                {
                    
                    MessageBox.Show("Error al establecer el cliente activo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void barCheckItem2_CheckedChanged(object sender, ItemClickEventArgs e)
        {

        }

        private void barToggleSwitchItem2_CheckedChanged(object sender, ItemClickEventArgs e)
        {

            nuevoClienteAutoToggle();

        }
        public void setNuevoClienteComoActivo(Cliente cliente)
        {
            this.cliente = cliente;
            clienteIdLabel.Text = cliente.id_cliente;
            if (cliente != null && cliente.id_cliente != null)
            {
                LlenarContratosCombo(cliente.id_cliente);
            }
            else
            {
                clienteIdLabel.Text = "Error";
                MessageBox.Show("Error al establecer el cliente activo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void resetNuevoClienteButton()
        {
            toggleNewCustomer.Checked = false;
            nuevoClienteAutoToggle();
        }

        private void CargarPagosContrato(string idContrato)
        {
            try
            {
                var lista = pagoDAO.ObtenerPagosPorContrato(idContrato);

                pagosGrid.DataSource = lista;
                gridView1.OptionsView.ColumnAutoWidth = false;
                gridView1.BestFitColumns();

                // Si tu GridControl tiene una GridView asociada:
                // gridViewPagos.OptionsView.ColumnAutoWidth = false;
                // gridViewPagos.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los pagos: " + ex.Message,
                    "Pagos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void nuevoClienteAutoToggle()
        {
            NuevoClienteForm nuevoClienteForm = new NuevoClienteForm(this);

            if (toggleNewCustomer.Checked)
            {
                accNumberText.Enabled = false;
                searchButton.Enabled = false;
                nuevoClienteForm.ShowDialog();

            }
            else
            {
                accNumberText.Enabled = true;
                searchButton.Enabled = true;
                nuevoClienteForm.Close();
                nuevoClienteForm.Dispose();
                nuevoClienteForm = null;
            }
        }

        private void xtraTabPage1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void imageEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void pictureEdit1_EditValueChanged_2(object sender, EventArgs e)
        {

        }
        private void setClienteActivo(Cliente cliente)
        {
            this.cliente = cliente;

        }

        private void cableBasicoToggle_Toggled(object sender, EventArgs e)
        {
            if (cableBasicoToggle.IsOn)
            {
                cablePlusToggle.IsOn = false;
                cablePlusToggle.Enabled = false;
                cablePremiumToggle.IsOn = false;
                cablePremiumToggle.Enabled = false;
            }
            else
            {
                cablePlusToggle.Enabled = true;
                cablePremiumToggle.Enabled = true;
            }
        }

        private void internetBasicoToggle_Toggled(object sender, EventArgs e)
        {
            if (internetBasicoToggle.IsOn)
            {
                internetPlusToggle.IsOn = false;
                internetPlusToggle.Enabled = false;
                internetPremiumToggle.IsOn = false;
                internetPremiumToggle.Enabled = false;
            }
            else
            {
                internetPlusToggle.Enabled = true;
                internetPremiumToggle.Enabled = true;
            }
        }

        private void telefonoBasicoToggle_Toggled(object sender, EventArgs e)
        {
            if (telefonoBasicoToggle.IsOn)
            {
                telefonoPlusToggle.IsOn = false;
                telefonoPlusToggle.Enabled = false;
                telefonoPremiumToggle.IsOn = false;
                telefonoPremiumToggle.Enabled = false;
            }
            else
            {
                telefonoPlusToggle.Enabled = true;
                telefonoPremiumToggle.Enabled = true;
            }
        }

        private void cablePlusToggle_Toggled(object sender, EventArgs e)
        {
            if (cablePlusToggle.IsOn)
            {
                cableBasicoToggle.IsOn = false;
                cableBasicoToggle.Enabled = false;
                cablePremiumToggle.IsOn = false;
                cablePremiumToggle.Enabled = false;
            }
            else
            {
                cableBasicoToggle.Enabled = true;
                cablePremiumToggle.Enabled = true;
            }
        }

        private void internetPlusToggle_Toggled(object sender, EventArgs e)
        {
            if (internetPlusToggle.IsOn)
            {
                internetBasicoToggle.IsOn = false;
                internetBasicoToggle.Enabled = false;
                internetPremiumToggle.IsOn = false;
                internetPremiumToggle.Enabled = false;
            }
            else
            {
                internetBasicoToggle.Enabled = true;
                internetPremiumToggle.Enabled = true;
            }
        }

        private void telefonoPlusToggle_Toggled(object sender, EventArgs e)
        {
            if (telefonoPlusToggle.IsOn)
            {
                telefonoBasicoToggle.IsOn = false;
                telefonoBasicoToggle.Enabled = false;
                telefonoPremiumToggle.IsOn = false;
                telefonoPremiumToggle.Enabled = false;
            }
            else
            {
                telefonoBasicoToggle.Enabled = true;
                telefonoPremiumToggle.Enabled = true;
            }
        }

        private void cablePremiumToggle_Toggled(object sender, EventArgs e)
        {
            if (cablePremiumToggle.IsOn)
            {
                cableBasicoToggle.IsOn = false;
                cableBasicoToggle.Enabled = false;
                cablePlusToggle.IsOn = false;
                cablePlusToggle.Enabled = false;
            }
            else
            {
                cableBasicoToggle.Enabled = true;
                cablePlusToggle.Enabled = true;
            }
        }

        private void internetPremiumToggle_Toggled(object sender, EventArgs e)
        {
            if (internetPremiumToggle.IsOn)
            {
                internetBasicoToggle.IsOn = false;
                internetBasicoToggle.Enabled = false;
                internetPlusToggle.IsOn = false;
                internetPlusToggle.Enabled = false;
            }
            else
            {
                internetBasicoToggle.Enabled = true;
                internetPlusToggle.Enabled = true;
            }
        }

        private void telefonoPremiumToggle_Toggled(object sender, EventArgs e)
        {
            if (telefonoPremiumToggle.IsOn)
            {
                telefonoBasicoToggle.IsOn = false;
                telefonoBasicoToggle.Enabled = false;
                telefonoPlusToggle.IsOn = false;
                telefonoPlusToggle.Enabled = false;
            }
            else
            {
                telefonoBasicoToggle.Enabled = true;
                telefonoPlusToggle.Enabled = true;
            }
        }

        private void LlenarContratosCombo(string idCliente)
        {
            List<string> listaContratos = contratoDAO.ObtenerIdsContratosPorCliente(idCliente);

            DevExpress.XtraEditors.Repository.RepositoryItemComboBox comboRepository =
                barEditItem1.Edit as DevExpress.XtraEditors.Repository.RepositoryItemComboBox;

            if (comboRepository != null)
            {
                comboRepository.Items.Clear();

                foreach (string idContrato in listaContratos)
                {
                    comboRepository.Items.Add(idContrato);
                }
            }
            else if (barEditItem1.Edit is DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookUpRepository)
            {
                lookUpRepository.DataSource = listaContratos.Select(c => new { Id = c }).ToList();
                lookUpRepository.ValueMember = "Id";
                lookUpRepository.DisplayMember = "Id";
            }
            else
            {
                Console.WriteLine("Tipo de RepositoryItem no compatible para llenado directo.");
            }
        }


        private void clienteIdLabel_Click(object sender, EventArgs e)
        {

        }

        private void barStaticItem4_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barEditItem1_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barEditItem1_EditValueChanged(object sender, EventArgs e)
        {
            if(barEditItem1.EditValue!=null) {
                CargarPagosContrato(barEditItem1.EditValue.ToString());
            }
        }
    }
}
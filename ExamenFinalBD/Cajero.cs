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
using TuProyecto.DAO;

namespace ExamenFinalBD
{
    public partial class Cajero : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        Cliente cliente;
        ContratoDAO contratoDAO = new ContratoDAO();
        ClienteDAO clienteDAO = new ClienteDAO();
        ResumenPagosDAO resumenDAO = new ResumenPagosDAO();
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

                var view = gridView1;

                // Ocultar columnas técnicas
                view.Columns[nameof(PagoGridDTO.IdContrato)].Visible = false;
                view.Columns[nameof(PagoGridDTO.IdServicio)].Visible = false;
                view.Columns[nameof(PagoGridDTO.IdCuota)].Visible = false;
                view.Columns[nameof(PagoGridDTO.MontoFactura)].Visible = false;
                view.Columns[nameof(PagoGridDTO.MontoPago)].Visible = false;

                // Captions
                view.Columns[nameof(PagoGridDTO.TipoServicio)].Caption = "Tipo de servicio";
                view.Columns[nameof(PagoGridDTO.NumeroCuota)].Caption = "Cuota #";
                view.Columns[nameof(PagoGridDTO.IdFactura)].Caption = "Factura";
                view.Columns[nameof(PagoGridDTO.FechaVencimientoCuota)].Caption = "Vence";
                view.Columns[nameof(PagoGridDTO.IdPago)].Caption = "Pago";
                view.Columns[nameof(PagoGridDTO.FechaPago)].Caption = "Fecha pago";
                view.Columns[nameof(PagoGridDTO.EstadoPago)].Caption = "Estado pago";
                view.Columns[nameof(PagoGridDTO.PagoAtrasado)].Caption = "¿Atrasado?";
                view.Columns[nameof(PagoGridDTO.MontoCuota)].Caption = "Monto cuota";
                view.Columns[nameof(PagoGridDTO.MoraAplicada)].Caption = "Mora";
                view.Columns[nameof(PagoGridDTO.TotalConMora)].Caption = "Total con mora";
                view.Columns[nameof(PagoGridDTO.BalanceForward)].Caption = "Balance forward";

                // Orden de columnas:
                view.Columns[nameof(PagoGridDTO.TipoServicio)].VisibleIndex = 0;
                view.Columns[nameof(PagoGridDTO.NumeroCuota)].VisibleIndex = 1;
                view.Columns[nameof(PagoGridDTO.IdFactura)].VisibleIndex = 2;
                view.Columns[nameof(PagoGridDTO.FechaVencimientoCuota)].VisibleIndex = 3;
                view.Columns[nameof(PagoGridDTO.IdPago)].VisibleIndex = 4;
                view.Columns[nameof(PagoGridDTO.FechaPago)].VisibleIndex = 5;
                view.Columns[nameof(PagoGridDTO.EstadoPago)].VisibleIndex = 6;
                view.Columns[nameof(PagoGridDTO.PagoAtrasado)].VisibleIndex = 7;
                view.Columns[nameof(PagoGridDTO.MontoCuota)].VisibleIndex = 8;
                view.Columns[nameof(PagoGridDTO.MoraAplicada)].VisibleIndex = 9;
                view.Columns[nameof(PagoGridDTO.TotalConMora)].VisibleIndex = 10;
                view.Columns[nameof(PagoGridDTO.BalanceForward)].VisibleIndex = 11;

                // Formato de fechas
                view.Columns[nameof(PagoGridDTO.FechaVencimientoCuota)].DisplayFormat.FormatType =
                    DevExpress.Utils.FormatType.DateTime;
                view.Columns[nameof(PagoGridDTO.FechaVencimientoCuota)].DisplayFormat.FormatString = "dd/MM/yyyy";

                view.Columns[nameof(PagoGridDTO.FechaPago)].DisplayFormat.FormatType =
                    DevExpress.Utils.FormatType.DateTime;
                view.Columns[nameof(PagoGridDTO.FechaPago)].DisplayFormat.FormatString = "dd/MM/yyyy";

                view.BestFitColumns();
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

        private void ActualizarBarraContrato(string idContrato)
        {
            var resumen = resumenDAO.ObtenerResumenActual(idContrato);

            if (resumen == null)
            {
                bsiDireccion.Caption = "-";
                bsiInicioContrato.Caption = "-";
                bsiFinContrato.Caption = "-";
                bsiTotalCable.Caption = "-";
                bsiTotalInternet.Caption = "-";
                bsiTotalTelefono.Caption = "-";
                bsiTotalFactura.Caption = "-";
                return;
            }

            bsiDireccion.Caption = resumen.DireccionInstalacion;
            bsiInicioContrato.Caption = resumen.FechaInicioContratoTexto;
            bsiFinContrato.Caption = resumen.FechaFinContrato.ToString("dd/MM/yyyy");

            bsiTotalCable.Caption = resumen.TotalCable.ToString("Q0.00");
            bsiTotalInternet.Caption = resumen.TotalInternet.ToString("Q0.00");
            bsiTotalTelefono.Caption = resumen.TotalTelefono.ToString("Q0.00");

            bsiTotalFactura.Caption = resumen.TotalPendiente.ToString("Q0.00");
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
                ActualizarBarraContrato(barEditItem1.EditValue.ToString());
            }
        }

        private void barStaticItem12_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}
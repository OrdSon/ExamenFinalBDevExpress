using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
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



namespace ExamenFinalBD
{
    public partial class FrmCliente : Form
    {
        private readonly db_ac0671_finalDataSet1 ds = new db_ac0671_finalDataSet1();

        private readonly db_ac0671_finalDataSet1TableAdapters.ClienteTableAdapter taCliente
            = new db_ac0671_finalDataSet1TableAdapters.ClienteTableAdapter();
        private readonly db_ac0671_finalDataSet1TableAdapters.ContratoTableAdapter taContrato
            = new db_ac0671_finalDataSet1TableAdapters.ContratoTableAdapter();
        private readonly db_ac0671_finalDataSet1TableAdapters.Info_cobroTableAdapter taInfoCobro
            = new db_ac0671_finalDataSet1TableAdapters.Info_cobroTableAdapter();

        private readonly db_ac0671_finalDataSet1TableAdapters.TableAdapterManager manager
            = new db_ac0671_finalDataSet1TableAdapters.TableAdapterManager();

        private readonly BindingSource bsCliente = new BindingSource();
        private readonly BindingSource bsInfoCobro = new BindingSource(); // panel derecho / detalle opcional
        private readonly BindingSource bsContrato = new BindingSource(); // para búsquedas rápidas


        public FrmCliente()
        {
            InitializeComponent();
            InitContext();
        }


        private void InitContext()
        {
            manager.BackupDataSetBeforeUpdate = false;
            manager.ClienteTableAdapter = taCliente;
            manager.ContratoTableAdapter = taContrato;
            manager.Info_cobroTableAdapter = taInfoCobro;
            manager.UpdateOrder = db_ac0671_finalDataSet1TableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;

            CargarDatos();

            ConfigurarBindings();

            ConfigurarGrid();
        }


        private void CargarDatos()
        {
            taCliente.Fill(ds.Cliente);
            taContrato.Fill(ds.Contrato);
            taInfoCobro.Fill(ds.Info_cobro);
        }

        private void ConfigurarBindings()
        {
            bsCliente.DataSource = ds;
            bsCliente.DataMember = "Cliente";

            bsContrato.DataSource = ds;
            bsContrato.DataMember = "Contrato";

            bsInfoCobro.DataSource = ds;
            bsInfoCobro.DataMember = "Info_cobro"; 

            gridControlCliente.DataSource = bsCliente;
            gridControlCliente.ForceInitialize();
            gridViewCliente.PopulateColumns();

            dataLayoutControl1.DataSource = bsInfoCobro;
            dataLayoutControl1.RetrieveFields();
        }

        private void ConfigurarGrid()
        {
            var view = gridViewCliente as GridView;

            view.OptionsBehavior.Editable = true;
            view.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            view.OptionsView.ShowAutoFilterRow = true;

            // Captions
            TryCaption("id_cliente", "ID");
            TryCaption("dpi", "DPI");
            TryCaption("nombre", "Nombre");
            TryCaption("telefono_primario", "Tel. 1");
            TryCaption("telefono_secundario", "Tel. 2");
            TryCaption("email", "Email");

            view.BestFitColumns();

            view.FocusedRowChanged += (s, e) =>
            {
                var rowView = bsCliente.Current as DataRowView;
                var row = rowView?.Row as db_ac0671_finalDataSet1.ClienteRow;
                if (row == null) { bsInfoCobro.Filter = "1=0"; return; }

                var contratos = ds.Contrato
                    .Where(c => c.RowState != DataRowState.Deleted && c.id_cliente == row.id_cliente)
                    .ToList();

                if (contratos.Count == 0)
                {
                    bsInfoCobro.Filter = "1=0"; // vacío
                    return;
                }


                contratos = ds.Contrato
    .Where(c => c.RowState != DataRowState.Deleted && c.id_cliente == row.id_cliente)
    .ToList();

                if (contratos.Count == 0)
                {
                    bsInfoCobro.Filter = "1=0";
                    return;
                }

                var idInfo = contratos.First().id_info_cobro;
                if (string.IsNullOrEmpty(idInfo))
                {
                    bsInfoCobro.Filter = "1=0";
                }
                else
                {
                    bsInfoCobro.Filter = $"id_info_cobro = '{idInfo.Replace("'", "''")}'";
                }



                if (string.IsNullOrEmpty(idInfo))
                {
                    bsInfoCobro.Filter = "1=0";
                }
                else
                {
                    bsInfoCobro.Filter = $"id_info_cobro = '{idInfo.Replace("'", "''")}'";
                }
            };

            view.ValidateRow += (s, e) =>
            {
                var v = (GridView)s;
                string id = (string)v.GetRowCellValue(e.RowHandle, "id_cliente");
                string nombre = (string)v.GetRowCellValue(e.RowHandle, "nombre");

                if (string.IsNullOrWhiteSpace(id))
                {
                    e.Valid = false; e.ErrorText = "ID requerido."; return;
                }
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    e.Valid = false; e.ErrorText = "Nombre requerido."; return;
                }

                if (v.IsNewItemRow(e.RowHandle))
                {
                    bool existe = ds.Cliente.AsEnumerable()
                        .Any(r => r.RowState != DataRowState.Deleted &&
                                  r.Field<string>("id_cliente") == id);
                    if (existe)
                    {
                        e.Valid = false; e.ErrorText = "El ID ya existe."; return;
                    }
                }
            };

            view.InvalidRowException += (s, e) =>
                e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }


        private void TryCaption(string field, string caption)
        {
            var col = gridViewCliente.Columns[field];
            if (col != null) col.Caption = caption;
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                gridViewCliente.CloseEditor();
                bsCliente.EndEdit();
                Validate();

                manager.UpdateAll(ds); 
                XtraMessageBox.Show("Cambios guardados.");

                taCliente.Fill(ds.Cliente);
                taContrato.Fill(ds.Contrato);
                taInfoCobro.Fill(ds.Info_cobro);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al guardar: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var view = gridViewCliente;

            view.CloseEditor();
            if (view.IsNewItemRow(view.FocusedRowHandle))
            {
                view.CancelUpdateCurrentRow();
                return;
            }

            var rowView = bsCliente.Current as DataRowView;
            var row = rowView?.Row as db_ac0671_finalDataSet1.ClienteRow;
            if (row == null) return;

            if (XtraMessageBox.Show("¿Eliminar cliente seleccionado?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                bool tieneContratos = ds.Contrato
                    .Any(c => c.RowState != DataRowState.Deleted && c.id_cliente == row.id_cliente);

                if (tieneContratos)
                {
                    XtraMessageBox.Show("No se puede eliminar: el cliente tiene contratos asociados.");
                    return;
                }


                row.Delete();
                manager.UpdateAll(ds);

                taCliente.Fill(ds.Cliente);
                taContrato.Fill(ds.Contrato);
                taInfoCobro.Fill(ds.Info_cobro);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al eliminar: " + ex.Message);
            }
        }

        private void FrmCliente_Load(object sender, EventArgs e)
        {
            

        }

        



    }
}
    


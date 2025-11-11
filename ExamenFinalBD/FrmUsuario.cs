using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class FrmUsuario : Form
    {
        private readonly db_ac0671_finalDataSet1 ds = new db_ac0671_finalDataSet1();

        private readonly db_ac0671_finalDataSet1TableAdapters.ClienteTableAdapter taCliente
            = new db_ac0671_finalDataSet1TableAdapters.ClienteTableAdapter();

        private readonly db_ac0671_finalDataSet1TableAdapters.TableAdapterManager manager
            = new db_ac0671_finalDataSet1TableAdapters.TableAdapterManager();

        private readonly BindingSource bsCliente = new BindingSource();
        public FrmUsuario()
        {
            InitializeComponent();
            InitContext();
        }

        private void InitContext()
        {
            manager.BackupDataSetBeforeUpdate = false;
            manager.ClienteTableAdapter = taCliente;
            manager.UpdateOrder = db_ac0671_finalDataSet1TableAdapters
                                  .TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;

            CargarDatos();

            ConfigurarBindings();
            ConfigurarGrid();

            
        }

        private void CargarDatos()
        {
            taCliente.Fill(ds.Cliente);
        }

        private void ConfigurarBindings()
        {
            bsCliente.DataSource = ds;
            bsCliente.DataMember = "Cliente";

            gridControlUsuario.DataSource = bsCliente;
            gridControlUsuario.ForceInitialize();
            gridViewUsuario.PopulateColumns();
        }



        private void ConfigurarGrid()
        {
            var view = gridViewUsuario as GridView;

            view.OptionsBehavior.Editable = true;
            view.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            view.OptionsView.ShowAutoFilterRow = true;

            TryCaption("id_cliente", "ID");
            TryCaption("dpi", "DPI");
            TryCaption("nombre", "Nombre");
            TryCaption("telefono_primario", "Tel. 1");
            TryCaption("telefono_secundario", "Tel. 2");
            TryCaption("email", "Email");

            view.BestFitColumns();

            view.ValidateRow += (s, e) =>
            {
                var v = (GridView)s;
                string id = (string)v.GetRowCellValue(e.RowHandle, "id_cliente");
                string nombre = (string)v.GetRowCellValue(e.RowHandle, "nombre");

                if (string.IsNullOrWhiteSpace(id))
                {
                    e.Valid = false;
                    e.ErrorText = "ID requerido.";
                    return;
                }
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    e.Valid = false;
                    e.ErrorText = "Nombre requerido.";
                    return;
                }

                if (v.IsNewItemRow(e.RowHandle))
                {
                    bool existe = ds.Cliente.AsEnumerable()
                        .Any(r => !r.RowState.Equals(DataRowState.Deleted) &&
                                  r.Field<string>("id_cliente") == id);
                    if (existe)
                    {
                        e.Valid = false;
                        e.ErrorText = "El ID ya existe.";
                    }
                }
            };

            view.InvalidRowException += (s, e) =>
                e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void TryCaption(string field, string caption)
        {
            var col = gridViewUsuario.Columns[field];
            if (col != null) col.Caption = caption;
        }
        private void FrmUsuario_Load(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                gridViewUsuario.CloseEditor();
                bsCliente.EndEdit();
                Validate();

                manager.UpdateAll(ds);      // guarda todo lo pendiente (insert/update/delete)
                XtraMessageBox.Show("Cambios guardados.");

                taCliente.Fill(ds.Cliente);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al guardar: " + ex.Message);
            }
        }



        

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            var view = gridViewUsuario;

            view.CloseEditor();
            if (view.IsNewItemRow(view.FocusedRowHandle))
            {
                view.CancelUpdateCurrentRow();
                return;
            }

            var row = (bsCliente.Current as DataRowView)?.Row as db_ac0671_finalDataSet1.ClienteRow;
            if (row == null) return;

            if (XtraMessageBox.Show("¿Eliminar cliente seleccionado?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                row.Delete();
                manager.UpdateAll(ds);
                taCliente.Fill(ds.Cliente);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al eliminar: " + ex.Message);
            }
        }

           
    }
}

using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
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
    public partial class Configuración : Form
    {

        private readonly db_ac0671_finalDataSet1 ds = new db_ac0671_finalDataSet1();

        private readonly db_ac0671_finalDataSet1TableAdapters.ConfiguracionTableAdapter taConfig
            = new db_ac0671_finalDataSet1TableAdapters.ConfiguracionTableAdapter();

        private readonly db_ac0671_finalDataSet1TableAdapters.TableAdapterManager manager
            = new db_ac0671_finalDataSet1TableAdapters.TableAdapterManager();

        private readonly BindingSource bs = new BindingSource();


        public Configuración()
        {
            InitializeComponent();
            InitContext();



        }

        private void InitContext()
        {
            var cs = ConfigurationManager.ConnectionStrings["ConexionBD"]?.ConnectionString;
            if (!string.IsNullOrEmpty(cs))
            {
                taConfig.Connection.ConnectionString = cs;
            }

            manager.BackupDataSetBeforeUpdate = false;
            manager.ConfiguracionTableAdapter = taConfig;
            manager.UpdateOrder = db_ac0671_finalDataSet1TableAdapters
                                  .TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;

            CargarDatos();
            ConfigurarBindings();
            ConfigurarGrid();
        }


        private void CargarDatos()
        {
            taConfig.Fill(ds.Configuracion);
        }

        private void ConfigurarBindings()
        {
            bs.DataSource = ds;
            bs.DataMember = "Configuracion";

            gridControlConfiguracion.DataSource = bs;
            gridControlConfiguracion.ForceInitialize();
            gridViewConfiguracion.PopulateColumns();
        }


        private void ConfigurarGrid()
        {
            var view = gridViewConfiguracion as GridView;

            view.OptionsBehavior.Editable = true;
            view.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            view.OptionsView.ShowAutoFilterRow = true;

            TryCaption("id_configuracion", "ID");
            TryCaption("monto_mora", "Mora (Q)");
            TryCaption("monto_inscripcion", "Inscripción (Q)");

            SetNumericMask("monto_mora", "n2");
            SetNumericMask("monto_inscripcion", "n2");

            view.BestFitColumns();

            view.ValidateRow += (s, e) =>
            {
                var v = (GridView)s;
                var id = Convert.ToString(v.GetRowCellValue(e.RowHandle, "id_configuracion"));
                var moraObj = v.GetRowCellValue(e.RowHandle, "monto_mora");
                var insObj = v.GetRowCellValue(e.RowHandle, "monto_inscripcion");

                if (string.IsNullOrWhiteSpace(id))
                {
                    e.Valid = false; e.ErrorText = "ID requerido."; return;
                }
                if (!TryDecimal(moraObj, out var mora) || mora < 0)
                {
                    e.Valid = false; e.ErrorText = "Mora inválida."; return;
                }
                if (!TryDecimal(insObj, out var ins) || ins < 0)
                {
                    e.Valid = false; e.ErrorText = "Inscripción inválida."; return;
                }

                if (v.IsNewItemRow(e.RowHandle))
                {
                    bool existe = ds.Configuracion.AsEnumerable()
                        .Any(r => r.RowState != DataRowState.Deleted &&
                                  r.Field<string>("id_configuracion") == id);
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
            var col = gridViewConfiguracion.Columns[field];
            if (col != null) col.Caption = caption;
        }

        private void SetNumericMask(string field, string editMask)
        {
            var col = gridViewConfiguracion.Columns[field];
            if (col == null) return;

            var repo = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
            {
                Mask =
                {
                    MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric,
                    EditMask = editMask,
                    UseMaskAsDisplayFormat = true
                }
            };
            gridControlConfiguracion.RepositoryItems.Add(repo);
            col.ColumnEdit = repo;
        }


        private bool TryDecimal(object value, out decimal result)
        {
            if (value == null) { result = 0; return false; }
            return decimal.TryParse(Convert.ToString(value), out result);
        }

        private void Configuración_Load(object sender, EventArgs e)
        {
            


        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                gridViewConfiguracion.CloseEditor();
                bs.EndEdit();
                Validate();

                manager.UpdateAll(ds);
                XtraMessageBox.Show("Cambios guardados.");
                taConfig.Fill(ds.Configuracion);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al guardar: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var view = gridViewConfiguracion;

            view.CloseEditor();
            if (view.IsNewItemRow(view.FocusedRowHandle))
            {
                view.CancelUpdateCurrentRow();
                return;
            }

            var row = (bs.Current as DataRowView)?.Row as db_ac0671_finalDataSet1.ConfiguracionRow;
            if (row == null) return;

            if (XtraMessageBox.Show("¿Eliminar el registro seleccionado?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                row.Delete();
                manager.UpdateAll(ds);
                taConfig.Fill(ds.Configuracion);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al eliminar: " + ex.Message);
            }

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            
        }

        private void configuracionBindingSource_CurrentChanged(object sender, EventArgs e)
        {
                    }
    }
    }


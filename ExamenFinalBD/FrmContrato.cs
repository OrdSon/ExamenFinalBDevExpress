using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
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
    public partial class FrmContrato : Form
    {
        private db_ac0671_finalDataSet1 ds = new db_ac0671_finalDataSet1();

        private db_ac0671_finalDataSet1TableAdapters.ContratoTableAdapter taContrato
            = new db_ac0671_finalDataSet1TableAdapters.ContratoTableAdapter();
        private db_ac0671_finalDataSet1TableAdapters.ClienteTableAdapter taCliente
            = new db_ac0671_finalDataSet1TableAdapters.ClienteTableAdapter();
        private db_ac0671_finalDataSet1TableAdapters.Info_cobroTableAdapter taInfo
            = new db_ac0671_finalDataSet1TableAdapters.Info_cobroTableAdapter();
        private db_ac0671_finalDataSet1TableAdapters.MunicipioTableAdapter taMuni
            = new db_ac0671_finalDataSet1TableAdapters.MunicipioTableAdapter();
        private db_ac0671_finalDataSet1TableAdapters.Estado_contratoTableAdapter taEstado
            = new db_ac0671_finalDataSet1TableAdapters.Estado_contratoTableAdapter();
        private db_ac0671_finalDataSet1TableAdapters.UsuarioTableAdapter taUsuario
            = new db_ac0671_finalDataSet1TableAdapters.UsuarioTableAdapter();

        private db_ac0671_finalDataSet1TableAdapters.TableAdapterManager manager
            = new db_ac0671_finalDataSet1TableAdapters.TableAdapterManager();


        private readonly BindingSource bsContrato = new BindingSource();
        private readonly BindingSource bsCliente = new BindingSource();
        private readonly BindingSource bsInfoCobro = new BindingSource();
        private readonly BindingSource bsMunicipio = new BindingSource();
        private readonly BindingSource bsEstadoContrato = new BindingSource();
        private readonly BindingSource bsUsuario = new BindingSource();

        public FrmContrato()
        {
            InitializeComponent();

            InitContext();
            ConfigurarGrid();
            ConfigurarDataLayout();
        }

        private void InitContext()
        {
            manager.BackupDataSetBeforeUpdate = false;
            manager.ContratoTableAdapter = taContrato;
            manager.ClienteTableAdapter = taCliente;
            manager.Info_cobroTableAdapter = taInfo;
            manager.MunicipioTableAdapter = taMuni;
            manager.Estado_contratoTableAdapter = taEstado;
            manager.UsuarioTableAdapter = taUsuario;
            manager.UpdateOrder = db_ac0671_finalDataSet1TableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;

            CargarDatos();
            ConfigurarBindings();
            ConfigurarGrid();
            ConfigurarDataLayout();
        }

        private void CargarDatos()
        {
            taCliente.Fill(ds.Cliente);
            taInfo.Fill(ds.Info_cobro);
            taMuni.Fill(ds.Municipio);
            taEstado.Fill(ds.Estado_contrato);
            taUsuario.Fill(ds.Usuario);

            taContrato.Fill(ds.Contrato);
        }


        private void ConfigurarGrid()
        {
            gridViewContrato.OptionsBehavior.Editable = true;
            gridViewContrato.OptionsView.NewItemRowPosition =
                DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            gridViewContrato.OptionsView.ShowAutoFilterRow = true;

            

            var repoCliente = new RepositoryItemLookUpEdit
            {
                DataSource = bsCliente,
                DisplayMember = "nombre",
                ValueMember = "id_cliente",
                NullText = ""
            };
            repoCliente.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("id_cliente", "ID", 60));
            repoCliente.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("nombre", "Cliente", 180));
            gridControlContrato.RepositoryItems.Add(repoCliente);
            gridViewContrato.Columns["id_cliente"].ColumnEdit = repoCliente;

            var repoInfo = new RepositoryItemLookUpEdit
            {
                DataSource = bsInfoCobro,
                DisplayMember = "direccion",
                ValueMember = "id_info_cobro",
                NullText = ""
            };
            repoInfo.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("id_info_cobro", "ID", 60));
            repoInfo.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("direccion", "Dirección", 220));
            gridControlContrato.RepositoryItems.Add(repoInfo);
            gridViewContrato.Columns["id_info_cobro"].ColumnEdit = repoInfo;

            var repoMuni = new RepositoryItemLookUpEdit
            {
                DataSource = bsMunicipio,
                DisplayMember = "nombre_municipio",
                ValueMember = "id_municipio",
                NullText = ""
            };
            gridControlContrato.RepositoryItems.Add(repoMuni);
            gridViewContrato.Columns["id_municipio"].ColumnEdit = repoMuni;

            var repoEstado = new RepositoryItemLookUpEdit
            {
                DataSource = bsEstadoContrato,
                DisplayMember = "nombre_estado_contrato",
                ValueMember = "id_estado_contrato",
                NullText = ""
            };
            gridControlContrato.RepositoryItems.Add(repoEstado);
            gridViewContrato.Columns["id_estado_contrato"].ColumnEdit = repoEstado;

            var repoUsuario = new RepositoryItemLookUpEdit
            {
                DataSource = bsUsuario,
                DisplayMember = "nombre_usuario",
                ValueMember = "id_usuario",
                NullText = ""
            };
            gridControlContrato.RepositoryItems.Add(repoUsuario);
            gridViewContrato.Columns["id_usuario"].ColumnEdit = repoUsuario;

            gridViewContrato.BestFitColumns();

            gridViewContrato.ValidateRow += (s, e) =>
            {
                var v = (DevExpress.XtraGrid.Views.Grid.GridView)s;
                string id = (string)v.GetRowCellValue(e.RowHandle, "id_contrato");
                var cli = v.GetRowCellValue(e.RowHandle, "id_cliente");
                var est = v.GetRowCellValue(e.RowHandle, "id_estado_contrato");

                if (string.IsNullOrWhiteSpace(id)) { e.Valid = false; e.ErrorText = "ID contrato requerido."; return; }
                if (cli == null || string.IsNullOrWhiteSpace(cli.ToString())) { e.Valid = false; e.ErrorText = "Cliente requerido."; return; }
                if (est == null || string.IsNullOrWhiteSpace(est.ToString())) { e.Valid = false; e.ErrorText = "Estado requerido."; return; }
            };
            gridViewContrato.InvalidRowException += (s, e) =>
                e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }
        private void ConfigurarBindings()
        {
            bsContrato.DataSource = ds;
            bsContrato.DataMember = "Contrato";

            bsCliente.DataSource = ds; bsCliente.DataMember = "Cliente";
            bsInfoCobro.DataSource = ds; bsInfoCobro.DataMember = "Info_cobro";
            bsMunicipio.DataSource = ds; bsMunicipio.DataMember = "Municipio";
            bsEstadoContrato.DataSource = ds; bsEstadoContrato.DataMember = "Estado_contrato";
            bsUsuario.DataSource = ds; bsUsuario.DataMember = "Usuario";

            gridControlContrato.DataSource = bsContrato;
            gridControlContrato.ForceInitialize();
            gridViewContrato.PopulateColumns();

            // DataLayoutControl editará la fila actual
            dataLayoutControl1.DataSource = bsContrato;
        }



        private void ConfigurarDataLayout()
        {
            dataLayoutControl1.RetrieveFields();

            SetLookupInLayout("id_cliente", bsCliente, "nombre", "id_cliente");
            SetLookupInLayout("id_info_cobro", bsInfoCobro, "direccion", "id_info_cobro");
            SetLookupInLayout("id_municipio", bsMunicipio, "nombre_municipio", "id_municipio");
            SetLookupInLayout("id_estado_contrato", bsEstadoContrato, "nombre_estado_contrato", "id_estado_contrato");
            SetLookupInLayout("id_usuario", bsUsuario, "nombre_usuario", "id_usuario");

           
            var deInicio = new DevExpress.XtraEditors.DateEdit
            {
                Properties =
    {
        CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Classic,
        VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False,
        Mask = { EditMask = "yyyy-MM-dd", UseMaskAsDisplayFormat = true }
    }
            };
            var itemInicio = dataLayoutControl1.GetItemByFieldName("fecha_inicio") as DevExpress.XtraLayout.LayoutControlItem;
            if (itemInicio != null)
            {
                itemInicio.Control = deInicio;
                deInicio.DataBindings.Add("EditValue", bsContrato, "fecha_inicio", true, DataSourceUpdateMode.OnPropertyChanged);
            }


            var deFin = GetEditor<DateEdit>("fecha_fin");
            if (deFin != null)
            {
                deFin.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Classic;
                deFin.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
            }

            var txtSaldo = GetEditor<TextEdit>("saldo_total");
            if (txtSaldo != null)
            {
                txtSaldo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtSaldo.Properties.Mask.EditMask = "n2";
                txtSaldo.Properties.Mask.UseMaskAsDisplayFormat = true;
            }

            dataLayoutControl1.BestFit();
        }


        private T GetEditor<T>(string fieldName) where T : Control
        {
            var item = dataLayoutControl1.GetItemByFieldName(fieldName) as LayoutControlItem;
            return item?.Control as T;
        }
        private void SetLookupInLayout(string fieldName, object dataSource, string display, string value)
        {
            var item = dataLayoutControl1.GetItemByFieldName(fieldName) as LayoutControlItem;
            if (item == null) return;

            var lookup = new LookUpEdit
            {
                Properties =
                {
                    DataSource = dataSource,
                    DisplayMember = display,
                    ValueMember = value,
                    NullText = ""
                }
            };
            item.Control = lookup;
            lookup.DataBindings.Add("EditValue", bsContrato, fieldName, true, DataSourceUpdateMode.OnPropertyChanged);
        }

        

        private void FrmContrato_Load(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                gridViewContrato.CloseEditor();
                bsContrato.EndEdit();
                Validate();

                manager.UpdateAll(ds);

                XtraMessageBox.Show("Cambios guardados.");
                taContrato.Fill(ds.Contrato);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al guardar: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            gridViewContrato.CloseEditor();
            if (gridViewContrato.IsNewItemRow(gridViewContrato.FocusedRowHandle))
            {
                gridViewContrato.CancelUpdateCurrentRow();
                return;
            }

            var row = (bsContrato.Current as DataRowView)?.Row as db_ac0671_finalDataSet1.ContratoRow;
            if (row == null) return;

            if (XtraMessageBox.Show("¿Eliminar contrato seleccionado?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                row.Delete();
                manager.UpdateAll(ds);
                taContrato.Fill(ds.Contrato);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error al eliminar: " + ex.Message);
            }
        }
    }
    }


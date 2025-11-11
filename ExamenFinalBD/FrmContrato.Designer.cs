namespace ExamenFinalBD
{
    partial class FrmContrato
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gridControlContrato = new DevExpress.XtraGrid.GridControl();
            this.gridViewContrato = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.btnEliminar = new DevExpress.XtraEditors.SimpleButton();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.bindingSourceContrato = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceCliente = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceInfoCobro = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceMunicipio = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceEstadoContrato = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceUsuario = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlContrato)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewContrato)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceContrato)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceCliente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceInfoCobro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceMunicipio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceEstadoContrato)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceUsuario)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlContrato
            // 
            this.gridControlContrato.Location = new System.Drawing.Point(27, 31);
            this.gridControlContrato.MainView = this.gridViewContrato;
            this.gridControlContrato.Name = "gridControlContrato";
            this.gridControlContrato.Size = new System.Drawing.Size(569, 301);
            this.gridControlContrato.TabIndex = 0;
            this.gridControlContrato.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewContrato});
            // 
            // gridViewContrato
            // 
            this.gridViewContrato.GridControl = this.gridControlContrato;
            this.gridViewContrato.Name = "gridViewContrato";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Location = new System.Drawing.Point(0, 0);
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(495, 194);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(305, 366);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 2;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(745, 366);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 23);
            this.btnEliminar.TabIndex = 3;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Location = new System.Drawing.Point(634, 31);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.layoutControlGroup1;
            this.dataLayoutControl1.Size = new System.Drawing.Size(447, 300);
            this.dataLayoutControl1.TabIndex = 4;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(447, 300);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // FrmContrato
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1115, 523);
            this.Controls.Add(this.dataLayoutControl1);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.gridControlContrato);
            this.Name = "FrmContrato";
            this.Text = "FrmContrato";
            this.Load += new System.EventHandler(this.FrmContrato_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlContrato)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewContrato)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceContrato)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceCliente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceInfoCobro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceMunicipio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceEstadoContrato)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceUsuario)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlContrato;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewContrato;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
        private DevExpress.XtraEditors.SimpleButton btnEliminar;
        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private System.Windows.Forms.BindingSource bindingSourceContrato;
        private System.Windows.Forms.BindingSource bindingSourceCliente;
        private System.Windows.Forms.BindingSource bindingSourceInfoCobro;
        private System.Windows.Forms.BindingSource bindingSourceMunicipio;
        private System.Windows.Forms.BindingSource bindingSourceEstadoContrato;
        private System.Windows.Forms.BindingSource bindingSourceUsuario;
    }
}
namespace ExamenFinalBD
{
    partial class FrmCliente
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
            this.gridControlCliente = new DevExpress.XtraGrid.GridControl();
            this.gridViewCliente = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.btnEliminar = new DevExpress.XtraEditors.SimpleButton();
            this.bindingSourceCliente = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceInfoCobro = new System.Windows.Forms.BindingSource(this.components);
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlCliente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCliente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceCliente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceInfoCobro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlCliente
            // 
            this.gridControlCliente.Location = new System.Drawing.Point(12, 12);
            this.gridControlCliente.MainView = this.gridViewCliente;
            this.gridControlCliente.Name = "gridControlCliente";
            this.gridControlCliente.Size = new System.Drawing.Size(638, 217);
            this.gridControlCliente.TabIndex = 0;
            this.gridControlCliente.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewCliente});
            // 
            // gridViewCliente
            // 
            this.gridViewCliente.GridControl = this.gridControlCliente;
            this.gridViewCliente.Name = "gridViewCliente";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(362, 293);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 1;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(630, 293);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 23);
            this.btnEliminar.TabIndex = 2;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Location = new System.Drawing.Point(805, 22);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.Root;
            this.dataLayoutControl1.Size = new System.Drawing.Size(273, 376);
            this.dataLayoutControl1.TabIndex = 3;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(273, 376);
            this.Root.TextVisible = false;
            // 
            // FrmCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 460);
            this.Controls.Add(this.dataLayoutControl1);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.gridControlCliente);
            this.Name = "FrmCliente";
            this.Text = "FrmCliente";
            this.Load += new System.EventHandler(this.FrmCliente_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlCliente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCliente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceCliente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceInfoCobro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlCliente;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewCliente;
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
        private DevExpress.XtraEditors.SimpleButton btnEliminar;
        private System.Windows.Forms.BindingSource bindingSourceCliente;
        private System.Windows.Forms.BindingSource bindingSourceInfoCobro;
        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
    }
}
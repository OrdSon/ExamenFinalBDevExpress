namespace ExamenFinalBD
{
    partial class Configuración
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
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            this.btnEliminar = new DevExpress.XtraEditors.SimpleButton();
            this.btnActualizar = new DevExpress.XtraEditors.SimpleButton();
            this.gridViewConfiguracion = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControlConfiguracion = new DevExpress.XtraGrid.GridControl();
            this.bindingSourceConfiguracion = new System.Windows.Forms.BindingSource(this.components);
            this.ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewConfiguracion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlConfiguracion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceConfiguracion)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(126, 315);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 1;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(636, 315);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 23);
            this.btnEliminar.TabIndex = 2;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Location = new System.Drawing.Point(388, 315);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(75, 23);
            this.btnActualizar.TabIndex = 3;
            this.btnActualizar.Text = "Actualizar";
            // 
            // gridViewConfiguracion
            // 
            this.gridViewConfiguracion.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ID,
            this.gridColumn2,
            this.gridColumn3});
            this.gridViewConfiguracion.GridControl = this.gridControlConfiguracion;
            this.gridViewConfiguracion.Name = "gridViewConfiguracion";
            this.gridViewConfiguracion.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            // 
            // gridControlConfiguracion
            // 
            this.gridControlConfiguracion.DataSource = this.bindingSourceConfiguracion;
            this.gridControlConfiguracion.Location = new System.Drawing.Point(46, 12);
            this.gridControlConfiguracion.MainView = this.gridViewConfiguracion;
            this.gridControlConfiguracion.Name = "gridControlConfiguracion";
            this.gridControlConfiguracion.Size = new System.Drawing.Size(716, 283);
            this.gridControlConfiguracion.TabIndex = 4;
            this.gridControlConfiguracion.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewConfiguracion});
            // 
            // ID
            // 
            this.ID.Caption = "ID";
            this.ID.Name = "ID";
            this.ID.Visible = true;
            this.ID.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Mora (Q)";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Inscripción";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // Configuración
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gridControlConfiguracion);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnGuardar);
            this.Name = "Configuración";
            this.Text = "Configuración";
            this.Load += new System.EventHandler(this.Configuración_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewConfiguracion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlConfiguracion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceConfiguracion)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
        private DevExpress.XtraEditors.SimpleButton btnEliminar;
        private DevExpress.XtraEditors.SimpleButton btnActualizar;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewConfiguracion;
        private DevExpress.XtraGrid.GridControl gridControlConfiguracion;
        private System.Windows.Forms.BindingSource bindingSourceConfiguracion;
        private DevExpress.XtraGrid.Columns.GridColumn ID;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
    }
}
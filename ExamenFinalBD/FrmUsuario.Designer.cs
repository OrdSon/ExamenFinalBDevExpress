namespace ExamenFinalBD
{
    partial class FrmUsuario
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
            this.gridControlUsuario = new DevExpress.XtraGrid.GridControl();
            this.gridViewUsuario = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.bindingSourceUsuario = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceTipoUsuario = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceEstadoUsuario = new System.Windows.Forms.BindingSource(this.components);
            this.btnEliminar = new DevExpress.XtraEditors.SimpleButton();
            this.btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlUsuario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewUsuario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceUsuario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceTipoUsuario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceEstadoUsuario)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlUsuario
            // 
            this.gridControlUsuario.Location = new System.Drawing.Point(34, 12);
            this.gridControlUsuario.MainView = this.gridViewUsuario;
            this.gridControlUsuario.Name = "gridControlUsuario";
            this.gridControlUsuario.Size = new System.Drawing.Size(719, 296);
            this.gridControlUsuario.TabIndex = 0;
            this.gridControlUsuario.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewUsuario});
            // 
            // gridViewUsuario
            // 
            this.gridViewUsuario.GridControl = this.gridControlUsuario;
            this.gridViewUsuario.Name = "gridViewUsuario";
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(415, 345);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 23);
            this.btnEliminar.TabIndex = 5;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click_1);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(265, 345);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 4;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // FrmUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.gridControlUsuario);
            this.Name = "FrmUsuario";
            this.Text = "FrmUsuario";
            this.Load += new System.EventHandler(this.FrmUsuario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlUsuario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewUsuario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceUsuario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceTipoUsuario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceEstadoUsuario)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlUsuario;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewUsuario;
        private System.Windows.Forms.BindingSource bindingSourceUsuario;
        private System.Windows.Forms.BindingSource bindingSourceTipoUsuario;
        private System.Windows.Forms.BindingSource bindingSourceEstadoUsuario;
        private DevExpress.XtraEditors.SimpleButton btnEliminar;
        private DevExpress.XtraEditors.SimpleButton btnGuardar;
    }
}
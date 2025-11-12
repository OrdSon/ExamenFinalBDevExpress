namespace ExamenFinalBD.Tecnico
{
    partial class Retomar
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
            this.gridControlListaPendientes = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlListaPendientes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlListaPendientes
            // 
            this.gridControlListaPendientes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlListaPendientes.Location = new System.Drawing.Point(0, 0);
            this.gridControlListaPendientes.MainView = this.gridView1;
            this.gridControlListaPendientes.Name = "gridControlListaPendientes";
            this.gridControlListaPendientes.Size = new System.Drawing.Size(853, 747);
            this.gridControlListaPendientes.TabIndex = 0;
            this.gridControlListaPendientes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControlListaPendientes.DoubleClick += new System.EventHandler(this.gridControlListaPendientes_DoubleClick);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControlListaPendientes;
            this.gridView1.Name = "gridView1";
            // 
            // Retomar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 747);
            this.Controls.Add(this.gridControlListaPendientes);
            this.Name = "Retomar";
            this.Text = "Retomar";
            this.Load += new System.EventHandler(this.Retomar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlListaPendientes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlListaPendientes;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}
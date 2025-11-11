namespace ExamenFinalBD.Tecnico
{
    partial class BuscarRepuesto
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
            this.gridControlListaRepuestos = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlListaRepuestos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlListaRepuestos
            // 
            this.gridControlListaRepuestos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlListaRepuestos.Location = new System.Drawing.Point(0, 0);
            this.gridControlListaRepuestos.MainView = this.gridView1;
            this.gridControlListaRepuestos.Name = "gridControlListaRepuestos";
            this.gridControlListaRepuestos.Size = new System.Drawing.Size(847, 735);
            this.gridControlListaRepuestos.TabIndex = 0;
            this.gridControlListaRepuestos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControlListaRepuestos.DoubleClick += new System.EventHandler(this.gridControlListaRepuestos_DoubleClick);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControlListaRepuestos;
            this.gridView1.Name = "gridView1";
            // 
            // BuscarRepuesto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 735);
            this.Controls.Add(this.gridControlListaRepuestos);
            this.Name = "BuscarRepuesto";
            this.Text = "Repuesto";
            this.Load += new System.EventHandler(this.BuscarRepuesto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlListaRepuestos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlListaRepuestos;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}
namespace ExamenFinalBD.Tecnico
{
    partial class BuscaContrato
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
            this.gridControlContratos = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlContratos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlContratos
            // 
            this.gridControlContratos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlContratos.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.gridControlContratos.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridControlContratos.Location = new System.Drawing.Point(0, 0);
            this.gridControlContratos.MainView = this.gridView1;
            this.gridControlContratos.Margin = new System.Windows.Forms.Padding(4);
            this.gridControlContratos.Name = "gridControlContratos";
            this.gridControlContratos.Size = new System.Drawing.Size(876, 649);
            this.gridControlContratos.TabIndex = 0;
            this.gridControlContratos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControlContratos.DoubleClick += new System.EventHandler(this.gridControlContratos_DoubleClick);
            // 
            // gridView1
            // 
            this.gridView1.DetailHeight = 443;
            this.gridView1.GridControl = this.gridControlContratos;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            // 
            // BuscaContrato
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 649);
            this.Controls.Add(this.gridControlContratos);
            this.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "BuscaContrato";
            this.Text = "BuscaContrato";
            this.Load += new System.EventHandler(this.BuscaContrato_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlContratos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlContratos;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}
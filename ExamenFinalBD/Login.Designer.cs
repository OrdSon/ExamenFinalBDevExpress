namespace ExamenFinalBD
{
    partial class Login
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
            this.directXFormContainerControl1 = new DevExpress.XtraEditors.DirectXFormContainerControl();
            this.skinPaletteDropDownButtonItem1 = new DevExpress.XtraBars.SkinPaletteDropDownButtonItem();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.simpleSeparator4 = new DevExpress.XtraLayout.SimpleSeparator();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.directXFormContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator4)).BeginInit();
            this.SuspendLayout();
            // 
            // directXFormContainerControl1
            // 
            this.directXFormContainerControl1.Controls.Add(this.panelControl1);
            this.directXFormContainerControl1.Location = new System.Drawing.Point(1, 39);
            this.directXFormContainerControl1.Name = "directXFormContainerControl1";
            this.directXFormContainerControl1.Size = new System.Drawing.Size(1022, 554);
            this.directXFormContainerControl1.TabIndex = 0;
            this.directXFormContainerControl1.SizeChanged += new System.EventHandler(this.directXFormContainerControl1_SizeChanged);
            // 
            // skinPaletteDropDownButtonItem1
            // 
            this.skinPaletteDropDownButtonItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.skinPaletteDropDownButtonItem1.Id = 0;
            this.skinPaletteDropDownButtonItem1.Name = "skinPaletteDropDownButtonItem1";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.flowLayoutPanel1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1022, 554);
            this.panelControl1.TabIndex = 0;
            this.panelControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.panelControl1_Paint_1);
            // 
            // simpleSeparator4
            // 
            this.simpleSeparator4.AllowHotTrack = false;
            this.simpleSeparator4.Location = new System.Drawing.Point(0, 0);
            this.simpleSeparator4.Name = "simpleSeparator4";
            this.simpleSeparator4.Size = new System.Drawing.Size(0, 0);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.flowLayoutPanel1.Location = new System.Drawing.Point(417, 138);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(600, 800);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ChildControls.Add(this.directXFormContainerControl1);
            this.ClientSize = new System.Drawing.Size(1024, 594);
            this.Links.Add(this.skinPaletteDropDownButtonItem1);
            this.Name = "Login";
            this.Text = "Login";
            this.directXFormContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.DirectXFormContainerControl directXFormContainerControl1;
        private DevExpress.XtraBars.SkinPaletteDropDownButtonItem skinPaletteDropDownButtonItem1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}
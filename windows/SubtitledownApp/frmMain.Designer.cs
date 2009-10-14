namespace SubtitledownApp
{
    partial class frmMain
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Liberare le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTelefilm = new System.Windows.Forms.TextBox();
            this.txtStagione = new System.Windows.Forms.TextBox();
            this.txtPuntata = new System.Windows.Forms.TextBox();
            this.btnSalva = new System.Windows.Forms.Button();
            this.btnAllineaCartella = new System.Windows.Forms.Button();
            this.lblProcesso = new System.Windows.Forms.Label();
            this.chkSincrono = new System.Windows.Forms.CheckBox();
            this.chkAllineaSubDir = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripApri = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripAllinea = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripPreferenze = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripEsci = new System.Windows.Forms.ToolStripMenuItem();
            this.timerDown = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Serie televisiva:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Stagione:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(128, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Puntata:";
            // 
            // txtTelefilm
            // 
            this.txtTelefilm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTelefilm.Location = new System.Drawing.Point(15, 25);
            this.txtTelefilm.Name = "txtTelefilm";
            this.txtTelefilm.Size = new System.Drawing.Size(355, 20);
            this.txtTelefilm.TabIndex = 3;
            // 
            // txtStagione
            // 
            this.txtStagione.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStagione.Location = new System.Drawing.Point(15, 64);
            this.txtStagione.Name = "txtStagione";
            this.txtStagione.Size = new System.Drawing.Size(100, 20);
            this.txtStagione.TabIndex = 4;
            // 
            // txtPuntata
            // 
            this.txtPuntata.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPuntata.Location = new System.Drawing.Point(131, 64);
            this.txtPuntata.Name = "txtPuntata";
            this.txtPuntata.Size = new System.Drawing.Size(100, 20);
            this.txtPuntata.TabIndex = 5;
            // 
            // btnSalva
            // 
            this.btnSalva.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalva.Location = new System.Drawing.Point(295, 62);
            this.btnSalva.Name = "btnSalva";
            this.btnSalva.Size = new System.Drawing.Size(75, 23);
            this.btnSalva.TabIndex = 6;
            this.btnSalva.Text = "Salva";
            this.btnSalva.UseVisualStyleBackColor = true;
            this.btnSalva.Click += new System.EventHandler(this.btnSalva_Click);
            // 
            // btnAllineaCartella
            // 
            this.btnAllineaCartella.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAllineaCartella.Location = new System.Drawing.Point(15, 128);
            this.btnAllineaCartella.Name = "btnAllineaCartella";
            this.btnAllineaCartella.Size = new System.Drawing.Size(133, 23);
            this.btnAllineaCartella.TabIndex = 7;
            this.btnAllineaCartella.Text = "Allinea cartella";
            this.btnAllineaCartella.UseVisualStyleBackColor = true;
            this.btnAllineaCartella.Click += new System.EventHandler(this.btnAllineaCartella_Click);
            // 
            // lblProcesso
            // 
            this.lblProcesso.AutoSize = true;
            this.lblProcesso.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcesso.Location = new System.Drawing.Point(12, 170);
            this.lblProcesso.Name = "lblProcesso";
            this.lblProcesso.Size = new System.Drawing.Size(114, 16);
            this.lblProcesso.TabIndex = 8;
            this.lblProcesso.Text = "Info processo...";
            // 
            // chkSincrono
            // 
            this.chkSincrono.AutoSize = true;
            this.chkSincrono.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkSincrono.Location = new System.Drawing.Point(216, 128);
            this.chkSincrono.Name = "chkSincrono";
            this.chkSincrono.Size = new System.Drawing.Size(114, 17);
            this.chkSincrono.TabIndex = 9;
            this.chkSincrono.Text = "Download sincrono";
            this.chkSincrono.UseVisualStyleBackColor = true;
            // 
            // chkAllineaSubDir
            // 
            this.chkAllineaSubDir.AutoSize = true;
            this.chkAllineaSubDir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkAllineaSubDir.Location = new System.Drawing.Point(216, 151);
            this.chkAllineaSubDir.Name = "chkAllineaSubDir";
            this.chkAllineaSubDir.Size = new System.Drawing.Size(114, 17);
            this.chkAllineaSubDir.TabIndex = 10;
            this.chkAllineaSubDir.Text = "Allinea sottocartelle";
            this.chkAllineaSubDir.UseVisualStyleBackColor = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripApri,
            this.ToolStripAllinea,
            this.toolStripPreferenze,
            this.toolStripSeparator1,
            this.toolStripEsci});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(155, 120);
            // 
            // toolStripApri
            // 
            this.toolStripApri.Image = ((System.Drawing.Image)(resources.GetObject("toolStripApri.Image")));
            this.toolStripApri.Name = "toolStripApri";
            this.toolStripApri.Size = new System.Drawing.Size(154, 22);
            this.toolStripApri.Text = "Apri";
            this.toolStripApri.Click += new System.EventHandler(this.toolStripApri_Click);
            // 
            // ToolStripAllinea
            // 
            this.ToolStripAllinea.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripAllinea.Image")));
            this.ToolStripAllinea.Name = "ToolStripAllinea";
            this.ToolStripAllinea.Size = new System.Drawing.Size(154, 22);
            this.ToolStripAllinea.Text = "Allinea cartelle";
            this.ToolStripAllinea.Click += new System.EventHandler(this.ToolStripAllinea_Click);
            // 
            // toolStripPreferenze
            // 
            this.toolStripPreferenze.Image = ((System.Drawing.Image)(resources.GetObject("toolStripPreferenze.Image")));
            this.toolStripPreferenze.Name = "toolStripPreferenze";
            this.toolStripPreferenze.Size = new System.Drawing.Size(154, 22);
            this.toolStripPreferenze.Text = "Preferenze";
            this.toolStripPreferenze.Click += new System.EventHandler(this.toolStripPreferenze_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(151, 6);
            // 
            // toolStripEsci
            // 
            this.toolStripEsci.Image = ((System.Drawing.Image)(resources.GetObject("toolStripEsci.Image")));
            this.toolStripEsci.Name = "toolStripEsci";
            this.toolStripEsci.Size = new System.Drawing.Size(154, 22);
            this.toolStripEsci.Text = "Esci";
            this.toolStripEsci.Click += new System.EventHandler(this.toolStripEsci_Click);
            // 
            // timerDown
            // 
            this.timerDown.Interval = 500;
            this.timerDown.Tick += new System.EventHandler(this.timerDown_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 208);
            this.Controls.Add(this.chkAllineaSubDir);
            this.Controls.Add(this.chkSincrono);
            this.Controls.Add(this.lblProcesso);
            this.Controls.Add(this.btnAllineaCartella);
            this.Controls.Add(this.btnSalva);
            this.Controls.Add(this.txtPuntata);
            this.Controls.Add(this.txtStagione);
            this.Controls.Add(this.txtTelefilm);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Subtitledown";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTelefilm;
        private System.Windows.Forms.TextBox txtStagione;
        private System.Windows.Forms.TextBox txtPuntata;
        private System.Windows.Forms.Button btnSalva;
        private System.Windows.Forms.Button btnAllineaCartella;
        private System.Windows.Forms.Label lblProcesso;
        private System.Windows.Forms.CheckBox chkSincrono;
        private System.Windows.Forms.CheckBox chkAllineaSubDir;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripApri;
        private System.Windows.Forms.ToolStripMenuItem toolStripPreferenze;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripEsci;
        private System.Windows.Forms.ToolStripMenuItem ToolStripAllinea;
        private System.Windows.Forms.Timer timerDown;
    }
}


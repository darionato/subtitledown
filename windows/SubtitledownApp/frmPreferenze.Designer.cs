namespace SubtitledownApp
{
    partial class frmPreferenze
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPreferenze));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chkSincrono = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUtente = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chkSottoDir = new System.Windows.Forms.CheckBox();
            this.btnDelDir = new System.Windows.Forms.Button();
            this.btnAddDir = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.btnSalva = new System.Windows.Forms.Button();
            this.btnChiudi = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(497, 236);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chkSincrono);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(489, 207);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Impostazioni";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // chkSincrono
            // 
            this.chkSincrono.AutoSize = true;
            this.chkSincrono.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkSincrono.Location = new System.Drawing.Point(24, 134);
            this.chkSincrono.Name = "chkSincrono";
            this.chkSincrono.Size = new System.Drawing.Size(102, 17);
            this.chkSincrono.TabIndex = 1;
            this.chkSincrono.Tag = "sync";
            this.chkSincrono.Text = "Scarico sincrono";
            this.chkSincrono.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPass);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtUtente);
            this.groupBox1.Location = new System.Drawing.Point(15, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(242, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Login italiansubs.net";
            // 
            // txtPass
            // 
            this.txtPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPass.Location = new System.Drawing.Point(76, 62);
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '*';
            this.txtPass.Size = new System.Drawing.Size(144, 20);
            this.txtPass.TabIndex = 3;
            this.txtPass.Tag = "pass";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Utente:";
            // 
            // txtUtente
            // 
            this.txtUtente.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUtente.Location = new System.Drawing.Point(76, 32);
            this.txtUtente.Name = "txtUtente";
            this.txtUtente.Size = new System.Drawing.Size(144, 20);
            this.txtUtente.TabIndex = 0;
            this.txtUtente.Tag = "user";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chkSottoDir);
            this.tabPage2.Controls.Add(this.btnDelDir);
            this.tabPage2.Controls.Add(this.btnAddDir);
            this.tabPage2.Controls.Add(this.listView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(489, 207);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Cartelle";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // chkSottoDir
            // 
            this.chkSottoDir.AutoSize = true;
            this.chkSottoDir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkSottoDir.Location = new System.Drawing.Point(369, 77);
            this.chkSottoDir.Name = "chkSottoDir";
            this.chkSottoDir.Size = new System.Drawing.Size(114, 17);
            this.chkSottoDir.TabIndex = 3;
            this.chkSottoDir.Tag = "recursive";
            this.chkSottoDir.Text = "Includi sottocartelle";
            this.chkSottoDir.UseVisualStyleBackColor = true;
            // 
            // btnDelDir
            // 
            this.btnDelDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelDir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelDir.Location = new System.Drawing.Point(369, 47);
            this.btnDelDir.Name = "btnDelDir";
            this.btnDelDir.Size = new System.Drawing.Size(100, 23);
            this.btnDelDir.TabIndex = 2;
            this.btnDelDir.Text = "Rimuovi";
            this.btnDelDir.UseVisualStyleBackColor = true;
            this.btnDelDir.Click += new System.EventHandler(this.btnDelDir_Click);
            // 
            // btnAddDir
            // 
            this.btnAddDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddDir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddDir.Location = new System.Drawing.Point(368, 17);
            this.btnAddDir.Name = "btnAddDir";
            this.btnAddDir.Size = new System.Drawing.Size(101, 23);
            this.btnAddDir.TabIndex = 1;
            this.btnAddDir.Text = "Aggiungi";
            this.btnAddDir.UseVisualStyleBackColor = true;
            this.btnAddDir.Click += new System.EventHandler(this.btnAddDir_Click);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView1.Location = new System.Drawing.Point(22, 17);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(335, 172);
            this.listView1.TabIndex = 0;
            this.listView1.Tag = "directory";
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            // 
            // btnSalva
            // 
            this.btnSalva.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalva.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalva.Location = new System.Drawing.Point(339, 257);
            this.btnSalva.Name = "btnSalva";
            this.btnSalva.Size = new System.Drawing.Size(75, 23);
            this.btnSalva.TabIndex = 4;
            this.btnSalva.Text = "Salva";
            this.btnSalva.UseVisualStyleBackColor = true;
            this.btnSalva.Click += new System.EventHandler(this.btnSalva_Click);
            // 
            // btnChiudi
            // 
            this.btnChiudi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChiudi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChiudi.Location = new System.Drawing.Point(430, 257);
            this.btnChiudi.Name = "btnChiudi";
            this.btnChiudi.Size = new System.Drawing.Size(75, 23);
            this.btnChiudi.TabIndex = 5;
            this.btnChiudi.Text = "Chiudi";
            this.btnChiudi.UseVisualStyleBackColor = true;
            this.btnChiudi.Click += new System.EventHandler(this.btnChiudi_Click);
            // 
            // frmPreferenze
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 286);
            this.Controls.Add(this.btnChiudi);
            this.Controls.Add(this.btnSalva);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPreferenze";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Preferenze";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmPreferenze_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPreferenze_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPreferenze_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUtente;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox chkSincrono;
        private System.Windows.Forms.Button btnDelDir;
        private System.Windows.Forms.Button btnAddDir;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.CheckBox chkSottoDir;
        private System.Windows.Forms.Button btnSalva;
        private System.Windows.Forms.Button btnChiudi;
    }
}
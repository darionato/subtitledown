using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using Badlydone.Subtitledown;

namespace SubtitledownApp
{
    public partial class frmMain : Form
    {

        private clSubtitledown m_SubDown;
        private NotifyIcon m_Tray;
        private bool m_Close = false;
        private Int16 m_CurrentIconDown = 1;

        public frmMain()
        {
            InitializeComponent();

            m_SubDown = new clSubtitledown();
        }

        private string PathConf
        {
            get { return System.IO.Path.Combine(Application.StartupPath, "Subtitledownapp.xml"); }
        }

        private bool CkeckConf()
        {

            if (File.Exists(this.PathConf) == false)
            {
                if (MessageBox.Show("Nessuna configurazione trovata!\nAprire la maschera delle impostazioni?", "Configurazione", MessageBoxButtons.YesNo , MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    return this.OpenPreferences(true);
                }
                return false;
            }
            else
            {
                return true;
            }

        }

        private void AllineaCartelle()
        {
            
            if (this.CkeckConf() == false) return;

            if (File.Exists(this.PathConf) == false) return;

            DataSet ds = new DataSet();
            ds.ReadXml(this.PathConf);

            if (ds.Tables.Count > 0)
            {

                DataTable tb_dir = ds.Tables["directories"];

                if (tb_dir == null) return;

                this.TrayIconWorking = true;
                foreach (DataRow riga in tb_dir.Rows)
                {

                    if (Directory.Exists(riga[0].ToString()) == true)
                    {
                        this.AllineaCartella(riga[0].ToString());
                    }

                }
                this.TrayIconWorking = false;

            }

        }

        private void AllineaCartella(string percorso)
        {

            clDirSubNice dir_align = new clDirSubNice();
            dir_align.Sincrono = chkSincrono.Checked;
            dir_align.Ricorsivo = chkAllineaSubDir.Checked;
            dir_align.Directory = percorso;

            dir_align.WorkOutDir();

        }

        private void btnSalva_Click(object sender, EventArgs e)
        {

            if (this.CkeckConf() == false) return;

            FolderBrowserDialog il_save = new FolderBrowserDialog();
            //il_save.Title = "Salva sottotitolo in...";
            //il_save.Filter = "Tutti i files|*.*";
            if (il_save.ShowDialog() == DialogResult.OK)
            {

                m_SubDown.DirDownload = il_save.SelectedPath;
                m_SubDown.InfoSerie.Telefilm = this.txtTelefilm.Text;
                m_SubDown.InfoSerie.Serie = Convert.ToInt32(this.txtStagione.Text);
                m_SubDown.InfoSerie.Puntata = Convert.ToInt32(this.txtPuntata.Text);

                this.TrayIconWorking = true;
                this.lblProcesso.Text = "Scarico in corso...";

                if (chkSincrono.Checked)
                {
                    m_SubDown.DownloadSub();
                }
                else
                {
                    m_SubDown.DownloadSubAsynch();
                    m_SubDown.WaitDone();
                }
                this.lblProcesso.Text = "Scarico completato";
                this.TrayIconWorking = false;

            }

        }

        private void btnAllineaCartella_Click(object sender, EventArgs e)
        {

            if (this.CkeckConf() == false) return;

            FolderBrowserDialog il_open = new FolderBrowserDialog();
            if (il_open.ShowDialog() == DialogResult.OK)
            {
                this.TrayIconWorking = true;
                this.lblProcesso.Text = "Inizio allineamento in corso...";
                this.AllineaCartella(il_open.SelectedPath);
                this.lblProcesso.Text = "Allineamento completato";
                this.TrayIconWorking = false;
            }

        }

        private void ShowForm()
        {
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Normal;
            this.Visible = true;
            this.Show();
        }

        private void HideForm()
        {
            this.ShowInTaskbar = false;
            this.Hide();
        }

        private bool TrayIconWorking
        {
            get
            {
                return timerDown.Enabled;
            }

            set
            {
                timerDown.Enabled = value;
                if (value == false) m_Tray.Icon = getIconByIndex(0);
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Icon = this.getIconByIndex(0);

            m_Tray = new NotifyIcon();
            m_Tray.DoubleClick += new EventHandler(m_Tray_DoubleClick);
            m_Tray.ContextMenuStrip = this.contextMenuStrip1;
            m_Tray.Icon = getIconByIndex(0);
            m_Tray.Visible = true;

        }

        void m_Tray_DoubleClick(object sender, EventArgs e)
        {
            this.ShowForm();
        }

        private void toolStripEsci_Click(object sender, EventArgs e)
        {
            m_Close = true;
            this.Close();
            Application.Exit();
        }

        private void toolStripPreferenze_Click(object sender, EventArgs e)
        {
            this.OpenPreferences(false);
        }

        private bool OpenPreferences(bool is_modal)
        {
            frmPreferenze pref = new frmPreferenze();
            if (is_modal == true)
            {
                pref.Show();
                while (pref.IsClosed == false)
                {
                    Application.DoEvents();
                }
                return (pref.DialogResult == DialogResult.Yes);
            }
            else
            {
                pref.Show();
                return true;
            }
        }


        private void toolStripApri_Click(object sender, EventArgs e)
        {
            this.ShowForm();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_Tray.Visible = false;
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            this.HideForm();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_Close == false)
            {
                e.Cancel = true;
                this.HideForm();
            }
            
        }

        private void ToolStripAllinea_Click(object sender, EventArgs e)
        {
            this.AllineaCartelle();
        }

        private Icon getIconByIndex(Int16 index)
        {
            Assembly a = Assembly.GetExecutingAssembly();

            Bitmap the_bitmap = new Bitmap(a.GetManifestResourceStream("SubtitledownApp.icons.sub_down_" + index.ToString() + ".png"));

            IntPtr iii = the_bitmap.GetHicon();
            Icon ic = Icon.FromHandle(iii);
            return ic;
        }

        private void timerDown_Tick(object sender, EventArgs e)
        {
            if (m_CurrentIconDown > 3) m_CurrentIconDown = 1;

            m_Tray.Icon = this.getIconByIndex(m_CurrentIconDown);
            m_CurrentIconDown++;
        }

    }
}

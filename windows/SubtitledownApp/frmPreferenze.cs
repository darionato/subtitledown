using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SubtitledownApp
{
    public partial class frmPreferenze : Form
    {

        private bool m_IsClosed = false;

        public frmPreferenze()
        {
            InitializeComponent();
        }

        public bool IsClosed 
        {
            get
            {
                return m_IsClosed;
            }
        }

        private void btnChiudi_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog il_open = new FolderBrowserDialog();
            if (il_open.ShowDialog() == DialogResult.OK)
            {
                ListViewItem L_ = this.listView1.Items.Add(il_open.SelectedPath);
            }
        }

        private void btnDelDir_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 0) return;


            foreach (ListViewItem L_ in this.listView1.SelectedItems)
            {
                this.listView1.Items.Remove(L_);
            }
        }

        private void btnSalva_Click(object sender, EventArgs e)
        {
            this.SaveConf();
        }

        private string PathConf
        {
            get { return System.IO.Path.Combine(Application.StartupPath, "Subtitledownapp.xml"); }
        }

        private void SaveConf()
        {
            DataTable tb_opz = new DataTable("options");
            tb_opz.Columns.Add("option",  typeof(string));
            tb_opz.Columns.Add("value", typeof(string));

            tb_opz.Rows.Add(new object[] {"user",this.txtUtente.Text});
            tb_opz.Rows.Add(new object[] {"pass", this.txtPass.Text});
            tb_opz.Rows.Add(new object[] {"sync", (this.chkSincrono.Checked==true?1:0) });
            tb_opz.Rows.Add(new object[] {"recursive", (this.chkSottoDir.Checked == true ? 1 : 0) });


            DataTable tb_dir = new DataTable("directories");
            tb_dir.Columns.Add("directory", typeof(string));

            foreach (ListViewItem L_ in this.listView1.Items)
            {
                tb_dir.Rows.Add(new object[] {L_.Text});
            }

            DataSet ds = new DataSet("SubtitledownOptions");
            ds.Tables.Add(tb_opz);
            ds.Tables.Add(tb_dir);

            ds.WriteXml(this.PathConf);
        }

        private void OpenConf()
        {

            if (System.IO.File.Exists(this.PathConf) == false) return;

            DataSet ds = new DataSet();
            ds.ReadXml(this.PathConf);

            if (ds.Tables.Count > 0)
            {
                DataTable tb_opt = ds.Tables["options"];
                tb_opt.PrimaryKey = new DataColumn[] { ds.Tables["options"].Columns["option"] };

                DataTable tb_dir = ds.Tables["directories"];

                this.OpenSubControls(ref tb_opt, ref tb_dir, this.Controls);

            }

        }

        private void OpenSubControls(ref DataTable tb_opt, ref DataTable tb_dir, Control.ControlCollection cc)
        {
            DataRow dr;
            Object val;

            foreach (Control c in cc)
            {

                if (tb_dir != null && c.GetType().Name == "ListView" && c.Name == listView1.Name)
                {
                    ListView li = c as ListView;
                    li.Items.Clear();
                    foreach (DataRow the_dir in tb_dir.Rows)
                    {
                        li.Items.Add(the_dir["directory"].ToString());
                    }
                    continue;
                }

                if (c.Tag != null && c.Tag.ToString().Length > 0)
                {
                    dr = tb_opt.Rows.Find(c.Tag);
                    if (dr == null) continue;

                    val = dr["value"].ToString();

                    if (c.GetType().Name == "TextBox")
                    {
                        c.Text = val.ToString();
                    }
                    else if (c.GetType().Name == "CheckBox")
                    {
                        CheckBox ck = c as CheckBox;
                        ck.Checked = (val.ToString() == "1");
                    }
                    
                }

                this.OpenSubControls(ref tb_opt,ref tb_dir, c.Controls);

            }
        }

        private void frmPreferenze_Load(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.OpenConf();
        }

        private void frmPreferenze_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = (File.Exists(this.PathConf) ? DialogResult.Yes : DialogResult.No);
        }

        private void frmPreferenze_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_IsClosed = true;
        }

    }
}

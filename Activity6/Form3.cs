using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Activity6
{
    public partial class Form3 : Form
    {
        private string stringConnection = "data source=Danan-Nitro;" + "database=ProdiStatus;User ID=sa;Password=123";
        private SqlConnection koneksi;
        private string nim, nama, alamat, jk, proo;

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private DateTime tgl;
        BindingSource customersBindingSource = new BindingSource;
        public Form3()
        {
            InitializeComponent();
            koneksi = new SqlConnection(kstr);
            this.bnMahasiswa.BindingSource = this.customersBindingSource;
            refreshform();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            koneksi.Open();
            string str = "select nama_prodi from dbo.Prodi";
            SqlCommand cmd = new SqlCommand(str, koneksi);
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cmd.ExecuteReader();
            koneksi.Close();
            cbxProdi.DisplayMember = "nama_prodi";
            cbxProdi.ValueMember = "id_prodi";
            cbxProdi.DataSource = ds.Tables[0];
        }

  

        private void Form3_Load(object sender, EventArgs e)
        {
            koneksi.Open();
            SqlDataAdapter dataAdapter1 = new SqlDataAdapter(new SqlCommand("Select m.nim, m.nama_mahasiswa, " + "m.alamat, m.jenis_kel, m.tgl_lahir,p.nama_prodi from dbo.Mahasiswa m " + "join dbo.Prodi p on m.id_prodi = p.id_prodi", koneksi));
            DataSet ds = new DataSet();
            dataAdapter1.Fill(ds);

            this.customersBindingSource.DataSource = ds.Tables[0];
            this.txtNIM.DataBinding.Add(
                new Binding("Text", this.customersBindingSource, "NIM", true));
            this.txtNama.DataBinding.Add(
                new Binding("Text", this.customersBindingSource, "Nama", true));
            this.txtAlamat.DataBinding.Add(
                new Binding("Text", this.customersBindingSource, "Alamat", true));
            this.cbxJenisKelamin.DataBinding.Add(new Binding("Text", this.customersBindingSource, "jenis_kel", true));
            this.dtTanggalLahir.DataBinding.Add(new Binding("Text", this.customersBindingSource, "tgl_lahir", true));
            this.cbxProdi.DataBinding.Add(new Binding("Text", this.customersBindingSource, "nama_prodi", true));
            koneksi.Close();
        }
        private void clearBinding()
        {
            this.txtNIM.DataBinding.Clear();
            this.txtNama.DataBinding.Clear();
            this.txtAlamat.DataBinding.Clear();
            this.cbxJenisKelamin.DataBinding.Clear();
            this.dtTanggalLahir.DataBinding.Clear();
            this.cbxProdi.DataBinding.Clear();
        }
        private void refreshform()
        {
            txtNIM.Enabled = false;
            txtNama.Enabled = false;
            txtAlamat.Enabled = false;
            cbxJenisKelamin.Enabled = false;
            dtTanggalLahir.Enabled = false;
            cbxProdi.Enabled = false;
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnClear.Enabled = false;
            clearBinding();
            Form3_Load();
        }
    }
}

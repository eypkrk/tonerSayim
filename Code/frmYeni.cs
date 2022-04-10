using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Toner
{
    public partial class frmYeni : Form
    {
        public frmYeni()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=Toner_Takip;Integrated Security=True");

        private void button6_Click(object sender, EventArgs e)
        {
            frmBirimEkle ekle = new frmBirimEkle();
            ekle.ShowDialog();
        }

        private void frmYeni_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from birim",baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtBirim.Items.Add(read["Birim"].ToString());
            }
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into tonerr(Marka,Adet,Renk,Birim,Kodu)values(@Marka,@Adet,@Renk,@Birim,@Kodu)", baglanti);
            komut.Parameters.AddWithValue("@Marka", txtMarka.Text);
            komut.Parameters.AddWithValue("@Adet", txtAdet.Text);
            komut.Parameters.AddWithValue("@Renk", txtRenk.Text);
            komut.Parameters.AddWithValue("@Birim", txtBirim.Text);
            komut.Parameters.AddWithValue("@Kodu", txtKod.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Eklendi");
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            StokListele listele = new StokListele();
            listele.ShowDialog();
        }
    }
}

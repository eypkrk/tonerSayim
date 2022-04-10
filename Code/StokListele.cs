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
    public partial class StokListele : Form
    {
        SqlConnection baglanti = new SqlConnection(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=Toner_Takip;Integrated Security=True");
        DataSet daset = new DataSet();
        public StokListele()
        {
            InitializeComponent();
        }
        
        private void StokListele_Load(object sender, EventArgs e)
        {
            Kayıt_Göster();

        }

        private void Kayıt_Göster()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select*from tonerr", baglanti);
            adtr.Fill(daset, "tonerr");
            dataGridView1.DataSource = daset.Tables["tonerr"];
            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete  from tonerr where Marka='" + dataGridView1.CurrentRow.Cells["Marka"].Value.ToString()+"'",baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["tonerr"].Clear();
            Kayıt_Göster();
            MessageBox.Show("Kayıt Silindi");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update tonerr set Kodu=@Kodu,Adet=@Adet,Renk=@Renk,Birim=@Birim where Marka=@Marka",baglanti);
            komut.Parameters.AddWithValue("@Marka", txtmarka.Text);
            komut.Parameters.AddWithValue("@Adet", txtadet.Text);
            komut.Parameters.AddWithValue("@Renk", txtrenk.Text);
            komut.Parameters.AddWithValue("@Birim", txtbirim.Text);
            komut.Parameters.AddWithValue("@Kodu", txtkod.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["tonerr"].Clear();
            Kayıt_Göster();
            MessageBox.Show("Kayıt Güncellendi");
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void txtKodAra_TextChanged(object sender, EventArgs e)
        {
            DataTable tabl = new DataTable();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select*from tonerr where Birim like'%"+txtKodAra.Text+"%'",baglanti);
            adtr.Fill(tabl);
            dataGridView1.DataSource = tabl;
            baglanti.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtmarka.Text = dataGridView1.CurrentRow.Cells["Marka"].Value.ToString();
            txtadet.Text = dataGridView1.CurrentRow.Cells["Adet"].Value.ToString();
            txtrenk.Text = dataGridView1.CurrentRow.Cells["Renk"].Value.ToString();
            txtbirim.Text = dataGridView1.CurrentRow.Cells["Birim"].Value.ToString();
            txtkod.Text = dataGridView1.CurrentRow.Cells["Kodu"].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update tonerr set Adet=Adet+'"+int.Parse(txtEkle.Text)+"'where Marka='"+txtmarka.Text+"'",baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            Kayıt_Göster();
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
            MessageBox.Show("Ekleme Yapıldı");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update tonerr set Adet=Adet-'" + int.Parse(txtEkle.Text) + "'where Marka='" + txtmarka.Text + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            Kayıt_Göster();
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
            MessageBox.Show("Ürün Gönderildi");
        }
    }
}

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
namespace WindowsFormsApplication1
{
    public partial class lobby : Form
    {

        DataSet daset = new DataSet();
        bool durum;
        SqlConnection baglanti = new SqlConnection("SQLCONNECTIONSTRING");
        public lobby()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            musteriliste gec = new musteriliste();
            gec.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            kategori gec = new kategori();
            gec.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            marka gec = new marka();
            gec.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            urunekle gec = new urunekle();
            gec.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            urunlistele gec = new urunlistele();
            gec.Show();
        }

        private void lobby_Load(object sender, EventArgs e)
        {
            sepetliste();
            hesapla();
          /*  baglanti.Open();
            SqlCommand komut = new SqlCommand("select adsoyad from kayitedilecek", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboBox1.Items.Add(read["musterilistele"].ToString());

            }
            baglanti.Close();*/
        }

        private void sepetliste()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from sepet",baglanti);
            adtr.Fill(daset,"sepet");
            dataGridView1.DataSource = daset.Tables["sepet"];
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            baglanti.Close();
            
        }

        private void txttc_TextChanged(object sender, EventArgs e)
        {
            if (txttc.Text=="")
            {
                txtad.Text = "";
                txttel.Text = "";
            }

            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from kayitedilecek where tc like '"+txttc.Text+"'",baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtad.Text = read["adsoyad"].ToString();
                txttel.Text = read["tel"].ToString();
            }
            baglanti.Close();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            temizle();

            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from urun where barkodno like '" + txtkod.Text + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txturunad.Text = read["urunadi"].ToString();
                txtsatisfiyati.Text = read["satisfiyati"].ToString();
            }
            baglanti.Close();
        }
        private void temizle()
        {
            if (txtkod.Text == "")
            {
                foreach (Control item in groupBox2.Controls)
                {
                    if (item is TextBox)
                    {
                        if (item != txtmiktar)
                        {
                            item.Text = "";
                        }
                    }

                }
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into sepet (tc,adsoyad,telefon,barkodno,urunadi,miktari,satisfiyati,toplamfiyat,tarih) values (@tc,@adsoyad,@telefon,@barkodno,@urunadi,@miktari,@satisfiyati,@toplamfiyat,@tarih)", baglanti);
                komut.Parameters.AddWithValue("@tc", txttc.Text);
                komut.Parameters.AddWithValue("@adsoyad", txtad.Text);
                komut.Parameters.AddWithValue("@telefon", txttel.Text);
                komut.Parameters.AddWithValue("@barkodno", txtkod.Text);
                komut.Parameters.AddWithValue("@urunadi", txturunad.Text);
                komut.Parameters.AddWithValue("@miktari", int.Parse(txtmiktar.Text));
                komut.Parameters.AddWithValue("@satisfiyati", double.Parse(txtsatisfiyati.Text));
                komut.Parameters.AddWithValue("@toplamfiyat", double.Parse(txttplfiyat.Text));
                komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
                komut.ExecuteNonQuery();
                SqlCommand komutt = new SqlCommand("select sum(toplamfiyat) from sepet", baglanti);
                label9.Text = komutt.ExecuteScalar() + "TL";
                baglanti.Close();              
            txtmiktar.Text = "1";
            daset.Tables["sepet"].Clear();
            sepetliste();
            
            foreach (Control item in groupBox2.Controls)
            {
                if (item is TextBox)
                {
                    if (item != txtmiktar)
                    {
                        item.Text = "";
                    }
                }

            }
            
        }
        private void txtmiktar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txttplfiyat.Text = (double.Parse(txtmiktar.Text) * double.Parse(txtsatisfiyati.Text)).ToString();
            }
            catch (Exception)
            {
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from sepet where barkodno='"+dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString()+"'", baglanti);
            komut.ExecuteNonQuery();

            hesapla();

            baglanti.Close();
            MessageBox.Show("Ürün Sepetten Çıkarıldı");
            daset.Tables["sepet"].Clear();
            sepetliste();
           
        }

        private void button9_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from sepet" ,baglanti);
            komut.ExecuteNonQuery();

            label9.Text = "0TL";

            baglanti.Close();
            MessageBox.Show("Satış İptal Edildi");
            daset.Tables["sepet"].Clear();
            sepetliste();
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            satislistele gec = new satislistele();
            gec.Show();
        }

        private void hesapla()
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select sum(toplamfiyat) from sepet",baglanti);
                label9.Text = komut.ExecuteScalar() + "TL";
                baglanti.Close();
            }
            catch (Exception)
            {
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("insert into satis (tc,adsoyad,telefon,barkodno,urunadi,miktari,satisfiyati,toplamfiyat,tarih) values (@tc,@adsoyad,@telefon,@barkodno,@urunadi,@miktari,@satisfiyati,@toplamfiyat,@tarih)", baglanti);
                komut2.Parameters.AddWithValue("@tc", txttc.Text);
                komut2.Parameters.AddWithValue("@adsoyad", txtad.Text);
                komut2.Parameters.AddWithValue("@telefon", txttel.Text);
                komut2.Parameters.AddWithValue("@barkodno", dataGridView1.Rows[i].Cells["barkodno"].Value.ToString());
                komut2.Parameters.AddWithValue("@urunadi", dataGridView1.Rows[i].Cells["urunadi"].Value.ToString());
                komut2.Parameters.AddWithValue("@miktari", int.Parse(dataGridView1.Rows[i].Cells["miktari"].Value.ToString()));
                komut2.Parameters.AddWithValue("@satisfiyati", double.Parse(dataGridView1.Rows[i].Cells["satisfiyati"].Value.ToString()));
                komut2.Parameters.AddWithValue("@toplamfiyat", double.Parse(dataGridView1.Rows[i].Cells["toplamfiyat"].Value.ToString()));
                komut2.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
                komut2.ExecuteNonQuery();          
                SqlCommand komut3 = new SqlCommand("update urun set miktari=miktari-'" + int.Parse(dataGridView1.Rows[i].Cells["miktari"].Value.ToString()) + "'where barkodno='" + dataGridView1.Rows[i].Cells["barkodno"].Value.ToString() + "'", baglanti);
                komut3.ExecuteNonQuery();
                baglanti.Close();
                

            }
            baglanti.Open();
            SqlCommand komutt = new SqlCommand("delete from sepet", baglanti);
            komutt.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["sepet"].Clear();
            sepetliste();
                hesapla();
        }

        private void barkodkontrol()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from sepet ",baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (txtkod.Text == read["barkodno"].ToString())
                {
                    durum = false;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Musteriekle gec = new Musteriekle();
            gec.Show();
        }
    }
    }

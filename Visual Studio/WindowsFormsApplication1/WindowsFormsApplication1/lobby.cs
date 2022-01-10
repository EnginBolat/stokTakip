using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace WindowsFormsApplication1
{
    public partial class lobby : Form
    {
        public lobby()
        {
            InitializeComponent();
        }

        //Değişkenler ve Bağlantılar

        DataSet daset = new DataSet();
        bool durum;
        bool baglantiDurum = false;
        baglantiSinifi bgl = new baglantiSinifi();
        SqlConnection baglanti;
        
        void BaglantiAc()
        {
            if (baglantiDurum == false)
            {
                baglanti = new SqlConnection(bgl.baglantiAdresi);
                baglanti.Open();
                baglantiDurum = true;
            }
            else
            {
                MessageBox.Show("Bağlantı Zaten Açık");
            }
        }
        void BaglantiKapat()
        {
            if (baglantiDurum == true)
            {
                baglanti.Close();
                baglantiDurum = false;
            }
            else
            {
                MessageBox.Show("Bağlantı Zaten Kapalı");
            }
        }
        private void lobby_Load(object sender, EventArgs e)
        {
            sepetListe();
            Hesapla();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }
        private void sepetListe()
        {
            SqlConnection baglanti = new SqlConnection(bgl.baglantiAdresi);
            BaglantiAc();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from sepet", baglanti);
            adtr.Fill(daset, "sepet");
            dataGridView1.DataSource = daset.Tables["sepet"];
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            BaglantiKapat();
        }
        private void txttc_TextChanged(object sender, EventArgs e)
        {
            if (txttc.Text == "")
            {
                txtad.Text = "";
                txttel.Text = "";
            }
            BaglantiAc();
            SqlCommand komut = new SqlCommand("select * from kayitedilecek where tc like '" + txttc.Text + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtad.Text = read["adsoyad"].ToString();
                txttel.Text = read["tel"].ToString();
            }
            BaglantiKapat();
        }
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            Temizle();
            BaglantiAc();
            SqlCommand komut = new SqlCommand("select * from urun where barkodno like '" + txtkod.Text + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txturunad.Text = read["urunadi"].ToString();
                txtsatisfiyati.Text = read["satisfiyati"].ToString();
            }
            BaglantiKapat();
        }
        private void Temizle()
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
        private void Hesapla()
        {
            try
            {
                BaglantiAc();
                SqlCommand komut = new SqlCommand("select sum(toplamfiyat) from sepet", baglanti);
                label9.Text = komut.ExecuteScalar() + "TL";
                BaglantiKapat();
            }
            catch (Exception)
            {
            }
        }
        private void barkodKontrol()
        {
            BaglantiAc();
            SqlCommand komut = new SqlCommand("select * from sepet ", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (txtkod.Text == read["barkodno"].ToString())
                {
                    durum = false;
                }
            }
            BaglantiKapat();
        }
        private void btnMusteriEkle_Click(object sender, EventArgs e)
        {
            musteriEkle gec = new musteriEkle();
            gec.Show();
        }
        private void btnMusteriListele_Click(object sender, EventArgs e)
        {
            musteriListele gec = new musteriListele();
            gec.Show();
        }
        private void btnKategoriEkle_Click(object sender, EventArgs e)
        {
            kategoriEkle gec = new kategoriEkle();
            gec.Show();
        }
        private void btnMarkaEkle_Click(object sender, EventArgs e)
        {
            markaEkle gec = new markaEkle();
            gec.Show();
        }
        private void btnUrunEkle_Click(object sender, EventArgs e)
        {
            urunEkle gec = new urunEkle();
            gec.Show();
        }
        private void btnUrunListele_Click(object sender, EventArgs e)
        {
            urunListele gec = new urunListele();
            gec.Show();
        }
        private void btnSatisListele_Click(object sender, EventArgs e)
        {
            satisListele gec = new satisListele();
            gec.Show();
        }
        private void btnSil_Click(object sender, EventArgs e)
        {

            BaglantiAc();
            SqlCommand komut = new SqlCommand("delete from sepet where barkodno='" + dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString() + "'", baglanti);
            komut.ExecuteNonQuery();
            Hesapla();
            BaglantiKapat();
            MessageBox.Show("Ürün Sepetten Çıkarıldı");
            daset.Tables["sepet"].Clear();
            sepetListe();
        }
        private void btnSatisİptal_Click(object sender, EventArgs e)
        {
            BaglantiAc();
            SqlCommand komut = new SqlCommand("delete from sepet", baglanti);
            komut.ExecuteNonQuery();
            label9.Text = "0TL";
            BaglantiKapat();
            MessageBox.Show("Satış İptal Edildi");
            daset.Tables["sepet"].Clear();
            sepetListe();
        }
        private void btnSatisYap_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                BaglantiAc();
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
                BaglantiKapat();
            }
            BaglantiAc();
            SqlCommand komutt = new SqlCommand("delete from sepet", baglanti);
            komutt.ExecuteNonQuery();
            BaglantiKapat();
            daset.Tables["sepet"].Clear();
            sepetListe();
            Hesapla();
        }
        private void btnEkle_Click(object sender, EventArgs e)
        {
            BaglantiAc();
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
            BaglantiKapat();
            txtmiktar.Text = "1";
            daset.Tables["sepet"].Clear();
            sepetListe();
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
}

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
    public partial class kategori : Form
    {
        public kategori()
        {
            InitializeComponent();
        }

        bool durum;
        baglantiSinifi bgl = new baglantiSinifi();
        SqlConnection baglanti;
        void BaglantiAc()
        {
            if (durum == false)
            {
                baglanti = new SqlConnection(bgl.baglantiAdresi);
                baglanti.Open();
                durum = true;
            }
            else
            {
                MessageBox.Show("Bağlantı Zaten Açık");
            }
        }
        void BaglantiKapat()
        {
            if (durum == true)
            {
                baglanti.Close();
                durum = false;
            }
            else
            {
                MessageBox.Show("Bağlantı Zaten Kapalı");
            }
        }
        private void kategoriengelle()
        {
            durum = true;
            BaglantiAc();
            SqlCommand komut = new SqlCommand("select * from kategoribilgileri",baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (textBox1.Text == read["kategori"].ToString() ||textBox1.Text=="")
                {
                    durum = false;
                }
            }
            BaglantiKapat();

    }
        private void button1_Click(object sender, EventArgs e)
        {
            BaglantiAc();
            SqlCommand komut = new SqlCommand("insert into kategoribilgileri(kategori) values ('" + textBox1.Text + "')", baglanti);
            komut.ExecuteNonQuery();
            BaglantiKapat();     
            MessageBox.Show("Kategori Başarıyla Eklendi");         
            textBox1.Text = "";
        }

        private void kategori_Load(object sender, EventArgs e)
        {

        }
    }
}

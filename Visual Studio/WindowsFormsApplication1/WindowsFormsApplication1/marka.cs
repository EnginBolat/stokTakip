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
    public partial class marka : Form
    {
        public marka()
        {
            InitializeComponent();
        }
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



        private void button1_Click(object sender, EventArgs e)
        {
            BaglantiAc();
            SqlCommand komut = new SqlCommand("insert into markabilgileri(kategori,marka) values ('" + comboBox1.Text + "','" + textBox1.Text + "')", baglanti);
                komut.ExecuteNonQuery();
            BaglantiKapat();
                MessageBox.Show("Marka Başarıyla Eklendi");
     textBox1.Text = "";
            comboBox1.Text = "";
        }

        private void marka_Load(object sender, EventArgs e)
        {
            kategorigetir();
        }

        private void kategorigetir()
        {
            BaglantiAc();
            SqlCommand komut = new SqlCommand("select * from kategoribilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboBox1.Items.Add(read["kategori"].ToString());

            }
            BaglantiKapat();
        }
    }
}

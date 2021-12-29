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
        SqlConnection baglanti = new SqlConnection("SQLCONNECTIONSTRING");
        private void button1_Click(object sender, EventArgs e)
        {      
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into markabilgileri(kategori,marka) values ('" + comboBox1.Text + "','" + textBox1.Text + "')", baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();  
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
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from kategoribilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboBox1.Items.Add(read["kategori"].ToString());

            }
            baglanti.Close();
        }
    }
}

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

        SqlConnection baglanti = new SqlConnection("SQLCONNECTIONSTRING");
        bool durum;
        private void kategoriengelle()
    {
            durum = true;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from kategoribilgileri",baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (textBox1.Text == read["kategori"].ToString() ||textBox1.Text=="")
                {
                    durum = false;
                }
            }

    }
        private void button1_Click(object sender, EventArgs e)
        {
        
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into kategoribilgileri(kategori) values ('" + textBox1.Text + "')", baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();      
                MessageBox.Show("Kategori Başarıyla Eklendi");         
            textBox1.Text = "";
        }

        private void kategori_Load(object sender, EventArgs e)
        {

        }
    }
}

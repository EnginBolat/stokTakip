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
    public partial class Musteriekle : Form
    {
        public Musteriekle()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("SQLCONNECTIONSTRING");
        private void Musteriekle_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (txttc.Text.Length==11)
            {
                if (txttel.Text.Length==10)
                {
                    string metin = txtemail.Text;
                    int i = metin.IndexOf("@");
                    if (i == -1)
                    {
                        MessageBox.Show("Lütfen mail formatına uygun bir mail adresi giriniz!");
                    }

                    else
                    {
                        baglanti.Open();
                        SqlCommand komut = new SqlCommand("insert into kayitedilecek(tc,adsoyad,tel,adres,email) values (@tc,@adsoyad,@tel,@adres,@email)", baglanti);
                        komut.Parameters.AddWithValue("@tc", txttc.Text);
                        komut.Parameters.AddWithValue("@adsoyad", txtad.Text);
                        komut.Parameters.AddWithValue("@tel", txttel.Text);
                        komut.Parameters.AddWithValue("@adres", txtadres.Text);
                        komut.Parameters.AddWithValue("@email", txtemail.Text);
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        MessageBox.Show("Müşteri Kaydı Eklendi");
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen Geçerli Bir Telefon Numarası Giriniz.Başında 0 Olmadan Giriş Yapınız");
                }
            }
               
            else
            {
                MessageBox.Show("Lütfen Geçerli Bir Tc Kimlik Numarası Giriniz");
            }
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }

            }
        }
    }
    }


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
    public partial class girisEkrani : Form
    {
        public girisEkrani()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked==true)
            {
                checkBox1.Text = "Şifreyi Gizle";
                textBox2.PasswordChar = '\0';
            }
            else
            {
                checkBox1.Text = "Şifreyi Göster";
                textBox2.PasswordChar = '*';
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }
        SqlConnection baglanti = new SqlConnection("SQLCONNECTIONSTRING");
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                try
                {
                    string sorgu = ("select * from kayitli where kadi=@ad AND şifre=@sifre");
                    SqlParameter prm1 = new SqlParameter("ad", textBox1.Text.Trim());
                    SqlParameter prm2 = new SqlParameter("sifre", textBox2.Text.Trim());
                    SqlCommand komut = new SqlCommand(sorgu, baglanti);
                    komut.Parameters.Add(prm1);
                    komut.Parameters.Add(prm2);
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(komut);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        lobby gec = new lobby();
                        gec.Show();
                        this.Hide();
                        MessageBox.Show("Başarıyla Giriş Yapıldı Hoşgeldiniz.");
                    }
                    else
                    {
                        MessageBox.Show("Hatalı Giriş");
                        textBox1.Text = "";
                        textBox2.Text = "";
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Hatalı Giriş");
                    textBox1.Text = "";
                    textBox2.Text = "";             
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Sunucu İle Bağlantı Hatası");
                textBox1.Text = "";
                textBox2.Text = "";            
            }
            

        }
    }
}

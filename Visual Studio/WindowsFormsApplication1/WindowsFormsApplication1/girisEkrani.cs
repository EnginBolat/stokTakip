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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked==true)
            {
                checkBox1.Text = "Şifreyi Gizle";
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                checkBox1.Text = "Şifreyi Göster";
                txtPassword.PasswordChar = '*';
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                BaglantiAc();
                try
                {
                    string sorgu = ("select * from kayitli where kadi=@ad AND şifre=@sifre");
                    SqlParameter prm1 = new SqlParameter("ad", txtUsername.Text.Trim());
                    SqlParameter prm2 = new SqlParameter("sifre", txtPassword.Text.Trim());
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
                    }
                    else
                    {
                        MessageBox.Show("Hatalı Giriş");
                        txtUsername.Text = "";
                        txtPassword.Text = "";
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Hatalı Giriş");
                    txtUsername.Text = "";
                    txtPassword.Text = "";             
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Sunucu İle Bağlantı Hatası");
                txtUsername.Text = "";
                txtPassword.Text = "";            
            }
            BaglantiKapat();

        }
    }
}

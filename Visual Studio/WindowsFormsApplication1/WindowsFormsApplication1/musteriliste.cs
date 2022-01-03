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
    public partial class musteriliste : Form
    {
        public musteriliste()
        {
            InitializeComponent();
        }
        DataSet daset = new DataSet();
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



        private void musteriliste_Load(object sender, EventArgs e)
        {
            Kayıtgoster();
        }

        private void Kayıtgoster()
        {
            BaglantiAc();
            SqlDataAdapter adtr = new SqlDataAdapter("select tc,adsoyad,tel,adres,email from kayitedilecek", baglanti);
            adtr.Fill(daset, "kayitedilecek");
            dataGridView1.DataSource = daset.Tables["kayitedilecek"];
            BaglantiKapat();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txttc.Text = dataGridView1.CurrentRow.Cells["Tc"].Value.ToString();
            txtad.Text = dataGridView1.CurrentRow.Cells["AdSoyad"].Value.ToString();
            txtadres.Text = dataGridView1.CurrentRow.Cells["Adres"].Value.ToString();
            txttel.Text = dataGridView1.CurrentRow.Cells["Tel"].Value.ToString();
            txtemail.Text = dataGridView1.CurrentRow.Cells["Email"].Value.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            BaglantiAc();
            SqlCommand komut = new SqlCommand("update kayitedilecek set adsoyad=@AdSoyad,tel=@Tel,adres=@Adres,email=@Email where tc=@Tc ",baglanti);
            komut.Parameters.AddWithValue("@Tc", txttc.Text);
            komut.Parameters.AddWithValue("@AdSoyad", txtad.Text);
            komut.Parameters.AddWithValue("@Tel", txttel.Text);
            komut.Parameters.AddWithValue("@Adres", txtadres.Text);
            komut.Parameters.AddWithValue("@Email", txtemail.Text);
            komut.ExecuteNonQuery();
            BaglantiKapat();
            daset.Tables["kayitedilecek"].Clear();
            Kayıtgoster();
            MessageBox.Show("Müşteri Kaydı Güncellendi");
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            BaglantiAc();
            SqlCommand komut = new SqlCommand("delete from kayitedilecek where tc='"+dataGridView1.CurrentRow.Cells["Tc"].Value.ToString()+"'", baglanti);
            komut.ExecuteNonQuery();
            BaglantiKapat();
            daset.Tables["kayitedilecek"].Clear();
            Kayıtgoster();
            MessageBox.Show("Kayıt Silindi");
            txtad.Text = " ";
            txtadres.Text = " ";
            txttel.Text = " ";
            txtemail.Text = " ";
            txttc.Text = " ";

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

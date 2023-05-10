using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Data.SqlClient;
using System.Management.Instrumentation;

namespace Doviz_Ofisi
{
    


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string bugun = "https://www.tcmb.gov.tr/kurlar/today.xml";
            var xmldosya = new XmlDocument();
            xmldosya.Load(bugun);

            string dolaralıs = xmldosya.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteBuying").InnerXml;
            Lbldolaralıs.Text = dolaralıs;

            string dolarsatıs = xmldosya.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteSelling").InnerXml;
            lbldolarsatıs.Text = dolarsatıs;

            string euroalıs = xmldosya.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteBuying").InnerXml;
            Lbleuroalıs.Text = euroalıs;


            string eurosatıs = xmldosya.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteSelling").InnerXml;
            Lbleurosatıs.Text = eurosatıs;

            baglanti.Open();
            SqlCommand kmt = new SqlCommand("select ıd from Tbl_Kasa ",baglanti);
            SqlDataReader drrr=kmt.ExecuteReader();
            while (drrr.Read())
            {
                label23.Text = drrr[0].ToString();

            }
            baglanti.Close();

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void Btndolaral_Click(object sender, EventArgs e)
        {
            Txtkur.Text = Lbldolaralıs.Text;
            label17.Text = Lbldolaralıs.Text;
        }

        private void Btndolarsat_Click(object sender, EventArgs e)
        {
            Txtkur.Text = lbldolarsatıs.Text;
            label17.Text = lbldolarsatıs.Text;
        }

        private void Btneuroal_Click(object sender, EventArgs e)
        {
            Txtkur.Text = Lbleuroalıs.Text;
            label17.Text = Lbleuroalıs.Text;


        }

        private void Btneurosat_Click(object sender, EventArgs e)
        {
            Txtkur.Text = Lbleurosatıs.Text;
            label17.Text = Lbleurosatıs.Text;
        }
        int dolar;
        private void Btnsatısyap_Click(object sender, EventArgs e)
        {
            double sayı1, sayı2, tutar;
            sayı1=Convert.ToDouble(Txtkur.Text);
            sayı2=Convert.ToDouble(Txtmiktar.Text);
            tutar = sayı1 * sayı2;
            Txttutar.Text = tutar.ToString();

            label4.Text=tutar.ToString();
            if (label17.Text==Lbldolaralıs.Text)
            {
                label15.Text = "-"+Txtmiktar.Text;
                label7.Text = "0";
            }
            if (label17.Text == lbldolarsatıs.Text)
            {
                label15.Text = Txtmiktar.Text;
                label4.Text = "-"+Txttutar.Text;
                label7.Text = "0";
            }
            if (label17.Text ==Lbleuroalıs.Text)
            {
                label7.Text = "-"+Txtmiktar.Text;
                label4.Text = Txttutar.Text;
                label15.Text="0";
            }
            if (label17.Text == Lbleurosatıs.Text)
            {
                label7.Text = Txtmiktar.Text;
                label4.Text = "-" + Txttutar.Text;
                label15.Text = "0";
            }


            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into Tbl_Girdicikti (tlgirdicikti,eurogirdicikti,dolargirdicikti) values (@p1,@p2,@p3)",baglanti);
            komut.Parameters.AddWithValue("@p1",decimal.Parse(label4.Text));
            komut.Parameters.AddWithValue("@p2",decimal.Parse(label7.Text));
            komut.Parameters.AddWithValue("p3",decimal.Parse(label15.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Bilgiler kasaya eklendi");


           


        }

        private void Txtkur_TextChanged(object sender, EventArgs e)
        {
            Txtkur.Text = Txtkur.Text.Replace(".",",");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            double kur=Convert.ToDouble(Txtkur.Text);
            int miktar=Convert.ToInt32(Txtmiktar.Text);
            int tutar = Convert.ToInt32(miktar / kur);
            
            Txttutar.Text=tutar.ToString();

            double  kalan;
            kalan = miktar % kur;
            Txtkalan.Text = kalan.ToString();   

        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-045VOUG;Initial Catalog=DBKasa;Integrated Security=True");
        private void button2_Click(object sender, EventArgs e)
        {


            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("select sum(tlgirdicikti) from Tbl_Girdicikti", baglanti);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                label19.Text = dr2[0].ToString();

            }
            baglanti.Close();


            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("select sum(eurogirdicikti) from Tbl_Girdicikti", baglanti);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                label18.Text = dr3[0].ToString();

            }
            baglanti.Close();

            baglanti.Open();
            SqlCommand komut4 = new SqlCommand("select sum(dolargirdicikti) from Tbl_Girdicikti", baglanti);
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                label16.Text = dr4[0].ToString();

            }
            baglanti.Close();

            baglanti.Open();
            SqlCommand komut5 = new SqlCommand("update Tbl_Kasa set TL=@p1,euro=@p2,dolar=@p3 where ıd=@p4",baglanti);
            komut5.Parameters.AddWithValue("@p1",double.Parse(label19.Text));
            komut5.Parameters.AddWithValue("@p2",double.Parse( label18.Text));
            komut5.Parameters.AddWithValue("@p3",double.Parse( label16.Text));
            komut5.Parameters.AddWithValue("@p4",double.Parse(label23.Text));
            komut5.ExecuteNonQuery();





            baglanti.Close();








            Frmkasa frmkasa = new Frmkasa();    
            frmkasa.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("select sum(tlgirdicikti) from Tbl_Girdicikti",baglanti);
            SqlDataReader dr2=komut2.ExecuteReader();
            while(dr2.Read())
            {
                label19.Text = dr2[0].ToString();

            }
            baglanti.Close();


            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("select sum(eurogirdicikti) from Tbl_Girdicikti", baglanti);
            SqlDataReader dr3 = komut2.ExecuteReader();
            while (dr3.Read())
            {
                label18.Text = dr2[0].ToString();

            }baglanti.Close();

            baglanti.Open();
            SqlCommand komut4 = new SqlCommand("select sum(dolargirdicikti) from Tbl_Girdicikti", baglanti);
            SqlDataReader dr4 = komut2.ExecuteReader();
            while (dr4.Read())
            {
                label16.Text = dr2[0].ToString();

            }baglanti.Close();
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }
    }
}

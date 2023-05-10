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

namespace Doviz_Ofisi
{
    public partial class Frmkasa : Form
    {
        public Frmkasa()
        {
            InitializeComponent();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuImageButton1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-045VOUG;Initial Catalog=DBKasa;Integrated Security=True");
        private void Frmkasa_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Girdicikti", baglanti);
            da.Fill(dt);
            dataGridView2.DataSource = dt;

            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("select * from Tbl_Kasa",baglanti);
            da2.Fill(dt2);
            dataGridView1.DataSource = dt2;
        }
    }
}

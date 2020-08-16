using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication5
{
    public partial class Formdangnhap : Form
    {
        public Formdangnhap()
        {
            InitializeComponent();
        }

        private void Formdangnhap_Load(object sender, EventArgs e)
        {
            
                 
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            if (username == "e251" && password == "tram590")
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Hide();
                MessageBox.Show("ĐĂNG NHẬP THÀNH CÔNG");
                FORMKIEMTRA f3 = new FORMKIEMTRA();
                f3.ShowDialog();
            }
            else
            {
                textBox1.Clear();
                textBox2.Clear();
                MessageBox.Show(" ĐĂNG NHẬP SAI");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

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
    public partial class Formnguon : Form
    {
   
        private bool bat = false ,giay = false;
        Timer t = new Timer();
        int width = 500, height = 500;
        int u ;
        int cx, cy;
    
       
        Pen pen = new Pen(Color.Brown);
      
        Bitmap bmp;

        public Formnguon()
        {
            InitializeComponent();
          
        
 
        }

       
        private void Formnguon_Load(object sender, EventArgs e)
        {
            bmp = new Bitmap(width + 1, height + 1);
            this.BackColor = Color.Black;
            cx = width / 2;
            cy = height / 2;
            u = 0;
            t.Interval = 30;
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();

            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 1000; // 1 second
            timer1.Start();
            hien.Visible = true;
            bat = !bat;
           
        }
        private void t_Tick(object sender, EventArgs e)
        {
           


        }
        private int counter = 6;

        private void button2_Click_1(object sender, EventArgs e)
        {
            
          
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            counter--;
            if (counter == 0)
            
                timer1.Stop();
                hien.Text = " 00:0 "+counter.ToString();
            if (counter == 0)
            {
                FormMain f2 = new FormMain();
                f2.ShowDialog();
               
                this.Close();     
            }
        }

        private void hien_Click(object sender, EventArgs e)
        {
            giay = !giay;
        }

        private void MAU1_Click(object sender, EventArgs e)
        {

        }
      

    }
}

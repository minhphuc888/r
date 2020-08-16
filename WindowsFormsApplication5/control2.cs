using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication5
{
    public partial class control2 : UserControl
    {
        public control2()
        {
            InitializeComponent();
        }

        public event EventHandler ClickButton1;
        public event EventHandler ClickButton2;
        public event EventHandler ClickButton3;
        public event EventHandler ClickButton4;
        public event EventHandler ClickButton5;
        public event EventHandler ClickButton6;
        public event EventHandler ClickButton7;
        public event EventHandler ClickButton8;
        public event EventHandler ClickButton9;
        public event EventHandler ClickButton10;
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            
        }

        private void label14_Click(object sender, EventArgs e)
        {
           
        }

        private void button30_Click(object sender, EventArgs e)
        {
      
        }

        private void button29_Click(object sender, EventArgs e)
        {

        }

        private void button31_Click(object sender, EventArgs e)
        {
           
           
            if (ClickButton1 != null)
                ClickButton1(this, new EventArgs());
           
        }

        private void LE2_ClickButton_Click(object sender, EventArgs e)
        {
          

            if (ClickButton3 != null)
            {
                ClickButton3(this, new EventArgs());
              
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {

                if (ClickButton6 != null)
                    ClickButton6(this, new EventArgs());
        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void HE_ClickButton_Click(object sender, EventArgs e)
        {
            
            if (ClickButton2 != null)
                ClickButton2(this, new EventArgs());
        }

        private void LE1_ClickButton_Click(object sender, EventArgs e)
        {
            if (ClickButton4 != null)
                ClickButton4(this, new EventArgs());
        }

        private void EW_ClickButton_Click(object sender, EventArgs e)
        {
            if (ClickButton5 != null)
                ClickButton5(this, new EventArgs());
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (ClickButton8 != null)
                ClickButton8(this, new EventArgs());
        }

        private void LE260_ClickButton_Click(object sender, EventArgs e)
        {
            if (ClickButton7 != null)
                ClickButton7(this, new EventArgs());
        }

      
    }
}

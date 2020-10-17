using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HafmanTree
{
    public partial class SplashScrean : Form
    {
        public SplashScrean()
        {
            InitializeComponent();
        }     
        private void SplashScrean_KeyUp(object sender, KeyEventArgs e)
        {
                this.Close();
        }

        int Counter = 1;
        private void timer1_Tick(object sender, EventArgs e)
        {
            Counter++;
            switch (Counter)
            {
                case 1:
                    break;
                case 2:
                    label6.Visible = true;
                    break;
                case 3:
                    label7.Visible = true;
                    break;
                case 4:
                    label2.Visible = true;
                    break;
                case 5:
                    label3.Visible = true;
                    break;
                case 6:
                    label5.Visible = true;
                    break;
                case 7:
                    label4.Visible = true;
                    break;
                case 8:
                    label8.Visible = true;
                    break;
                default:
                    break;
            }
        }

    }
}

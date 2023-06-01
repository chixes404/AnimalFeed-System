using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimalFood
{
    public partial class Begin : Form
    {
        public Begin()
        {
            InitializeComponent();
            timer1.Start();
        }

        int startp = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            startp += 1;
            myProgress.Value = startp;
            percent.Text = startp + "%";
            if (myProgress.Value == 100  )
            {
                myProgress.Value = 0;
                login obj = new login();
                obj.Show();
                this.Hide();
                timer1.Stop(); 
            }


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
